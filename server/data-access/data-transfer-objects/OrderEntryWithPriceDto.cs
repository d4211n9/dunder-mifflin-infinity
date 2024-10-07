using data_access.models;

namespace data_access.data_transfer_objects;

public class OrderEntryWithPriceDto : OrderEntryDto
{
    public double PriceOfOrderEntry { get; set; }

    public OrderEntryWithPriceDto() {}

    public OrderEntryWithPriceDto(OrderEntry orderEntry) : base(orderEntry) {}

    public OrderEntryWithPriceDto(OrderEntry orderEntry, double priceOfOrderEntry) : base(orderEntry)
    {
        PriceOfOrderEntry = priceOfOrderEntry;
    }
    
    public OrderEntryWithPriceDto(CreateOrderEntryDto orderEntry) : base(orderEntry) {}
    
    public OrderEntryWithPriceDto(CreateOrderEntryWithoutOrderIdDto orderEntry) : base(orderEntry) {}
}