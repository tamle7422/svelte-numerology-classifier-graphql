# Numerology Prediction Application
A full-stack web application that predicts numerology numbers (1-33) based on keywords or text input using machine learning. Built with Svelte frontend, ASP.NET Core backend with GraphQL API, and scikit-learn for ML predictions.

## Features
- **ML-Powered Predictions**: Uses Support Vector Machine (SVM) to classify text into numerology numbers 1-33
- **Modern Frontend**: Clean, responsive Svelte interface with real-time predictions
- **GraphQL API**: Flexible and efficient API using HotChocolate
- **Persistent Storage**: SQLite database stores all predictions with timestamps
- **History Tracking**: View all past predictions with refresh capability
- **330+ Training Samples**: Comprehensive training dataset for accurate predictions

## Technology Stack
### Frontend
- **Svelte 4** - Reactive UI framework
- **Vite** - Fast build tool and dev server
- **URQL** - GraphQL client for Svelte
- **CSS3** - Custom styling with modern design

### Backend
- **ASP.NET Core 8** - Web API framework
- **HotChocolate** - GraphQL server
- **Entity Framework Core** - ORM for database operations
- **SQLite** - Lightweight database

### Machine Learning
- **Python 3.8+** - ML runtime
- **scikit-learn** - ML library (SVM classifier)
- **pandas** - Data processing
- **joblib** - Model serialization

## Project Structure
numerology-classifier-graphql/
├── backend/
 │   └── NumerologyAPI/
 │       ├── Program.cs                 # Main entry point
 │       ├── appsettings.json          # Configuration
 │       ├── NumerologyAPI.csproj      # Project file
 │       ├── Models/
 │       │   └── Prediction.cs         # Data model
 │       ├── Data/
 │       │   └── AppDbContext.cs       # Database context
 │       ├── GraphQL/
 │       │   ├── Query.cs              # GraphQL queries
 │       │   ├── Mutation.cs           # GraphQL mutations
 │       │   └── Types/
 │       │       └── PredictionType.cs # GraphQL type
 │       └── Services/
 │           └── PredictionService.cs  # ML service
├── frontend/
 │   ├── package.json                  # Dependencies
 │   ├── vite.config.js               # Vite config
 │   ├── svelte.config.js             # Svelte config
 │   └── src/
 │       ├── main.js                  # Entry point
 │       ├── app.css                  # Global styles
 │       ├── App.svelte               # Root component
 │       ├── routes/
 │       │   ├── Predict.svelte       # Prediction page
 │       │   └── History.svelte       # History page
 │       └── lib/
 │           └── graphql.js           # GraphQL client
├── ML/
 │   ├── train_data.csv               # Training dataset (330 rows)
 │   ├── train_model.py               # Model training script
 │   ├── predict.py                   # Prediction script
 │   ├── requirements.txt             # Python dependencies
 │   └── numerology_model.pkl         # Trained model (generated)
 └── README.md                        # This file


