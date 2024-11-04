import io
import json
from DQN.MSenv import MS_env
from DQN.basic_buffer import BasicBuffer
import torch.nn as nn
import torch.nn.functional as F
import numpy as np
import torch
import dill


class DQN(nn.Module):

    def __init__(self, input_dim, output_dim):
        super(DQN, self).__init__()
        self.input_dim = input_dim
        self.output_dim = output_dim

        self.fc = nn.Sequential(
            nn.Linear(self.input_dim, self.input_dim * 2),
            nn.Dropout(0.2),
            nn.ReLU(),
            nn.Linear(self.input_dim * 2, self.input_dim),
            nn.Dropout(0.2),
            nn.ReLU(),
            nn.Linear(self.input_dim, self.input_dim),
            nn.Dropout(0.2),
            nn.ReLU(),
            nn.Linear(self.input_dim, self.input_dim // 2),
            nn.Dropout(0.2),
            nn.ReLU(),
            nn.Linear(self.input_dim // 2, self.output_dim)
        )

    def forward(self, state):
        qvals = self.fc(state)
        return qvals


class DQNAgent:

    def __init__(self, env, learning_rate=3e-4, gamma=0.99, buffer_size=10000, tau=0.005):
        self.env = env
        # Learning rate
        self.learning_rate = learning_rate
        # discount factor
        self.gamma = gamma
        # that is ht edimension of the expreience replay
        self.replay_buffer = BasicBuffer(max_size=buffer_size)
        # Updating factor, is a wey to weight the target net update
        # in this script the periodical update of the net is applyed for every 1000 interval
        # if u want to use the same chose as van Hasselt, that use the periodic UPDATE u can set the tau to 1, so u will obtain a perfect copy of the online network
        # u can always try to do a mix of them using the tau and the periodic UPDATE
        self.tau = tau
        self.tau = 1

        self.target_update_interval = 100

        self.update_counter = 0

        # If CUDA available select it, else CPU mode
        self.device = torch.device(
            "cuda" if torch.cuda.is_available() else "cpu")

        # creating 2 model
        # Model that chose the actions
        self.model = DQN(env.observation_space_shape,
                         env.action_space.n).to(self.device)
        # Target that evaluate the actions
        self.target_model = DQN(
            env.observation_space_shape, env.action_space.n).to(self.device)

        # hard copy model parameters to target model parameters
        for target_param, param in zip(self.model.parameters(), self.target_model.parameters()):
            target_param.data.copy_(param)

        self.optimizer = torch.optim.Adam(self.model.parameters())

    def get_action(self, state, eps=0.20):
        state = torch.FloatTensor(state).float().unsqueeze(0).to(self.device)
        qvals = self.model.forward(state)
        action = np.argmax(qvals.cpu().detach().numpy())

        if (np.random.randn() < eps):
            return self.env.action_space.sample()

        return action

    def compute_loss(self, batch):
        states, actions, rewards, next_states, dones = batch
        states = torch.FloatTensor(states).to(self.device)
        actions = torch.LongTensor(actions).to(self.device)
        rewards = torch.FloatTensor(rewards).to(self.device)
        next_states = torch.FloatTensor(next_states).to(self.device)
        dones = torch.FloatTensor(dones)

        # resize tensors
        actions = actions.view(actions.size(0))
        dones = dones.view(dones.size(0))

        curr_Q = self.model.forward(states).gather(
            1, actions.view(actions.size(0), 1))
        # evaluate all the possible action starting from the next_stete associated with a q_value
        max_action_next_Q_online = self.model.forward(next_states)
        # Now i need to take the action with the max q-Value so i can do argmax
        max_action_next_Q_online = torch.argmax(max_action_next_Q_online, 1)
        max_action_next_Q_online = max_action_next_Q_online.view(
            max_action_next_Q_online.size(0), 1)

        # Calculate q-values for the next states using target network
        next_Q_target = self.target_model.forward(next_states)

        # Use the target network for evaluate the action selected by online net
        next_Q_target = next_Q_target.gather(1, max_action_next_Q_online)

        expected_Q = rewards + (1 - dones) * self.gamma * next_Q_target

        loss = F.mse_loss(curr_Q, expected_Q.detach())

        return loss

    def update(self, batch_size):
        batch = self.replay_buffer.sample(batch_size)
        loss = self.compute_loss(batch)

        self.optimizer.zero_grad()
        loss.backward()
        self.optimizer.step()

        if self.update_counter % self.target_update_interval == 0:
            self.update_target_net()
            self.update_counter = 0
        else:
            self.update_counter += 1

    # Defining the function to update the target network, done every update interval
    def update_target_net(self):
        for target_param, param in zip(self.target_model.parameters(), self.model.parameters()):
            target_param.data.copy_(
                self.tau * param + (1 - self.tau) * target_param)


class Network:
    """
    observation_space_shape: int - the number of features in the observation space (#joints in the hand)
    action_space_shape: int - the number of possible outputs (#notes to be predicted)
    """

    def __init__(self, observation_space_shape=154, action_space_shape=12, batch_size=1, max_steps=10, max_episodes=100):
        self.batch_size = batch_size
        self.max_steps = max_steps
        self.max_episodes = max_episodes
        self.step_counter = 0
        self.episode_counter = 0
        self.episode_reward = 0
        self.epoch_rewards = []
        self.episode_rewards = []
        self.env = MS_env(observation_space_shape, action_space_shape)
        self.agent = DQNAgent(self.env)

    def learn(self, state, actual_label):
        state = self.env.reset(state, actual_label)
        self.episode_reward = 0

        for step in range(self.max_steps):
            action = self.agent.get_action(state)
            next_state, reward, done, _ = self.env.step(action)
            self.agent.replay_buffer.push(
                state, action, reward, next_state, done)
            self.episode_reward += reward

            if len(self.agent.replay_buffer) > self.batch_size:
                self.agent.update(self.batch_size)

            if done or step == self.max_steps - 1:
                self.episode_rewards.append(self.episode_reward)
                break

            state = next_state

        self.episode_counter += 1
        if self.episode_counter == self.max_episodes:
            self.epoch_rewards.append(self.episode_reward)
            self.episode_counter = 0

        return action

    def save_model(self, model_name_prefix="models/model"):
        # JIC you want to add a timestamp / other info
        model_name = f"{model_name_prefix}"
        torch.save(self.agent.model.state_dict(), f"{model_name}.pth")
        torch.save(self.agent.optimizer.state_dict(),
                   f"{model_name}_optimizer.pth")

    def load_model(self, model_path="models/model.pth", optimizer_path="models/model_optimizer.pth"):
        try:
            with open(model_path, "rb") as f:
                buffer = io.BytesIO(f.read())
                self.agent.model.load_state_dict(torch.load(buffer, weights_only=False))
        except Exception as e:
            print(e)
        try:
            with open(optimizer_path, "rb") as f:
                buffer = io.BytesIO(f.read())
                self.agent.optimizer.load_state_dict(torch.load(buffer, weights_only=False))
        except Exception as e:
            print(e)
    
    def save_target_model(self, model_name_prefix="models/target_model"):
        # JIC you want to add a timestamp / other info
        model_name = f"{model_name_prefix}"
        torch.save(self.agent.target_model.state_dict(), f"{model_name}.pth")

    def load_target_model(self, model_path="models/target_model.pth"):
        try:
            with open(model_path, "rb") as f:
                buffer = io.BytesIO(f.read())
                self.agent.target_model.load_state_dict(torch.load(buffer, weights_only=False))
        except Exception as e:
            print(e)

    def close_env(self):
        self.env.close()
