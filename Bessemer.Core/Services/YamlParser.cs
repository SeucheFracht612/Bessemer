using Bessemer.Core.Dto;
using Bessemer.Core.Models;

namespace Bessemer.Core.Services;

using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

public class YamlParser
{
    // Will read YAML file based on CLI Input in the future (hopefully)
    public String getYamlFormat()
    {
        return """
               name: factorio-basics

               resources:
                 - id: iron_ore
                   name: Iron Ore
                 - id: coal
                   name: Coal
                 - id: iron_ingot
                   name: Iron Ingot
                 - id: iron_plate
                   name: Iron Plate

               buildings:
                 - id: furnace
                   name: Stone Furnace
                 - id: press
                   name: Iron Press

               recipes:
                 - id: smelt_iron
                   name: Smelt Iron
                   building: furnace
                   craft_time: 5.0
                   inputs:
                     iron_ore: 2
                     coal: 1
                   outputs:
                     iron_ingot: 1

                 - id: press_plates
                   name: Press Plates
                   building: press
                   craft_time: 8.0
                   inputs:
                     iron_ingot: 3
                   outputs:
                     iron_plate: 2
               """;
    }

    // Parses the YAML String provided by getYamlFormat and creates a new ChainDefinition Object to be worked with in later Steps
    public ChainDefinition CreateChainDefinition()
    {
        String yaml = File.ReadAllText("C:\\Users\\jaron\\AppData\\Roaming\\JetBrains\\Rider2025.3\\scratches\\factorio-basics.yaml");
        List<Recipe> recipes = new List<Recipe>();
        Dictionary<String, Resource> resources = new Dictionary<String, Resource>();
        Dictionary<String, Building> buildings = new Dictionary<String, Building>();
        
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(UnderscoredNamingConvention.Instance)
            .Build();
      
        var dto = deserializer.Deserialize<ChainDefinitionDto>(yaml);

        foreach (var b in dto.Buildings)
        {
          buildings.Add(b.Id, new Building(b.Id,b.Name));
        }

        foreach (var r in dto.Resources)
        {
          resources.Add(r.Id,new Resource(r.Id,r.Name));
        }

        foreach (var re in dto.Recipes)
        {
          Recipe recipe = new Recipe();
          
          recipe.Id = re.Id;
          recipe.Name = re.Name;
          recipe.Building = buildings[re.Building];
          recipe.CraftTime = re.CraftTime;

          foreach (var a in re.Inputs)
          {
            recipe.Inputs.Add(resources[a.Key],a.Value);
          }
          foreach (var b in re.Outputs)
          {
            recipe.Outputs.Add(resources[b.Key],b.Value);
          }
          
          recipes.Add(recipe);
        }
        
        return new ChainDefinition{
          Name = dto.Name,
          Resources = resources.Values.ToList(),
          Buildings = buildings.Values.ToList(),
          Recipes = recipes
        };
    }
    
}