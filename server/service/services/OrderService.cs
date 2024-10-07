using System.Collections;
using data_access.data_transfer_objects;
using data_access.interfaces;
using data_access.models;
using service.interfaces;

namespace service.services;

public class OrderService(
    IOrderRepository orderRepository, 
    IOrderEntryRepository orderEntryRepository,
    IPaperRepository paperRepository) : IOrderService
{
    public async Task<bool> ChangeStatus(ChangeOrderStatusDto changeOrderStatusDto)
    {
        return await orderRepository.ChangeStatus(changeOrderStatusDto);
    }

    public SelectionWithPaginationDto<Order> GetOrdersForCustomer(GetCustomerOrdersDto getCustomerOrdersDto)
    {
        return orderRepository.GetOrdersForCustomer(getCustomerOrdersDto);
    }

    public SelectionWithPaginationDto<Order> GetCustomerOrders(CustomerOrdersSearchDto customerOrdersSearchDto)
    {
        return orderRepository.GetCustomerOrders(customerOrdersSearchDto);
    }

    public async Task<OrderDto> CreateOrder(CreateOrderWithOrderEntriesDto createOrderWithOrderEntriesDto)
    {
        IEnumerable<OrderEntryWithPriceDto> orderEntryWithPriceDtos =
            await CalculateTotalPriceOfEachOrderEntry(createOrderWithOrderEntriesDto.OrderEntryDtos);
        
        Order createdOrder = 
            await InsertOrderIntoRepository(
                createOrderWithOrderEntriesDto.CustomerId, 
                TotalPriceOfOrderEntries(orderEntryWithPriceDtos));

        IEnumerable<OrderEntry> createdOrderEntries =
            await orderEntryRepository
                .CreateOrderEntries(GetCreateOrderEntriesFrom(createOrderWithOrderEntriesDto.OrderEntryDtos, createdOrder.Id));

        orderEntryWithPriceDtos = UpdateOrderEntryWithPriceDtos(orderEntryWithPriceDtos, createdOrderEntries);
        
        return new OrderDto(createdOrder, orderEntryWithPriceDtos);
    }











































    private IEnumerable<CreateOrderEntryDto> GetCreateOrderEntriesFrom(
        IEnumerable<CreateOrderEntryWithoutOrderIdDto> orderEntries, int orderId)
    {
        var createOrderEntryDtos = new List<CreateOrderEntryDto>();
        
        foreach (var orderEntry in orderEntries)
        {
            createOrderEntryDtos.Add(new CreateOrderEntryDto(orderEntry, orderId));
        }

        return createOrderEntryDtos;
    }
    
    private async Task<IEnumerable<OrderEntryWithPriceDto>> CalculateTotalPriceOfEachOrderEntry(IEnumerable<CreateOrderEntryWithoutOrderIdDto> orderEntries)
    {
        var orderEntryWithPriceDtos = ConvertCreateOrderEntriesWithoutOrderId(orderEntries);
        var orderEntryProductIdAndPrice = new Dictionary<int, double>();

        foreach (var orderEntry in orderEntryWithPriceDtos)
        {
            if (orderEntryProductIdAndPrice.TryGetValue(orderEntry.ProductId, out var orderPrice))
            {
                orderEntry.PriceOfOrderEntry = orderPrice * orderEntry.Quantity;
                continue;
            }

            double price = await paperRepository.GetPriceOfPaperFromPaperId(orderEntry.ProductId);
            
            orderEntryProductIdAndPrice.Add(orderEntry.ProductId, price);
            orderEntry.PriceOfOrderEntry = price * orderEntry.Quantity;
        }

        return orderEntryWithPriceDtos;
    }
    
    private IEnumerable<OrderEntryWithPriceDto> ConvertCreateOrderEntriesWithoutOrderId(IEnumerable<CreateOrderEntryWithoutOrderIdDto> createOrderEntryDtos)
    {
        var orderEntryWithPriceDtos = new List<OrderEntryWithPriceDto>();

        foreach (var orderEntry in createOrderEntryDtos)
        {
            orderEntryWithPriceDtos.Add(new OrderEntryWithPriceDto(orderEntry));
        }

        return orderEntryWithPriceDtos;
    } 
    
    private IEnumerable<OrderEntryWithPriceDto> UpdateOrderEntryWithPriceDtos(IEnumerable<OrderEntryWithPriceDto> orderEntryWithPriceDtos, IEnumerable<OrderEntry> orderEntries)
    {
        var updatedOrderEntryWithPriceDtos = new List<OrderEntryWithPriceDto>();

        for (int i = 0; i < orderEntries.Count(); i++)
        {
            updatedOrderEntryWithPriceDtos
                .Add(new OrderEntryWithPriceDto(
                    orderEntries.ElementAt(i),
                    orderEntryWithPriceDtos.ElementAt(i).PriceOfOrderEntry));
        }

        return updatedOrderEntryWithPriceDtos;
    } 
    
    private async Task<Order> InsertOrderIntoRepository(int customerId, double totalAmount)
    {
        CreateOrderDto createOrderDto = InstantiateCreateOrderDto(customerId, totalAmount);
        Order createdOrder = await orderRepository.CreateOrder(createOrderDto);

        return createdOrder;
    }

    private CreateOrderDto InstantiateCreateOrderDto(int customerId, double totalAmount)
    {
        CreateOrderDto createOrderDto = new CreateOrderDto
        {
            OrderDate = DateTime.UtcNow,
            DeliveryDate = new DateOnly(),
            Status = "Processing",
            TotalAmount = totalAmount,
            CustomerId = customerId
        };

        return createOrderDto;
    }

    private double TotalPriceOfOrderEntries(IEnumerable<OrderEntryWithPriceDto> orderEntryWithPriceDtos)
    {
        double totalPrice = 0;

        foreach (var orderEntry in orderEntryWithPriceDtos)
        {
            totalPrice += orderEntry.PriceOfOrderEntry;
        }

        return totalPrice;
    }
}