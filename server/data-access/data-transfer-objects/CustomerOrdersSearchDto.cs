namespace data_access.data_transfer_objects;

public class CustomerOrdersSearchDto
{
    private double MaxAmount { get; set; }
    private double MinAmount { get; set; }
    private DeliveryTimeFrameDto DeliveryTimeFrameDto { get; set; }
    private OrderTimeFrameDto OrderTimeFrameDto { get; set; }
    private string OrderStatus { get; set; }
    private PaginationDto PaginationDto { get; set; }
}