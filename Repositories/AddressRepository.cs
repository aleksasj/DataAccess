using DataAccess.DbAccess;
using DataAccess.Models;

namespace DataAccess.Data;
public interface IAddressRepository
{
    Task<AddressModel?> Create(string title, float latitude, float longitude);
    Task<AddressModel?> Get(float latitude, float longitude);
}
public class AddressRepository : IAddressRepository
{
    private readonly ISqlDataAccess _db;
    public AddressRepository(ISqlDataAccess db)
    {
        _db = db;
    }
    public async Task<AddressModel?> Create(string title, float latitude, float longitude)
    {
        var result = await _db.LoadData<AddressModel, dynamic>("dbo.spAddress_Create", new
        {
            Title = title,
            Latitude = Helper.GeoLocation.FormatToStandart(latitude),
            Longitude = Helper.GeoLocation.FormatToStandart(longitude)
        });

        return result.FirstOrDefault();
    }
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
