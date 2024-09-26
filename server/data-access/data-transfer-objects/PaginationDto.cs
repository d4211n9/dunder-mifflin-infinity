namespace data_access.data_transfer_objects;

public class PaginationDto
{
    private int PageNumber { get; set; } = 1;
    private int PageSize { get; set; } = 10;
    private int TotalPages { get; set; } = 1;
}