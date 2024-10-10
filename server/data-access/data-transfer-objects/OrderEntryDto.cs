using data_access.models;

namespace data_access.data_transfer_objects;

public class OrderEntryDto
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public int ProductId { get; set; }
    public int OrderId { get; set; }
    
    public OrderEntryDto() {}

    public OrderEntryDto(OrderEntry orderEntry)
    {
        Id = orderEntry.Id;
        Quantity = orderEntry.Quantity;
        ProductId = orderEntry.ProductId ?? -1;
        OrderId = orderEntry.OrderId ?? -1;
    }
    
    public OrderEntryDto(CreateOrderEntryDto orderEntry)
    {
        Id = -1;
        Quantity = orderEntry.Quantity;
        ProductId = orderEntry.ProductId;
        OrderId = orderEntry.OrderId;
    }
    
    public OrderEntryDto(CreateOrderEntryWithoutOrderIdDto orderEntry)
    {
        Id = -1;
        Quantity = orderEntry.Quantity;
        ProductId = orderEntry.ProductId;
        OrderId = -1;
    }
}