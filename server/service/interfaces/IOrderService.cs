using data_access.data_transfer_objects;
using data_access.models;

namespace service.interfaces;

public interface IOrderService
{
    Task<bool> ChangeStatus(ChangeOrderStatusDto changeOrderStatusDto);
    SelectionWithPaginationDto<Order> GetOrdersForCustomer(GetCustomerOrdersDto getCustomerOrdersDto);
    SelectionWithPaginationDto<Order> GetCustomerOrders(CustomerOrdersSearchDto customerOrdersSearchDto);
    Task<OrderDto> CreateOrder(CreateOrderWithOrderEntriesDto createOrderWithOrderEntriesDto);
}