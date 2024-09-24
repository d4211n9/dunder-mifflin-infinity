namespace data_access.data_transfer_objects;

public class GetCustomerOrdersDto
{
    private int CustomerId { get; set; }
    private PaginationDto PaginationDto { get; set; }
}