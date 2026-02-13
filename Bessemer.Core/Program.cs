// See https://aka.ms/new-console-template for more information

using Bessemer.Core.Models;
using Bessemer.Core.Services;

YamlParser parser = new YamlParser();

Console.WriteLine(parser.CreateChainDefinition().Name);
foreach (var building in parser.CreateChainDefinition().Buildings)
{
    Console.WriteLine(building.Id); 
}