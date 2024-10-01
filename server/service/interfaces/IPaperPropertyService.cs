using data_access.models;

namespace service.interfaces;

public interface IPaperPropertyService
{
    Property CreateProperty(string propertyName);
}