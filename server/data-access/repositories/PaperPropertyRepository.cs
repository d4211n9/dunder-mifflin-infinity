using data_access.interfaces;
using data_access.models;

namespace data_access.repositories;

public class PaperPropertyRepository(MyDbContext myDbContext) : IPaperPropertyRepository
{
    public Property CreateProperty(string propertyName)
    {
        Property newProperty = new Property()
        {
            PropertyName = propertyName
        };
        
        Property createdProperty = myDbContext.Properties.Add(newProperty).Entity;
        myDbContext.SaveChanges();

        return createdProperty;
    }
}