using data_access.models;

namespace data_access.data_transfer_objects;

public class PaperDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public bool Discontinued { get; set; }
    public int Stock { get; set; }
    public double Price { get; set; }
    public IEnumerable<PropertyDto> Properties { get; set; } = new List<PropertyDto>();
    
    public PaperDto() {}

    public PaperDto(Paper paper)
    {
        Id = paper.Id;
        Name = paper.Name;
        Discontinued = paper.Discontinued;
        Stock = paper.Stock;
        Price = paper.Price;
        Properties = ConvertProperties(paper.Properties);
    }

    private IEnumerable<PropertyDto> ConvertProperties(IEnumerable<Property> properties)
    {
        var propertyDtos = new List<PropertyDto>();
        
        foreach (var property in properties)
        {
            propertyDtos.Add(new PropertyDto(property));
        }

        return propertyDtos;
    }
}