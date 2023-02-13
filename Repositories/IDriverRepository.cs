using DataAccess.Models;

namespace DataAccess.Data
{
    public interface IDriverRepository
    {
        Task AddLocation(int driverId, float latitude, float longitude);
        Task<IEnumerable<UsersModel>> getAvailable(float latitude, float longitude, int maxActiveOrders = 0, int maxDistanceFrom = 10);
        Task StartWorking(int userId);
        Task StopWorking(int userId);
    }
}