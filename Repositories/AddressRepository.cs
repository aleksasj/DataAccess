using DataAccess.DbAccess;
using DataAccess.Models;

namespace DataAccess.Data;

public class AddressRepository : IAddressRepository
{
    private readonly ISqlDataAccess _db;
    public AddressRepository(ISqlDataAccess db)
    {
        _db = db;
    }
    public Task Create(string title, float latitude, float longitude) => _db.SaveData("dbo.spAddress_Create", new
        {
            Title = title,
            Latitude = Helper.GeoLocation.FormatToStandart(latitude),
            Longitude = Helper.GeoLocation.FormatToStandart(longitude)
        }
    );
    public async Task<AddressModel?> Get(float latitude, float longitude)
    {
        var result = await _db.LoadData<AddressModel, dynamic>("dbo.spAddress_Get", new
        {
            Latitude = Helper.GeoLocation.FormatToStandart(latitude),
            Longitude = Helper.GeoLocation.FormatToStandart(longitude)
        });

        return result.FirstOrDefault();
    }
}
