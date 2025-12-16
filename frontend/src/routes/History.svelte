<script>
  import { getContextClient } from '@urql/svelte';
  import { GET_PREDICTIONS } from '../lib/graphql';
  
  const client = getContextClient();
  
  let predictions = [];
  let loading = true;
  let error = null;
  
  async function loadPredictions() {
    loading = true;
    error = null;
    
    try {
      const response = await client.query(GET_PREDICTIONS, {}).toPromise();
      
      if (response.error) {
        error = response.error.message;
      } else {
        predictions = response.data?.predictions || [];
      }
    } catch (e) {
      error = e.message;
    } finally {
      loading = false;
    }
  }
  
  function refresh() {
    loadPredictions();
  }
  
  // Load predictions on mount
  loadPredictions();
</script>

<div class="container">
  <div class="header">
    <h1>Prediction History</h1>
    <button on:click={refresh}>Refresh</button>
  </div>
  
  {#if loading}
    <p class="loading">Loading history...</p>
  {:else if error}
    <div class="error">
      <strong>Error:</strong> {error}
    </div>
  {:else if predictions.length === 0}
    <p class="empty">No predictions yet. Go to the Predict page to make your first prediction!</p>
  {:else}
    <div class="predictions-list">
      {#each predictions as prediction}
        <div class="prediction-card">
          <div class="prediction-number">{prediction.predictedNumber}</div>
          <div class="prediction-details">
            <p class="prediction-text">{prediction.inputText}</p>
            <p class="prediction-date">
              {new Date(prediction.timestamp).toLocaleString()}
            </p>
          </div>
        </div>
      {/each}
    </div>
  {/if}
</div>

<style>
  .container {
    max-width: 800px;
    margin: 0 auto;
    padding: 2rem;
  }
  
  .header {
    display: flex;
    justify-content: space-between;
    align-items: center;
    margin-bottom: 2rem;
  }
  
  h1 {
    color: #333;
    margin: 0;
  }
  
  button {
    background-color: #4CAF50;
    color: white;
    padding: 0.5rem 1rem;
    border: none;
    border-radius: 4px;
    font-size: 0.9rem;
    cursor: pointer;
    transition: background-color 0.3s;
  }
  
  button:hover {
    background-color: #45a049;
  }
  
  .loading {
    text-align: center;
    color: #666;
    padding: 2rem;
  }
  
  .error {
    padding: 1rem;
    background-color: #ffebee;
    border-left: 4px solid #f44336;
    color: #c62828;
  }
  
  .empty {
    text-align: center;
    color: #666;
    padding: 3rem 2rem;
    background-color: #f5f5f5;
    border-radius: 8px;
  }
  
  .predictions-list {
    display: flex;
    flex-direction: column;
    gap: 1rem;
  }
  
  .prediction-card {
    display: flex;
    align-items: center;
    padding: 1.5rem;
    background-color: white;
    border: 1px solid #ddd;
    border-radius: 8px;
    box-shadow: 0 2px 4px rgba(0,0,0,0.1);
    transition: box-shadow 0.3s;
  }
  
  .prediction-card:hover {
    box-shadow: 0 4px 8px rgba(0,0,0,0.15);
  }
  
  .prediction-number {
    font-size: 3rem;
    font-weight: bold;
    color: #4CAF50;
    min-width: 100px;
    text-align: center;
    margin-right: 2rem;
  }
  
  .prediction-details {
    flex: 1;
  }
  
  .prediction-text {
    color: #333;
    font-size: 1.1rem;
    margin: 0 0 0.5rem;
  }
  
  .prediction-date {
    color: #999;
    font-size: 0.9rem;
    margin: 0;
  }
</style>