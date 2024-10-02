using data_access.data_transfer_objects;
using data_access.models;

namespace data_access.interfaces;

public interface IOrderRepository
{
    Task<bool> ChangeStatus(ChangeOrderStatusDto changeOrderStatusDto);
    SelectionWithPaginationDto<Order> GetOrdersForCustomer(GetCustomerOrdersDto getCustomerOrdersDto);
    IEnumerable<Order> GetCustomerOrders(CustomerOrdersSearchDto customerOrdersSearchDto);
    Order CreateOrder(CreateOrderDto createOrderDto);
}