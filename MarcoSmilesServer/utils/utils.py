def read_request(request_as_json):
    hand_data = request_as_json.get('handWrapper')
    note = request_as_json.get('note')
    # This will be a list of all features from the hand
    numerical_values = []

    # Read the json data
    for key in hand_data:
        # Position
        position = hand_data[key]['position']
        numerical_values.extend(position.values())
        
        # Rotation
        rotation = hand_data[key]['rotation']
        numerical_values.extend(rotation.values())
        
    return numerical_values, note