using data_access.data_transfer_objects;
using data_access.interfaces;
using data_access.models;

namespace data_access.repositories;

public class CustomerRepository : ICustomerRepository
{
    public IEnumerable<Customer> GetCustomers(CustomerSearchDto customerSearchDto)
    {
        throw new NotImplementedException();
    }
}