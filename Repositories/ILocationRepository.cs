namespace DataAccess.Data;

public interface ILocationRepository
{
    Task Create(int driverId, float latitude, float longitude, int? orderId = null);
}