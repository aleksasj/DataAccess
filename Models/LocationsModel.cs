using System.Numerics;

namespace DataAccess.Models;

public class LocationsModel
{
    public BigInteger Id { get; internal set; }
    public UsersModel? Driver { get; set; }
    public OrdersModel? Order { get; internal set; }
    public float Latitude { get; set; }
    public float Longitude { get; set; }
    public DateTime UpdatedAt { get; internal set; }
}
