using data_access.data_transfer_objects;
using data_access.interfaces;
using data_access.models;

namespace data_access.repositories;

public class OrderRepository : IOrderRepository
{
    public void ChangeStatus(ChangeOrderStatusDto changeOrderStatusDto)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Order> GetOrdersForCustomer(GetCustomerOrdersDto getCustomerOrdersDto)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Order> GetCustomerOrders(CustomerOrdersSearchDto customerOrdersSearchDto)
    {
        throw new NotImplementedException();
    }

    public Order CreateOrder(CreateOrderDto createOrderDto)
    {
        throw new NotImplementedException();
    }
}