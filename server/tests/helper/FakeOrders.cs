using data_access.models;

namespace tests.helper;

public class FakeOrders
{
    public static readonly Order FakeOrder1 = new Order
    {
        Id = 1,
        OrderDate = DateTime.UtcNow,
        DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow).AddDays(5),
        Status = "Shipping",
        TotalAmount = 10,
        CustomerId = FakeCustomers.FakeCustomer1.Id,
        Customer = FakeCustomers.FakeCustomer1,
        OrderEntries = new List<OrderEntry>()
    };
}