using System.Diagnostics;

namespace NumerologyAPI.Services;

public class PredictionService
{
    private readonly string _pythonScriptPath;
    private readonly string _workingDirectory;
    private readonly ILogger<PredictionService> _logger;
    
    public PredictionService(IWebHostEnvironment env, ILogger<PredictionService> logger)
    {
        _logger = logger;
        
        // Get the solution root directory (grandparent of NumerologyAPI folder)
        var apiDirectory = env.ContentRootPath; // .../backend/NumerologyAPI
        var backendDirectory = Directory.GetParent(apiDirectory)?.FullName; // .../backend
        var rootDirectory = Directory.GetParent(backendDirectory ?? apiDirectory)?.FullName; // .../
        var mlDirectory = System.IO.Path.Combine(rootDirectory ?? backendDirectory ?? apiDirectory, "ML");
        
        _pythonScriptPath = System.IO.Path.Combine(mlDirectory, "predict.py");
        _workingDirectory = mlDirectory;
        
        _logger.LogInformation("Python script path: {Path}", _pythonScriptPath);
        _logger.LogInformation("Working directory: {Dir}", _workingDirectory);
        
        if (!File.Exists(_pythonScriptPath))
        {
            _logger.LogError("Python script not found at: {Path}", _pythonScriptPath);
        }
    }
    
    public async Task<int> PredictAsync(string inputText)
    {
        if (!File.Exists(_pythonScriptPath))
        {
            throw new FileNotFoundException($"Python script not found at: {_pythonScriptPath}");
        }
        
        var start = new ProcessStartInfo
        {
            FileName = "python3",
            Arguments = $"\"{_pythonScriptPath}\" \"{inputText}\"",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true,
            WorkingDirectory = _workingDirectory
        };
        
        _logger.LogInformation("Executing: {FileName} {Args}", start.FileName, start.Arguments);
        
        using var process = Process.Start(start);
        if (process == null)
            throw new Exception("Failed to start Python process");
            
        var output = await process.StandardOutput.ReadToEndAsync();
        var error = await process.StandardError.ReadToEndAsync();
        await process.WaitForExitAsync();
        
        _logger.LogInformation("Python output: {Output}", output);
        if (!string.IsNullOrEmpty(error))
        {
            _logger.LogWarning("Python stderr: {Error}", error);
        }
        
        if (process.ExitCode != 0)
            throw new Exception($"Python script error: {error}");
            
        if (int.TryParse(output.Trim(), out int result))
            return result;
            
        throw new Exception($"Invalid prediction result: {output}");
    }
}