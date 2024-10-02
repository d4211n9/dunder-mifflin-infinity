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

    public SelectionWithPaginationDto<Order> GetOrdersForCustomer(GetCustomerOrdersDto getCustomerOrdersDto)
    {
        return orderRepository.GetOrdersForCustomer(getCustomerOrdersDto);
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