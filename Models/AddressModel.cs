namespace DataAccess.Models;

public class AddressModel
{
    public int Id { get; internal set; }
    public string Title { get; set; }
    public float Latitude { get; set; }
    public float Longitude { get; set; }
}
