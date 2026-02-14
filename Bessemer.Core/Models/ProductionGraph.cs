namespace Bessemer.Core.Models;

public class ProductionGraph
{
    
    public ProductionGraph() { }
    
    public ProductionGraph(Dictionary<Recipe, List<(Resource, Recipe?)>> productionGraph)
    {
        _ProductionGraph = productionGraph;
    }

    public Dictionary<Recipe, List<(Resource, Recipe?)>> _ProductionGraph { get; set; } = new();
}