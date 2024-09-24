using data_access.models;

namespace data_access.interfaces;

public interface IPaperPropertiesRepository
{
    Property CreateProperty(string propertyName);
}