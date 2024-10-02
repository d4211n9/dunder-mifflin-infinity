namespace data_access.data_transfer_objects;

public class CustomerOrdersSearchDto
{
    public double MaxAmount { get; set; }
    public double MinAmount { get; set; }
    public DeliveryTimeFrameDto DeliveryTimeFrameDto { get; set; }
    public OrderTimeFrameDto OrderTimeFrameDto { get; set; }
    public string OrderStatus { get; set; }
    public PaginationDto PaginationDto { get; set; }
}