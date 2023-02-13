using DataAccess.Models;

namespace DataAccess.Data;

public interface IOrderRepository
{
    Task Assign(int orderId, int? driverId);
    Task Cancel(int orderId);
    Task Create(string name, string phone, int pickupId, int destinationId, string comment = "");
    Task<OrdersModel?> Detail(int orderId);
    Task Finish(int orderId);
    Task Picked(int orderId);
    Task<IEnumerable<OrdersModel>> GetOrders(int? driverId = null, int page = 1, int[] status = null, int perPage = 10);

}