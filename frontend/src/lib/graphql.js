import { Client, cacheExchange, fetchExchange } from '@urql/svelte';

export const client = new Client({
  url: 'http://localhost:5000/graphql',
  exchanges: [cacheExchange, fetchExchange],
});

export const ADD_PREDICTION = `
  mutation AddPrediction($inputText: String!) {
    addPrediction(inputText: $inputText) {
      id
      inputText
      predictedNumber
      timestamp
    }
  }
`;

export const GET_PREDICTIONS = `
  query GetPredictions {
    predictions {
      id
      inputText
      predictedNumber
      timestamp
    }
  }
`;