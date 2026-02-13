namespace Bessemer.Core.Models;

public class ChainDefinition
{

    public ChainDefinition() { }

    public ChainDefinition(String Name, List<Building> Buildings,List<Recipe> Recipes,List<Resource> Resources)
    {
        this.Name = Name;
        this.Buildings = Buildings;
        this.Recipes = Recipes;
        this.Resources = Resources;
    }

    public String Name { get; set; }
    public List<Building> Buildings { get; set; } = new();
    public List<Recipe> Recipes { get; set; } = new();
    public List<Resource> Resources { get; set; } = new();
}