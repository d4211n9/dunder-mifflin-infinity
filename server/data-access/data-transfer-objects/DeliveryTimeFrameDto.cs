namespace data_access.data_transfer_objects;

public class DeliveryTimeFrameDto
{
    private DateOnly DeliveryUntil { get; set; }
    private DateOnly DeliverySince { get; set; }
}