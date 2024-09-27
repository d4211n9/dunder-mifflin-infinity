using data_access.data_transfer_objects;
using data_access.interfaces;
using data_access.models;

namespace data_access.repositories;

public class CustomerRepository(MyDbContext myDbContext) : ICustomerRepository
{
    public SelectionWithPaginationDto<Customer> GetCustomers(CustomerSearchDto customerSearchDto)
    {
        string lowerSearchQuery = customerSearchDto.SearchQuery.ToLower();
        
        IEnumerable<Customer> matchingCustomers = myDbContext.Customers
            .Where(customer =>
                customer.Name.ToLower().Contains(lowerSearchQuery) ||
                (customer.Email != null && customer.Email.ToLower().Contains(lowerSearchQuery)) ||
                (customer.Address != null && customer.Address.ToLower().Contains(lowerSearchQuery)) ||
                (customer.Phone != null && customer.Phone.ToLower().Contains(lowerSearchQuery)));

        return new SelectionWithPaginationDto<Customer>
        {
            Selection = matchingCustomers
                .Skip((customerSearchDto.PaginationDto.PageNumber - 1) * customerSearchDto.PaginationDto.PageSize)
                .Take(customerSearchDto.PaginationDto.PageSize),
            TotalPages = matchingCustomers.Count()
        };
    }
}