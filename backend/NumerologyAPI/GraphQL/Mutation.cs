using NumerologyAPI.Data;
using NumerologyAPI.Models;
using NumerologyAPI.Services;

namespace NumerologyAPI.GraphQL;

public class Mutation
{
    public async Task<Prediction> AddPrediction(
        string inputText,
        [Service] AppDbContext context,
        [Service] PredictionService predictionService,
        [Service] ILogger<Mutation> logger)
    {
        try
        {
            logger.LogInformation("Starting prediction for input: {InputText}", inputText);
            
            var predictedNumber = await predictionService.PredictAsync(inputText);
            
            logger.LogInformation("Prediction result: {Number}", predictedNumber);
            
            var prediction = new Prediction
            {
                InputText = inputText,
                PredictedNumber = predictedNumber,
                Timestamp = DateTime.UtcNow
            };
            
            context.Predictions.Add(prediction);
            await context.SaveChangesAsync();
            
            logger.LogInformation("Prediction saved to database with ID: {Id}", prediction.Id);
            
            return prediction;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in AddPrediction: {Message}", ex.Message);
            throw;
        }
    }
}