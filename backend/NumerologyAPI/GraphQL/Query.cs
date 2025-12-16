using NumerologyAPI.Data;
using NumerologyAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace NumerologyAPI.GraphQL;

public class Query
{
    public async Task<List<Prediction>> GetPredictions([Service] AppDbContext context)
    {
        return await context.Predictions
            .OrderByDescending(p => p.Timestamp)
            .ToListAsync();
    }
    
    public async Task<Prediction?> GetPrediction(int id, [Service] AppDbContext context)
    {
        return await context.Predictions.FindAsync(id);
    }
}