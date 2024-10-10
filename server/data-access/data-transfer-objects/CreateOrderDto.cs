using data_access.models;

namespace data_access.data_transfer_objects;

public class CreateOrderDto
{
    public DateTime OrderDate { get; set; }
    public DateOnly DeliveryDate { get; set; }
    public string Status { get; set; }
    public double TotalAmount { get; set; }
    public int CustomerId { get; set; }
}