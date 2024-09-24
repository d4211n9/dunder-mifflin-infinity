namespace data_access.data_transfer_objects;

public class PaperSearchDto
{
    
    private int MaxPrice { get; set; }
    private int MinPrice { get; set; }
    private int MinStock { get; set; }
    private bool ShowDiscontinued { get; set; }
    private string NameSearchQuery { get; set; }
    private PaginationDto PaginationDto { get; set; }
}