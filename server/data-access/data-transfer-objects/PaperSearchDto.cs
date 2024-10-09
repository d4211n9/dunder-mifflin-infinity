namespace data_access.data_transfer_objects;

public class PaperSearchDto
{
    
    public int MaxPrice { get; set; }
    public int MinPrice { get; set; }
    public int MinStock { get; set; }
    public bool ShowDiscontinued { get; set; }
    public string NameSearchQuery { get; set; } = "";
    public PaginationDto PaginationDto { get; set; } = new();
}