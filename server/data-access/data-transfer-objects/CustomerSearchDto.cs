namespace data_access.data_transfer_objects;

public class CustomerSearchDto
{
    private string SearchQuery { get; set; }
    private PaginationDto PaginationDto { get; set; }
}