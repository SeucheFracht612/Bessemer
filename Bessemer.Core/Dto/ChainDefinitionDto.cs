namespace Bessemer.Core.Dto;

public class ChainDefinitionDto
{
    public string Name { get; set; }
    public List<ResourceDto> Resources { get; set; }
    public List<BuildingDto> Buildings { get; set; }
    public List<RecipeDto> Recipes { get; set; }
}