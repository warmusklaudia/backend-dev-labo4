namespace Cars.Models;

public class Brand
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string? Name { get; set; }
    public string? Country { get; set; }
    public string? Logo { get; set; }
    public DateTime CreatedOn { get; set; }
}