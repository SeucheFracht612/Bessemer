namespace Bessemer.Core.Models;

public class ProductionGraph
{
    public Dictionary<Recipe, List<(Resource, Recipe?)>> _ProductionGraph { get; set; }
    
}