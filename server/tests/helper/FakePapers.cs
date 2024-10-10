using data_access.models;

namespace tests.helper;

public class FakePapers
{
    public static Paper FakePaper1 = new Paper
    {
        Id = 1,
        Name = "paper1",
        Discontinued = false,
        Stock = 100,
        Price = 20,
        OrderEntries = new List<OrderEntry>(),
        Properties = new List<Property>()
    };
}