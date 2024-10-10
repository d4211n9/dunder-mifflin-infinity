namespace data_access.data_transfer_objects;

public class CreateOrderEntryDto
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }

    public CreateOrderEntryDto(CreateOrderEntryWithoutOrderIdDto orderEntryDto, int orderId)
    {
        OrderId = orderId;
        ProductId = orderEntryDto.ProductId;
        Quantity = orderEntryDto.Quantity;
    }
}