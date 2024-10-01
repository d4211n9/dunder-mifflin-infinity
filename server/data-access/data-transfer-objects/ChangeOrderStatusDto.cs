namespace data_access.data_transfer_objects;

public class ChangeOrderStatusDto
{
    public int OrderId { get; set; }
    public string UpdatedStatus { get; set; }
}