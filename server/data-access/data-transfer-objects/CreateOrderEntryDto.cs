namespace data_access.data_transfer_objects;

public class CreateOrderEntryDto
{
    private int Quantity { get; set; }
    private int ProductId { get; set; }
    private int OrderId { get; set; }
}