namespace Bessemer.Core.Models;

public class CalculatedRates
{
    public CalculatedRates() { }
    
    public CalculatedRates(Resource requestedResource, double requestedRate)
    {
        RequestedResource = requestedResource;
        RequestedRate = requestedRate;
    }

    public Resource RequestedResource { get; set; }
    public double RequestedRate { get; set; }
    
    public List<ProductionStep> ProductionSteps { get; set; } = new();
    public Dictionary<Resource, double> RawResourcesPerSecond { get; set; } = new();
}