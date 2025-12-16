<script>
  import { getContextClient, gql } from '@urql/svelte';
  import { ADD_PREDICTION } from '../lib/graphql';
  
  const client = getContextClient();
  
  let inputText = '';
  let result = null;
  let loading = false;
  let error = null;
  
  async function handleSubmit() {
    if (!inputText.trim()) return;
    
    loading = true;
    error = null;
    result = null;
    
    try {
      const response = await client.mutation(ADD_PREDICTION, { inputText }).toPromise();
      
      if (response.error) {
        error = response.error.message;
      } else {
        result = response.data.addPrediction;
        inputText = '';
      }
    } catch (e) {
      error = e.message;
    } finally {
      loading = false;
    }
  }
</script>

<div class="container">
  <h1>Numerology Prediction</h1>
  
  <form on:submit|preventDefault={handleSubmit}>
    <div class="form-group">
      <label for="inputText">Enter keywords or text:</label>
      <textarea
        id="inputText"
        bind:value={inputText}
        placeholder="e.g., spiritual wisdom intuition"
        rows="4"
        disabled={loading}
      ></textarea>
    </div>
    
    <button type="submit" disabled={loading || !inputText.trim()}>
      {loading ? 'Predicting...' : 'Predict Number'}
    </button>
  </form>
  
  {#if error}
    <div class="error">
      <strong>Error:</strong> {error}
    </div>
  {/if}
  
  {#if result}
    <div class="result">
      <h2>Prediction Result</h2>
      <div class="number-display">{result.predictedNumber}</div>
      <p class="input-text">Input: {result.inputText}</p>
      <p class="timestamp">
        Predicted at: {new Date(result.timestamp).toLocaleString()}
      </p>
    </div>
  {/if}
</div>

<style>
  .container {
    max-width: 600px;
    margin: 0 auto;
    padding: 2rem;
  }
  
  h1 {
    color: #333;
    margin-bottom: 2rem;
  }
  
  .form-group {
    margin-bottom: 1.5rem;
  }
  
  label {
    display: block;
    margin-bottom: 0.5rem;
    font-weight: 600;
    color: #555;
  }
  
  textarea {
    width: 100%;
    padding: 0.75rem;
    border: 2px solid #ddd;
    border-radius: 4px;
    font-size: 1rem;
    font-family: inherit;
    resize: vertical;
  }
  
  textarea:focus {
    outline: none;
    border-color: #4CAF50;
  }
  
  button {
    background-color: #4CAF50;
    color: white;
    padding: 0.75rem 1.5rem;
    border: none;
    border-radius: 4px;
    font-size: 1rem;
    cursor: pointer;
    transition: background-color 0.3s;
  }
  
  button:hover:not(:disabled) {
    background-color: #45a049;
  }
  
  button:disabled {
    background-color: #ccc;
    cursor: not-allowed;
  }
  
  .error {
    margin-top: 1.5rem;
    padding: 1rem;
    background-color: #ffebee;
    border-left: 4px solid #f44336;
    color: #c62828;
  }
  
  .result {
    margin-top: 2rem;
    padding: 2rem;
    background-color: #f5f5f5;
    border-radius: 8px;
    text-align: center;
  }
  
  .result h2 {
    margin-top: 0;
    color: #333;
  }
  
  .number-display {
    font-size: 4rem;
    font-weight: bold;
    color: #4CAF50;
    margin: 1rem 0;
  }
  
  .input-text {
    color: #666;
    font-style: italic;
    margin: 0.5rem 0;
  }
  
  .timestamp {
    color: #999;
    font-size: 0.9rem;
    margin: 0.5rem 0 0;
  }
</style>