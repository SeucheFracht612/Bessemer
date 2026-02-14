// See https://aka.ms/new-console-template for more information

using Bessemer.Core.Models;
using Bessemer.Core.Services;

YamlParser parser = new YamlParser();
GraphMapper mapper = new GraphMapper();

var ChainDefinition = parser.CreateChainDefinition();
var map = mapper.Mapper(new ProductionGraph(),ChainDefinition,ChainDefinition.Recipes[1]);

foreach (var x in map._ProductionGraph)
{
    Console.WriteLine(x.Key.Name + " depends on:");
    foreach (var dep in x.Value)
    {
        Console.WriteLine($"  {dep.Item1.Name} from {dep.Item2?.Name ?? "RAW"}");
    }
}

Console.WriteLine(map._ProductionGraph);

Console.WriteLine(parser.CreateChainDefinition().Name);
foreach (var building in parser.CreateChainDefinition().Buildings)
{
    Console.WriteLine(building.Id); 
}

