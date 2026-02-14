using Bessemer.Core.Models;

namespace Bessemer.Core.Services;

public class GraphMapper
{

    public ProductionGraph Mapper(ProductionGraph initialGraph, ChainDefinition chainDefinition, Recipe recipe)
    {
        var productionGraph = new ProductionGraph();
        
        RecusionMapper(chainDefinition, recipe, productionGraph);

        return productionGraph;

    }
    
    public void RecusionMapper(ChainDefinition chainDefinition, Recipe initialRecipe, ProductionGraph productionGraph)
    {
        var dependencies = new List<(Resource, Recipe?)>();
        foreach (var inputs in initialRecipe.Inputs)
        {
            bool found = false;
            foreach (var recipes in chainDefinition.Recipes)
            {
                foreach (var output in recipes.Outputs)
                {
                    if (inputs.Key.Equals(output.Key))
                    {
                        dependencies.Add((inputs.Key, recipes));
                        if (!productionGraph._ProductionGraph.ContainsKey(recipes))
                        {
                            RecusionMapper(chainDefinition, recipes, productionGraph);
                        }
                        found = true;
                    }
                }
            }
            
            if (!found)
            {
                dependencies.Add((inputs.Key, null));
            }
        }
        productionGraph._ProductionGraph.Add(initialRecipe,dependencies);
    }
}