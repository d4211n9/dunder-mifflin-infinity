using data_access.interfaces;
using data_access.models;
using service.interfaces;

namespace service.services;

public class PaperPropertyService(IPaperPropertyRepository paperPropertyRepository) : IPaperPropertyService
{
    public Property CreateProperty(string propertyName)
    {
        return paperPropertyRepository.CreateProperty(propertyName);
    }
}