namespace DataAccess.Models;

public class OrderListModel
{
    public int Id { get; set; }
    public int DriverId { internal get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public int PickupId { internal get; set; }
    public int DestinationId { internal get; set; }
    public string Comment { get; set; }
    public int Status { get; set; }
    public DateTime UpdatedAt { internal get; set; }
    public DateTime CreatedAt { get; internal set; }
    public string DriverUsername { get; set; }
    public string DestinationTitle { get; set; }
    public float DestinationLatitude { get; set; }
    public float DestinationLongitude { get; set; }
    public string PickupTitle { get; set; }
    public float PickupLatitude { get; set; }
    public float PickupLongitude { get; set; }
}
