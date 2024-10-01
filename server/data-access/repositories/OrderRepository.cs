using data_access.data_transfer_objects;
using data_access.interfaces;
using data_access.models;

namespace data_access.repositories;

public class OrderRepository(MyDbContext myDbContext) : IOrderRepository
{
    public async Task<bool> ChangeStatus(ChangeOrderStatusDto changeOrderStatusDto)
    {
         Order orderToChangeStatusOf = 
            myDbContext.Orders.FirstOrDefault(order => order.Id == changeOrderStatusDto.OrderId);

         Order updatedOrder = new Order
         {
             Id = orderToChangeStatusOf.Id,
             OrderDate = orderToChangeStatusOf.OrderDate,
             DeliveryDate = orderToChangeStatusOf.DeliveryDate,
             Status = orderToChangeStatusOf.Status,
             TotalAmount = orderToChangeStatusOf.TotalAmount,
             CustomerId = orderToChangeStatusOf.CustomerId,
             Customer = orderToChangeStatusOf.Customer,
             OrderEntries = orderToChangeStatusOf.OrderEntries
         };

         updatedOrder.Status = changeOrderStatusDto.UpdatedStatus;

        myDbContext.Entry(orderToChangeStatusOf).CurrentValues.SetValues(updatedOrder);

        return (await myDbContext.SaveChangesAsync()) > 0;
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