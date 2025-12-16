import pandas as pd
from sklearn.feature_extraction.text import TfidfVectorizer
from sklearn.svm import SVC
from sklearn.preprocessing import LabelEncoder
from sklearn.pipeline import Pipeline
import joblib
import os

def train_model():
    script_dir = os.path.dirname(os.path.abspath(__file__))
    data_path = os.path.join(script_dir, 'train_data.csv')
    model_path = os.path.join(script_dir, 'numerology_model.pkl')
    
    print("Loading training data...")
    df = pd.read_csv(data_path)
    
    X = df['keywords']
    y = df['numbers']
    
    print(f"Training samples: {len(df)}")
    print(f"Unique numbers: {sorted(df['numbers'].unique())}")
    print(f"\nClass distribution:")
    for num in sorted(df['numbers'].unique()):
        count = (df['numbers'] == num).sum()
        print(f"  Number {num}: {count} samples")
    
    # Create pipeline with SVM
    pipeline = Pipeline([
        ('tfidf', TfidfVectorizer(
            lowercase=True,
            strip_accents='unicode',
            ngram_range=(1, 2),
            max_features=300,
            min_df=1,
            max_df=0.9,
            use_idf=True,
            smooth_idf=True,
            sublinear_tf=True
        )),
        ('clf', SVC(
            kernel='rbf',
            C=10.0,
            gamma='scale',
            random_state=42,
            class_weight='balanced',
            decision_function_shape='ovr'
        ))
    ])
    
    print("\nTraining SVM model...")
    pipeline.fit(X, y)
    
    # Calculate training accuracy
    train_predictions = pipeline.predict(X)
    accuracy = (train_predictions == y).sum() / len(y)
    print(f"Training accuracy: {accuracy:.3f}")
    
    # Save the model
    joblib.dump(pipeline, model_path)
    print(f"\nModel saved to {model_path}")
    
    # Test predictions
    print("\nTesting predictions on various inputs:")
    test_cases = [
        ("new beginning start", "Expected: 1 or 10"),
        ("balance partnership harmony", "Expected: 2"),
        ("creativity expression joy", "Expected: 3"),
        ("stability foundation structure", "Expected: 4"),
        ("change freedom adventure", "Expected: 5"),
        ("love caring nurturing", "Expected: 6"),
        ("spiritual wisdom mystical", "Expected: 7"),
        ("power success wealth", "Expected: 8"),
        ("completion humanitarian compassion", "Expected: 9"),
        ("intuition enlightenment master", "Expected: 11"),
        ("master builder vision", "Expected: 22"),
        ("master teacher compassion service", "Expected: 33"),
        ("transformation rebirth karma", "Expected: 13"),
        ("prosperity abundance achievement", "Expected: 18"),
        ("leadership authority confidence", "Expected: 28")
    ]
    
    print("\n{:<40} {:<10} {}".format("Input", "Predicted", "Expected"))
    print("-" * 70)
    for text, expected in test_cases:
        pred = pipeline.predict([text])[0]
        print("{:<40} {:<10} {}".format(text, pred, expected))
    
    # Check for prediction diversity
    all_predictions = [pipeline.predict([text])[0] for text, _ in test_cases]
    unique_preds = len(set(all_predictions))
    print(f"\nPrediction diversity: {unique_preds}/{len(test_cases)} unique predictions")
    
    if unique_preds < 5:
        print("WARNING: Low prediction diversity. Model may need more varied training data.")
    else:
        print("Good prediction diversity!")

if __name__ == "__main__":
    train_model()