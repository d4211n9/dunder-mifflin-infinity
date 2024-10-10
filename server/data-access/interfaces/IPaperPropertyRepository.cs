using data_access.models;

namespace data_access.interfaces;

public interface IPaperPropertyRepository
{
    Property CreateProperty(string propertyName);
}