namespace MongoDb.Data;

public class Item
{
    public string Name { get; set; }
    public float Price { get; set; }
    public string Description { get; set; }

    public Item(string name, float price, string description)
    {
        Name = name;
        Price = price;
        Description = description;
    }
}
