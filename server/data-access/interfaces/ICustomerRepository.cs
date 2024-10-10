using data_access.data_transfer_objects;
using data_access.models;

namespace data_access.interfaces;

public interface ICustomerRepository
{
    SelectionWithPaginationDto<Customer> GetCustomers(CustomerSearchDto customerSearchDto);
}