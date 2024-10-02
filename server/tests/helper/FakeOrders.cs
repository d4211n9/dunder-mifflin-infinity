using data_access.models;

namespace tests.helper;

public class FakeOrders
{
    public static readonly Order FakeOrder1 = new Order
    {
        Id = 1,
        OrderDate = DateTime.UtcNow.AddHours(1),
        DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow).AddDays(1),
        Status = "Shipping",
        TotalAmount = 100,
        CustomerId = FakeCustomers.FakeCustomer1.Id,
        Customer = FakeCustomers.FakeCustomer1,
        OrderEntries = new List<OrderEntry>()
    };
    
    public static readonly Order FakeOrder2 = new Order
    {
        Id = 2,
        OrderDate = DateTime.UtcNow.AddHours(5),
        DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow).AddDays(4),
        Status = "Delivered",
        TotalAmount = 150,
        CustomerId = FakeCustomers.FakeCustomer2.Id,
        Customer = FakeCustomers.FakeCustomer2,
        OrderEntries = new List<OrderEntry>()
    };
    
    public static readonly Order FakeOrder3 = new Order
    {
        Id = 3,
        OrderDate = DateTime.UtcNow.AddHours(3),
        DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow).AddDays(4),
        Status = "Shipping",
        TotalAmount = 200,
        CustomerId = FakeCustomers.FakeCustomer3.Id,
        Customer = FakeCustomers.FakeCustomer3,
        OrderEntries = new List<OrderEntry>()
    };
    
    public static readonly Order FakeOrder4 = new Order
    {
        Id = 4,
        OrderDate = DateTime.UtcNow.AddHours(2),
        DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow).AddDays(2),
        Status = "Delivered",
        TotalAmount = 300,
        CustomerId = FakeCustomers.FakeCustomer4.Id,
        Customer = FakeCustomers.FakeCustomer4,
        OrderEntries = new List<OrderEntry>()
    };
    
    public static readonly Order FakeOrder5 = new Order
    {
        Id = 5,
        OrderDate = DateTime.UtcNow.AddHours(4),
        DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow).AddDays(5),
        Status = "Shipping",
        TotalAmount = 50,
        CustomerId = FakeCustomers.FakeCustomer5.Id,
        Customer = FakeCustomers.FakeCustomer5,
        OrderEntries = new List<OrderEntry>()
    };
}