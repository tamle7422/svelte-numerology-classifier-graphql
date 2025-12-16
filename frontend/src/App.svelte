<script>
  import { setContextClient } from '@urql/svelte';
  import { client } from './lib/graphql';
  import Predict from './routes/Predict.svelte';
  import History from './routes/History.svelte';
  
  setContextClient(client);
  
  let currentPage = 'predict';
</script>

<main>
  <nav>
    <button 
      class:active={currentPage === 'predict'}
      on:click={() => currentPage = 'predict'}
    >
      Predict
    </button>
    <button 
      class:active={currentPage === 'history'}
      on:click={() => currentPage = 'history'}
    >
      History
    </button>
  </nav>
  
  {#if currentPage === 'predict'}
    <Predict />
  {:else}
    <History />
  {/if}
</main>

<style>
  :global(body) {
    margin: 0;
    padding: 0;
    font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Oxygen,
      Ubuntu, Cantarell, 'Open Sans', 'Helvetica Neue', sans-serif;
    background-color: #fafafa;
  }
  
  main {
    min-height: 100vh;
  }
  
  nav {
    background-color: #2c3e50;
    padding: 1rem;
    display: flex;
    gap: 1rem;
    box-shadow: 0 2px 4px rgba(0,0,0,0.1);
  }
  
  nav button {
    background-color: transparent;
    color: white;
    border: none;
    padding: 0.75rem 1.5rem;
    font-size: 1rem;
    cursor: pointer;
    border-radius: 4px;
    transition: background-color 0.3s;
  }
  
  nav button:hover {
    background-color: rgba(255,255,255,0.1);
  }
  
  nav button.active {
    background-color: #4CAF50;
  }
</style>