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
    
    public static readonly Order FakeOrder2 = new Order
    {
        Id = 2,
        OrderDate = DateTime.UtcNow,
        DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow).AddDays(5),
        Status = "Delivered",
        TotalAmount = 15,
        CustomerId = FakeCustomers.FakeCustomer2.Id,
        Customer = FakeCustomers.FakeCustomer2,
        OrderEntries = new List<OrderEntry>()
    };
    
    public static readonly Order FakeOrder3 = new Order
    {
        Id = 3,
        OrderDate = DateTime.UtcNow,
        DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow).AddDays(5),
        Status = "Shipping",
        TotalAmount = 12,
        CustomerId = FakeCustomers.FakeCustomer3.Id,
        Customer = FakeCustomers.FakeCustomer3,
        OrderEntries = new List<OrderEntry>()
    };
    
    public static readonly Order FakeOrder4 = new Order
    {
        Id = 4,
        OrderDate = DateTime.UtcNow,
        DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow).AddDays(5),
        Status = "Delivered",
        TotalAmount = 3,
        CustomerId = FakeCustomers.FakeCustomer4.Id,
        Customer = FakeCustomers.FakeCustomer4,
        OrderEntries = new List<OrderEntry>()
    };
    
    public static readonly Order FakeOrder5 = new Order
    {
        Id = 5,
        OrderDate = DateTime.UtcNow,
        DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow).AddDays(5),
        Status = "Shipping",
        TotalAmount = 2,
        CustomerId = FakeCustomers.FakeCustomer5.Id,
        Customer = FakeCustomers.FakeCustomer5,
        OrderEntries = new List<OrderEntry>()
    };
}