using data_access.data_transfer_objects;
using data_access.interfaces;
using data_access.models;
using service.interfaces;

namespace service.services;

public class OrderService(IOrderRepository orderRepository) : IOrderService
{
    public async Task<bool> ChangeStatus(ChangeOrderStatusDto changeOrderStatusDto)
    {
        return await orderRepository.ChangeStatus(changeOrderStatusDto);
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