namespace Bessemer.Core.Dto;

public class RecipeDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Building { get; set; }
    public double CraftTime { get; set; }
    public Dictionary<string, double> Inputs { get; set; }
    public Dictionary<string, double> Outputs { get; set; }
}