namespace Bessemer.Core.Models;

public class ChainDefinition
{
    public String Name { get; set; }
    public List<Building> Buildings { get; set; }
    public List<Recipe> Recipes { get; set; }
    public List<Resource> Resources { get; set; }
}