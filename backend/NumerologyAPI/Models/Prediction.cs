namespace NumerologyAPI.Models;

public class Prediction
{
    public int Id { get; set; }
    public string InputText { get; set; } = string.Empty;
    public int PredictedNumber { get; set; }
    public DateTime Timestamp { get; set; }
}