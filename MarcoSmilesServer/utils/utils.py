import json
import numpy as np

def read_request(request_as_json):
    hand_data = request_as_json.get('HandWrappers')
    note = request_as_json.get('Note')
    # All poses
    all_poses = []

    # Read the json data
    for pose in hand_data:
        numerical_values = []
        for joint in pose:
            # Position
            positions = pose[joint] # dove credi di stare in Java? testa di cazzo
            for coordinate in positions:
                numerical_values.append(positions[coordinate])
        all_poses.append(numerical_values)

    # print(f"Received data: {numerical_values}")
    return all_poses, note


# write request to file
def write_request(request_as_json):
    with open('tmp.json', 'a') as f:
        json.dump(request_as_json, f)

def toint(x):
    if isinstance(x, np.int64):
        return x.tolist()
    else:
        return x

# retrive action_space from file
def load_action_space():
    try:
        with open('models/action_space.json', 'r') as f:
            return json.load(f)
    except Exception as e:
        print(e)
