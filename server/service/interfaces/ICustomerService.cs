using data_access.data_transfer_objects;
using data_access.models;

namespace service.interfaces;

public interface ICustomerService
{
    SelectionWithPaginationDto<Customer> GetCustomers(CustomerSearchDto customerSearchDto);
}