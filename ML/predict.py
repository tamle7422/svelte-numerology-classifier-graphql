import sys
import joblib
import os

def predict(text):
    script_dir = os.path.dirname(os.path.abspath(__file__))
    model_path = os.path.join(script_dir, 'numerology_model.pkl')
    
    if not os.path.exists(model_path):
        print("Error: Model not found. Please train the model first.", file=sys.stderr)
        sys.exit(1)
    
    model = joblib.load(model_path)
    prediction = model.predict([text])[0]
    print(prediction)

if __name__ == "__main__":
    if len(sys.argv) < 2:
        print("Error: No input text provided", file=sys.stderr)
        sys.exit(1)
    
    input_text = sys.argv[1]
    predict(input_text)