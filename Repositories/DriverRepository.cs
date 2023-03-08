using DataAccess.DbAccess;
using DataAccess.Models;

namespace DataAccess.Data;

public interface IDriverRepository
{
    Task AddLocation(int driverId, float latitude, float longitude);
    Task<IEnumerable<UsersModel>> getAvailable(float latitude, float longitude, int maxActiveOrders = 0, int maxDistanceFrom = 10);
    Task StartWorking(int userId);
    Task StopWorking(int userId);
}
public class DriverRepository : IDriverRepository
{
    private readonly ISqlDataAccess _db;
    public DriverRepository(ISqlDataAccess db)
    {
        _db = db;
    }
    public Task AddLocation(int driverId, float latitude, float longitude) => _db.Execute("dbo.spLocation_Create", new
    {
        Id = driverId,
        Latitude = Helper.GeoLocation.FormatToStandart(latitude),
        Longitude = Helper.GeoLocation.FormatToStandart(longitude)
    }
    );
    public Task StartWorking(int userId) => _db.Execute("dbo.spLocation_StartWorking", new { Id = userId });

    public Task StopWorking(int userId) => _db.Execute("dbo.spLocation_StopWorking", new { Id = userId });

    public async Task<IEnumerable<UsersModel>> getAvailable(float latitude, float longitude, int maxActiveOrders = 0, int maxDistanceFrom = 10)
    {
        return await _db.Execute<UsersModel, dynamic>("dbo.spDriver_GetAvailable",
           new { 
               OrderLatitude = Helper.GeoLocation.FormatToStandart(latitude), 
               OrderLogintude = Helper.GeoLocation.FormatToStandart(longitude), 
               MaxAllowedOrdersCount = maxActiveOrders, 
               MaxAllowedOrdersDistance = maxDistanceFrom 
           });
    }
}
