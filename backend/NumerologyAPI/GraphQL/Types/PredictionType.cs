using NumerologyAPI.Models;

namespace NumerologyAPI.GraphQL.Types;

public class PredictionType
{
    public int Id { get; set; }
    public string InputText { get; set; } = string.Empty;
    public int PredictedNumber { get; set; }
    public DateTime Timestamp { get; set; }
}