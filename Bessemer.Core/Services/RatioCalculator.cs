using Bessemer.Core.Models;

namespace Bessemer.Core.Services;

public class RatioCalculator
{
    public CalculatedRates Calculate(double targetAmount, Resource targetResource, ProductionGraph productionGraph)
    {
        var calculatedRates = new CalculatedRates();
        
        calculatedRates.RequestedRate = targetAmount;
        calculatedRates.RequestedResource = targetResource;
        
        RecursiveCalculate(calculatedRates,targetAmount, targetResource, productionGraph);
        SmoothRates(calculatedRates);
        
        return calculatedRates;
    }

    public void RecursiveCalculate(CalculatedRates calculatedRates, double targetAmount, Resource targetResource, ProductionGraph productionGraph)
    {
        double neededPerSecond = targetAmount / 60;
        double productionPerSecond = 0;
        double buildingsPerSecond = 0;
        
        foreach (var entry in productionGraph._ProductionGraph)
        {
            Recipe recipe = entry.Key;
            if (recipe.Outputs.ContainsKey(targetResource))
            {
                productionPerSecond = recipe.Outputs[targetResource] / recipe.CraftTime;
                buildingsPerSecond = neededPerSecond / productionPerSecond;
                ProductionStep productionStep = new ProductionStep(recipe, buildingsPerSecond, (int)Math.Ceiling(buildingsPerSecond));
                calculatedRates.ProductionSteps.Add(productionStep);
                
                foreach (var output in recipe.Outputs)
                {
                    productionStep.OutputPerSecondRaw.Add(output.Key, (output.Value / recipe.CraftTime) * buildingsPerSecond);
                }
                
                foreach (var en in entry.Value)
                {
                    if (en.Item2 != null)
                    {
                        targetAmount = buildingsPerSecond * (recipe.Inputs[en.Item1] / recipe.CraftTime) * 60;
                        productionStep.InputPerSecondRaw.Add(en.Item1,targetAmount / 60);
                        RecursiveCalculate(calculatedRates, targetAmount,en.Item1, productionGraph);
                    }

                    if (en.Item2 == null)
                    {
                        double consumption = buildingsPerSecond * (recipe.Inputs[en.Item1] / recipe.CraftTime);
                        productionStep.InputPerSecondRaw.Add(en.Item1, consumption);
                        calculatedRates.RawResourcesPerSecond.Add(en.Item1, consumption * 60);
                    }
                }
            }
        }
    }

    public void SmoothRates(CalculatedRates rates)
    {
        for (int i = 0; i < rates.ProductionSteps.Count; i++)
        {
            var step = rates.ProductionSteps[i];
            int practicalCount = step.BuildingCountPractical;
            
            foreach (var output in step.Recipe.Outputs)
            {
                double actualOutput = (output.Value / step.Recipe.CraftTime) * practicalCount;
                step.OutputPerSecondPractical[output.Key] = actualOutput;
                
                double rawOutput = step.OutputPerSecondRaw.ContainsKey(output.Key)
                    ? step.OutputPerSecondRaw[output.Key]
                    : 0;
                step.SurplusPerSecond[output.Key] = actualOutput - rawOutput;
            }
            
            foreach (var input in step.Recipe.Inputs)
            {
                double actualInput = (input.Value / step.Recipe.CraftTime) * practicalCount;
                step.InputPerSecondPractical[input.Key] = actualInput;
                
                for (int j = i + 1; j < rates.ProductionSteps.Count; j++)
                {
                    var downstream = rates.ProductionSteps[j];
                    if (downstream.Recipe.Outputs.ContainsKey(input.Key))
                    {
                        double outputPerBuilding = downstream.Recipe.Outputs[input.Key] / downstream.Recipe.CraftTime;
                        int neededBuildings = (int)Math.Ceiling(actualInput / outputPerBuilding);
                        if (neededBuildings > downstream.BuildingCountPractical)
                        {
                            downstream.BuildingCountPractical = neededBuildings;
                        }
                    }
                }
            }
        }
        rates.RawResourcesPerSecond.Clear();
        foreach (var step in rates.ProductionSteps)
        {
            foreach (var input in step.Recipe.Inputs)
            {
                // If no other step produces this input, it's raw
                bool isRaw = !rates.ProductionSteps.Any(s => s.Recipe.Outputs.ContainsKey(input.Key));
                if (isRaw)
                {
                    double actualConsumption = (input.Value / step.Recipe.CraftTime) * step.BuildingCountPractical * 60;
                    if (rates.RawResourcesPerSecond.ContainsKey(input.Key))
                        rates.RawResourcesPerSecond[input.Key] += actualConsumption;
                    else
                        rates.RawResourcesPerSecond.Add(input.Key, actualConsumption);
                }
            }
        }
    }
}