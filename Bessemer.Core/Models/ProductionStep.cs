namespace Bessemer.Core.Models;

public class ProductionStep
{
    public ProductionStep(Recipe recipe, double buildingCountRaw, int buildingCountPractical)
    {
        Recipe = recipe;
        BuildingCountRaw = buildingCountRaw;
        BuildingCountPractical = buildingCountPractical;
    }
    
    public Recipe Recipe { get; set; }
    public double BuildingCountRaw { get; set; }
    public int BuildingCountPractical { get; set; }
    
    public Dictionary<Resource,double> OutputPerSecondRaw { get; set; } = new();
    public Dictionary<Resource,double> InputPerSecondRaw { get; set; } = new();
    public Dictionary<Resource,double> OutputPerSecondPractical { get; set; } = new();
    public Dictionary<Resource,double> InputPerSecondPractical { get; set; } = new();
    public Dictionary<Resource, double> SurplusPerSecond { get; set; } = new();
}