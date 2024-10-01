using data_access.data_transfer_objects;
using data_access.models;

namespace service.interfaces;

public interface IOrderService
{
    Task<bool> ChangeStatus(ChangeOrderStatusDto changeOrderStatusDto);
    IEnumerable<Order> GetOrdersForCustomer(GetCustomerOrdersDto getCustomerOrdersDto);
    IEnumerable<Order> GetCustomerOrders(CustomerOrdersSearchDto customerOrdersSearchDto);
    Order CreateOrder(CreateOrderDto createOrderDto);
}