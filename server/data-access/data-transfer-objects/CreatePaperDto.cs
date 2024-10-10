namespace data_access.data_transfer_objects;

public class CreatePaperDto
{
    public string Name { get; set; }
    public int Stock { get; set; }
    public double Price { get; set; }
}