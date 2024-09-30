using data_access.models;

namespace tests.helper;

public class FakeCustomers
{
    public static readonly Customer _fakeCustomer1 = new Customer()
    {
        Id = 1,
        Address = "Amager Strandvej 100",
        Email = "dadimov@gmaillli.com",
        Name = "Martha D. Daniels",
        Orders = new List<Order>()
    };
    
    public static readonly Customer _fakeCustomer2 = new Customer()
    {
        Id = 2,
        Address = "Brolæggervej 16",
        Email = "xbondinx1@maxpedia.cloud",
        Name = "Lewis W. Degraff",
        Orders = new List<Order>()
    };
    
    public static readonly Customer _fakeCustomer3 = new Customer()
    {
        Id = 3,
        Address = "Storegade 28A",
        Email = "mrdove@bensenisevmem.shop",
        Name = "Maria W. Trimmer",
        Orders = new List<Order>()
    };
    
    public static readonly Customer _fakeCustomer4 = new Customer()
    {
        Id = 4,
        Address = "Islevhusvej 32",
        Email = "se1943@replicant.club",
        Name = "Pandora T. Warner",
        Orders = new List<Order>()
    };
    
    public static readonly Customer _fakeCustomer5 = new Customer()
    {
        Id = 5,
        Address = "Zeltnersvej 4",
        Email = "broederetd@onlyu.link",
        Name = "Clint C. Badilla",
        Orders = new List<Order>()
    };
}