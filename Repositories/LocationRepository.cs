using DataAccess.DbAccess;

namespace DataAccess.Data;
public interface ILocationRepository
{
    Task Create(int driverId, float latitude, float longitude, int? orderId = null);
}
public class LocationRepository : ILocationRepository
{
    private readonly ISqlDataAccess _db;
    public LocationRepository(ISqlDataAccess db)
    {
        _db = db;
    }
    public Task Create(int driverId, float latitude, float longitude, int? orderId = null) => _db.SaveData("dbo.spLocation_Create", new {
            DriverId = driverId,
            Latitude = Helper.GeoLocation.FormatToStandart(latitude),
            Longitude = Helper.GeoLocation.FormatToStandart(longitude),
            OrderId = orderId
        }
    );
}
