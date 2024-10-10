namespace data_access.data_transfer_objects;

public class ChangeOrderTotalAmountDto
{
    public int OrderId { get; set; }
    public double UpdatedTotalAmount { get; set; }
}