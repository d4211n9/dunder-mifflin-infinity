using System.Collections;
using data_access.models;

namespace data_access.data_transfer_objects;

public class OrderDto
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public DateOnly? DeliveryDate { get; set; }
    public string Status { get; set; } = null!;
    public double TotalAmount { get; set; }
    public int CustomerId { get; set; }
    public IEnumerable<OrderEntryWithPriceDto> OrderEntries { get; set; } = new List<OrderEntryWithPriceDto>();

    public OrderDto(Order order)
    {
        Id = order.Id;
        OrderDate = order.OrderDate;
        Status = order.Status;
        TotalAmount = order.TotalAmount;
        CustomerId = order.CustomerId ?? -1;
    }

    public OrderDto(Order order, IEnumerable<OrderEntryWithPriceDto> orderEntries) : this(order)
    {
        OrderEntries = orderEntries;
    }
}