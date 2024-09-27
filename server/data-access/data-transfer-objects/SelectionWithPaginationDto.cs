namespace data_access.data_transfer_objects;

public class SelectionWithPaginationDto<T>
{
    public IEnumerable<T> Selection { get; set; }
    public int TotalPages { get; set; }
}