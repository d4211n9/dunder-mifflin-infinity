namespace data_access.data_transfer_objects;

public class OrderTimeFrameDto
{
    public DateTime OrderTimeUntil { get; set; }
    public DateTime OrderTimeSince { get; set; }
}