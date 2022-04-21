using System.Diagnostics;

var memoryMetrics = GetUnixMetrics();
Console.WriteLine(memoryMetrics.Free);

MemoryMetrics GetUnixMetrics()
{
    var info = new ProcessStartInfo("free -m")
    {
        FileName = "/bin/bash",
        Arguments = "-c \"free -m\"",
        RedirectStandardOutput = true
    };

    using var process = Process.Start(info);

    var output = process?.StandardOutput.ReadToEnd() ?? string.Empty;

    var lines = output.Split("\n");
    var memory = lines[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);

    var metrics = new MemoryMetrics
    {
        Total = double.Parse(memory[1]),
        Used = double.Parse(memory[2]),
        Free = double.Parse(memory[3])
    };

    return metrics;
}

public class MemoryMetrics
{
    public double Total;
    public double Used;
    public double Free;
}