namespace data_access.data_transfer_objects;

public class CreateOrderWithOrderEntriesDto
{
    public int CustomerId { get; set; }
    public IEnumerable<CreateOrderEntryWithoutOrderIdDto> OrderEntryDtos { get; set; }
}