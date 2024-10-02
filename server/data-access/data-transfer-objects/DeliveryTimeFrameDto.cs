namespace data_access.data_transfer_objects;

public class DeliveryTimeFrameDto
{
    public DateOnly DeliveryUntil { get; set; }
    public DateOnly DeliverySince { get; set; }
}