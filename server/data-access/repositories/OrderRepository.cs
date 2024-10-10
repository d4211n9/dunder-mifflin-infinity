using data_access.data_transfer_objects;
using data_access.interfaces;
using data_access.models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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

    public SelectionWithPaginationDto<Order> GetOrdersForCustomer(GetCustomerOrdersDto getCustomerOrdersDto)
    {
        IEnumerable<Order> ordersOfCustomer = myDbContext.Orders
            .Where(order => order.CustomerId == getCustomerOrdersDto.CustomerId);

        return new SelectionWithPaginationDto<Order>
        {
            Selection = ordersOfCustomer
                .Skip((getCustomerOrdersDto.PaginationDto.PageNumber - 1) * getCustomerOrdersDto.PaginationDto.PageSize)
                .Take(getCustomerOrdersDto.PaginationDto.PageSize),
            TotalPages = (int) Math.Ceiling((double) ordersOfCustomer.Count() / getCustomerOrdersDto.PaginationDto.PageSize)
        };
    }

    public SelectionWithPaginationDto<Order> GetCustomerOrders(CustomerOrdersSearchDto customerOrdersSearchDto)
    {
        IEnumerable<Order> filteredOrders = myDbContext.Orders
            .Where(order => order.TotalAmount <= customerOrdersSearchDto.MaxAmount &&
                            order.TotalAmount >= customerOrdersSearchDto.MinAmount &&
                            order.DeliveryDate <= customerOrdersSearchDto.DeliveryTimeFrameDto.DeliveryUntil &&
                            order.DeliveryDate >= customerOrdersSearchDto.DeliveryTimeFrameDto.DeliverySince &&
                            order.OrderDate <= customerOrdersSearchDto.OrderTimeFrameDto.OrderTimeUntil &&
                            order.OrderDate >= customerOrdersSearchDto.OrderTimeFrameDto.OrderTimeSince &&
                            order.Status.Equals(customerOrdersSearchDto.OrderStatus));

        return new SelectionWithPaginationDto<Order>
        {
            Selection = filteredOrders
                .Skip((customerOrdersSearchDto.PaginationDto.PageNumber - 1) * customerOrdersSearchDto.PaginationDto.PageSize)
                .Take(customerOrdersSearchDto.PaginationDto.PageSize),
            TotalPages = (int) Math.Ceiling((double) filteredOrders.Count() / customerOrdersSearchDto.PaginationDto.PageSize)
        };
    }

    public async Task<Order> CreateOrder(CreateOrderDto createOrderDto)
    {
        EntityEntry<Order> createdOrder = await myDbContext.Orders.AddAsync(new Order
        {
            OrderDate = createOrderDto.OrderDate,
            DeliveryDate = createOrderDto.DeliveryDate,
            Status = createOrderDto.Status,
            TotalAmount = createOrderDto.TotalAmount,
            CustomerId = createOrderDto.CustomerId,
        });

        await myDbContext.SaveChangesAsync();

        return createdOrder.Entity;
    }

    public async Task<bool> ChangeTotalAmount(ChangeOrderTotalAmountDto changeOrderTotalAmountDto)
    {
        Order orderToChangeTotalAmountOf = 
            myDbContext.Orders.FirstOrDefault(order => order.Id == changeOrderTotalAmountDto.OrderId);

        Order updatedOrder = new Order
        {
            Id = orderToChangeTotalAmountOf.Id,
            OrderDate = orderToChangeTotalAmountOf.OrderDate,
            DeliveryDate = orderToChangeTotalAmountOf.DeliveryDate,
            Status = orderToChangeTotalAmountOf.Status,
            TotalAmount = changeOrderTotalAmountDto.UpdatedTotalAmount,
            CustomerId = orderToChangeTotalAmountOf.CustomerId
        };

        myDbContext.Entry(orderToChangeTotalAmountOf).CurrentValues.SetValues(updatedOrder);

        return (await myDbContext.SaveChangesAsync()) > 0;
    }
}