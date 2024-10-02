namespace data_access.data_transfer_objects;

public class GetCustomerOrdersDto
{
    public int CustomerId { get; set; }
    public PaginationDto PaginationDto { get; set; }
}