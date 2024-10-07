using data_access.models;

namespace data_access.data_transfer_objects;

public class PropertyDto
{
    public int Id { get; set; }
    public string PropertyName { get; set; } = null!;
    
    public PropertyDto() {}

    public PropertyDto(Property property)
    {
        Id = property.Id;
        PropertyName = property.PropertyName;
    }
}