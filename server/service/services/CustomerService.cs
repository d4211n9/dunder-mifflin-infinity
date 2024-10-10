using data_access.data_transfer_objects;
using data_access.interfaces;
using data_access.models;
using service.interfaces;

namespace service.services;

public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
{
    public SelectionWithPaginationDto<Customer> GetCustomers(CustomerSearchDto customerSearchDto)
    {
        return customerRepository.GetCustomers(customerSearchDto);
    }
}