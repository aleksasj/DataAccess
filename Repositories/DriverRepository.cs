using DataAccess.DbAccess;
using DataAccess.Models;

namespace DataAccess.Data;

public class DriverRepository : IDriverRepository
{
    private readonly ISqlDataAccess _db;
    public DriverRepository(ISqlDataAccess db)
    {
        _db = db;
    }
    public Task AddLocation(int driverId, float latitude, float longitude) => _db.SaveData("dbo.spLocation_Create", new
    {
        Id = driverId,
        Latitude = Helper.GeoLocation.FormatToStandart(latitude),
        Longitude = Helper.GeoLocation.FormatToStandart(longitude)
    }
    );
    public Task StartWorking(int userId) => _db.SaveData("dbo.spLocation_StartWorking", new { Id = userId });

    public Task StopWorking(int userId) => _db.SaveData("dbo.spLocation_StopWorking", new { Id = userId });

    public async Task<IEnumerable<UsersModel>> getAvailable(float latitude, float longitude, int maxActiveOrders = 0, int maxDistanceFrom = 10)
    {
        return await _db.LoadData<UsersModel, dynamic>("dbo.spDriver_GetAvailable",
           new { 
               Latitude = Helper.GeoLocation.FormatToStandart(latitude), 
               Logintude = Helper.GeoLocation.FormatToStandart(longitude), 
               MaxAllowedOrdersCount = maxActiveOrders, 
               MaxAllowedOrdersDistance = maxDistanceFrom 
           });
    }
}
