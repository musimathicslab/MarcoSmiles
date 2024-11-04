from flask import Flask, jsonify, request, session
from utils.utils import read_request
from DQN.Double_DQN import Network

app = Flask(__name__)


@app.route('/hello-world', methods=['GET'])
def hello_world():
    return jsonify({"message": "hello world!"})


@app.route('/hand-data', methods=['POST'])
def hand_data():
    try:
        global network
        
        # Print data received
        hand_data, note = read_request(request.json)
        prediction = network.learn(hand_data, note)
        
        # Restituisci il risultato come risposta JSON
        return jsonify({"message": f"{prediction}"})
    except Exception as e:
        print(e)
        return jsonify({"message": "Error processing request"}), 500


@app.route('/save-model', methods=['GET'])
def end_training():
    try:
        global network
        network.save_model()
        network.save_target_model()
        return jsonify({"message": "Network saved"}), 200
    except Exception as e:
        print(e)
        return jsonify({"message": "Error saving network"}), 500
    
@app.route('/new-model', methods=['POST'])
def new_model():
    try:
        global network
        output_dimension: int = request.json.get('output_dimension', None)
        if output_dimension is not None:
            network = Network(action_space_shape=output_dimension)
        else:
            network = Network()
        network.save_model()
        network.save_target_model()
        return jsonify({"message": "New model created"})
    except Exception as e:
        print(e)
        return jsonify({"message": "Error creating new model"}), 500


if __name__ == '__main__':
    network: Network = Network()
    network.load_model()
    network.load_target_model()
    app.run(host='0.0.0.0', port=5005)