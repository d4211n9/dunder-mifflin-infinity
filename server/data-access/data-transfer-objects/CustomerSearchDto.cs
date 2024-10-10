namespace data_access.data_transfer_objects;

public class CustomerSearchDto
{
    public string SearchQuery { get; set; } = "";
    public PaginationDto PaginationDto { get; set; }
}