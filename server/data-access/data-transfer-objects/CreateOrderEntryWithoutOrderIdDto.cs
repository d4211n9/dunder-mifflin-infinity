namespace data_access.data_transfer_objects;

public class CreateOrderEntryWithoutOrderIdDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}