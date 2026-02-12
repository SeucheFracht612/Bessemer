namespace Bessemer.Core.Models;

public class Recipe
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Dictionary<Resource, double> Inputs { get; set; }
    public Dictionary<Resource,int> Outputs { get;set; }
    public double CraftingTime { get; set; }
    public Building Building { get; set; }
}