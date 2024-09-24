namespace data_access.data_transfer_objects;

public class CreateOrderDto
{
    private int CustomerId { get; set; }
    private IEnumerable<OrderEntryDto> OrderEntryDtos { get; set; }
}