## Prerequisites
- **.NET 8.0 SDK** - [Download](https://dotnet.microsoft.com/download)
- **Node.js 18+** and npm - [Download](https://nodejs.org/)
- **Python 3.8+** - [Download](https://www.python.org/downloads/)
- **Git** (optional) - For cloning the repository

## Installation
### 1. Clone or Download the Project
```bash
git clone <repository-url>
cd numerology-classifier-graphql
```

### 2. Setup Python ML Environment
```bash
cd ML
pip install -r requirements.txt
python3 train_model.py
```

- Expected output: 
  - Loading training data...
  - Training samples: 330
  - Unique numbers: [1, 2, 3, ..., 33]
  - ...
  - Training accuracy: 0.xxx
  - Model saved to .../numerology_model.pkl

### 3. Setup Backend (ASP.NET)
```bash
cd ../backend
dotnet new webapi -n NumerologyAPI
cd NumerologyAPI
```

- Install required NuGet packages:
```bash
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package HotChocolate.AspNetCore
dotnet add package HotChocolate.Data.EntityFramework
```

- Copy all the C# files into their respective folders, then:
```bash
dotnet restore
dotnet build
```

### 4. Setup Frontend (Svelte)
```bash
cd ../../frontend
npm install
```

## Running the Application
### Start the Backend
```bash
cd backend/NumerologyAPI
dotnet run --urls "http://localhost:5000"
```

- The backend will start at: **http://localhost:5000**
- You should see: info: Microsoft.Hosting.Lifetime[14], Now listening on: http://localhost:5000


### Start the Frontend (in a new terminal)
```bash
cd frontend
npm run dev
```

- The frontend will start at: **http://localhost:5173**

## Usage
 - 1. **Open your browser** to http://localhost:5173
 - 2. **Predict Page**: 
     - Enter keywords or text (e.g., "spiritual wisdom intuition")
     - Click "Predict Number"
     - View your predicted numerology number (1-33)
 - 3. **History Page**:
     - View all past predictions
     - Click "Refresh" to reload history
     - See timestamps and input text for each prediction

## GraphQL Playground
- Access the GraphQL API directly at: **http://localhost:5000/graphql**

### Example Queries
**Get all predictions:**
```graphql
query {
  predictions {
    id
    inputText
    predictedNumber
    timestamp
  }
}
```

**Create a new prediction:**
```graphql
mutation {
  addPrediction(inputText: "love harmony balance") {
    id
    inputText
    predictedNumber
    timestamp
  }
}
```

## API Documentation
### GraphQL Schema
#### Query Type
```graphql
type Query {
  predictions: [Prediction!]!
  prediction(id: Int!): Prediction
}
```

#### Mutation Type
```graphql
type Mutation {
  addPrediction(inputText: String!): Prediction!
}
```

#### Prediction Type
```graphql
type Prediction {
  id: Int!
  inputText: String!
  predictedNumber: Int!
  timestamp: DateTime!
}
```

## Machine Learning Model
### Algorithm
- **SVM (Support Vector Machine)** with RBF kernel
- **TF-IDF Vectorization** for text feature extraction
- **Class balancing** to prevent bias

### Training Data
- **330 samples** across 33 numerology numbers
- **10 variations per number** for better accuracy
- Includes synonyms, related concepts, and varied phrasings

### Model Performance
- Training accuracy typically 95%+
- Good prediction diversity across all 33 numbers
- Handles both single words and phrases

### Retraining the Model
- If you modify `train_data.csv`:
```bash
cd ML
python3 train_model.py
```

- Then restart the backend to use the new model.
## Configuration
### Backend Port Configuration

- Edit `backend/NumerologyAPI/appsettings.json`:
```json
{
  "Kestrel": {
    "Endpoints": {
      "Http": {
        "Url": "http://localhost:5000"
      }
    }
  }
}
```

### Frontend API URL
- Edit `frontend/src/lib/graphql.js`:
```javascript
export const client = new Client({
  url: 'http://localhost:5000/graphql',
  // ...
});
```

### CORS Configuration
- Edit `backend/NumerologyAPI/Program.cs`:
```csharp
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5173") // Frontend URL
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
```

## Troubleshooting
### Python Command Not Found
- **Error:** `python: command not found`
- **Solution:** Use `python3` instead. Update `PredictionService.cs`:
```csharp
FileName = "python3",  // Change from "python" to "python3"
```

### Model File Not Found
- **Error:** `Model not found. Please train the model first.`

**Solution:**
```bash
cd ML
python3 train_model.py
```

### Port Already in Use
- **Error:** `Address already in use`
- **Solution:** Change the port in `appsettings.json` or kill the process:
```bash
# Find the process
lsof -i :5000
# Kill it
kill -9 <PID>
```

### CORS Errors
- **Error:** `Access to fetch at 'http://localhost:5000/graphql' from origin 'http://localhost:5173' has been blocked by CORS policy`
- **Solution:** Verify CORS configuration in `Program.cs` matches your frontend URL.

### Database Locked Error
- **Error:** `SQLite Error: database is locked`
- **Solution:** Close any database connections and restart the backend:
```bash
rm backend/NumerologyAPI/numerology.db
dotnet run --urls "http://localhost:5000"
```

### Low Prediction Diversity
- **Problem:** Model predicts mostly the same numbers
- **Solution:** Add more varied training data to `train_data.csv` and retrain:
```bash
cd ML
python3 train_model.py
```

## Development
### Adding New Training Data
- 1. Edit `ML/train_data.csv`
- 2. Add rows in format: `number,keywords`
- 3. Retrain: `python3 train_model.py`
- 4. Restart backend

### Modifying the UI
- Frontend files are in `frontend/src/`:
  - `App.svelte` - Main layout and navigation
  - `routes/Predict.svelte` - Prediction interface
  - `routes/History.svelte` - History display
  - `app.css` - Global styles

- After changes, Vite will hot-reload automatically.

### Database Management
- The SQLite database is created automatically at: backend/NumerologyAPI/numerology.db
- To reset the database:
```bash
rm backend/NumerologyAPI/numerology.db
dotnet run
```

## Building for Production
### Frontend
```bash
cd frontend
npm run build
```

- Output will be in `frontend/dist/`

### Backend
```bash
cd backend/NumerologyAPI
dotnet publish -c Release -o ./publish
```

- Output will be in `backend/NumerologyAPI/publish/`

## Acknowledgments
- **Svelte** - Frontend framework
- **ASP.NET Core** - Backend framework
- **HotChocolate** - GraphQL implementation
- **scikit-learn** - Machine learning library
- **URQL** - GraphQL client