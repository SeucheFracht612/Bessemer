namespace Bessemer.Core.Models;

public class Resource
{
    public Resource(string id, string name)
    {
        Id = id;
        Name = name;
    }

    public String Id { get; set; }
    public String Name { get; set; }
}