namespace Bessemer.Core.Models;

public class Recipe
{

    public Recipe()
    {
        
    }
    
    public Recipe(string id, string name, double craftTime, Building building, Dictionary<Resource, double> inputs, Dictionary<Resource, double> outputs)
    {
        Id = id;
        Name = name;
        CraftTime = craftTime;
        Building = building;
        Inputs = inputs;
        Outputs = outputs;
    }

    public string Id { get; set; }
    public string Name { get; set; }
    public double CraftTime { get; set; }
    public Building Building { get; set; }
    public Dictionary<Resource, double> Inputs { get; set; } = new();
    public Dictionary<Resource, double> Outputs { get;set; } = new();
}