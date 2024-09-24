namespace data_access.data_transfer_objects;

public class PaginationDto
{
    private int PageNumber { get; set; } = 0;
    private int PageSize { get; set; } = 10;
}