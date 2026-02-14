// Program.cs — quick test
using Bessemer.Core.Models;
using Bessemer.Core.Services;

YamlParser parser = new YamlParser();
GraphMapper mapper = new GraphMapper();
RatioCalculator calculator = new RatioCalculator();

var chain = parser.CreateChainDefinition();
var graph = mapper.Mapper(new ProductionGraph(), chain, chain.Recipes[1]);

// "I want 100 iron plates per minute"
var ironPlate = chain.Resources[3]; // iron_plate
var result = calculator.Calculate(100, ironPlate, graph);

Console.WriteLine($"Target: {result.RequestedRate} {result.RequestedResource.Name}/min");
Console.WriteLine();

foreach (var step in result.ProductionSteps)
{
    Console.WriteLine($"{step.Recipe.Name} ({step.Recipe.Building.Name})");
    Console.WriteLine($"  Buildings: {step.BuildingCountRaw:F2} raw → {step.BuildingCountPractical} practical");
    
    Console.WriteLine("  Outputs:");
    foreach (var o in step.OutputPerSecondRaw)
        Console.WriteLine($"    {o.Key.Name}: {o.Value:F4}/sec raw, {(step.OutputPerSecondPractical.ContainsKey(o.Key) ? step.OutputPerSecondPractical[o.Key].ToString("F4") : "?")} smooth");
    
    Console.WriteLine("  Inputs:");
    foreach (var inp in step.InputPerSecondRaw)
        Console.WriteLine($"    {inp.Key.Name}: {inp.Value:F4}/sec raw, {(step.InputPerSecondPractical.ContainsKey(inp.Key) ? step.InputPerSecondPractical[inp.Key].ToString("F4") : "?")} smooth");
    
    if (step.SurplusPerSecond.Count > 0)
    {
        Console.WriteLine("  Surplus:");
        foreach (var s in step.SurplusPerSecond)
            Console.WriteLine($"    {s.Key.Name}: {s.Value:F4}/sec");
    }
    Console.WriteLine();
}

Console.WriteLine("Raw resources needed:");
foreach (var raw in result.RawResourcesPerSecond)
    Console.WriteLine($"  {raw.Key.Name}: {raw.Value:F2}/min");