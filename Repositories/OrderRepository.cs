using DataAccess.DbAccess;
using DataAccess.Models;

namespace DataAccess.Data;

public class OrderRepository : IOrderRepository
{
    private readonly ISqlDataAccess _db;
    public OrderRepository(ISqlDataAccess db)
    {
        _db = db;
    }
    public Task Create(string name, string phone, int pickupId, int destinationId, string comment = "") =>
        _db.SaveData("dbo.spOrder_Create", new { Name = name, Phone = phone, PickupId = pickupId, DestinationId = destinationId, Comment = comment });

    public Task Assign(int orderId, int? driverId) =>
        _db.SaveData("dbo.spOrder_Assign", new { Id = orderId, DriverId = driverId });

    public Task Cancel(int orderId) =>
       _db.SaveData("dbo.spOrder_Cancel", new { Id = orderId });

    public Task Picked(int orderId) =>
       _db.SaveData("dbo.spOrder_Picked", new { Id = orderId });

    public Task Finish(int orderId) =>
       _db.SaveData("dbo.spOrder_Finish", new { Id = orderId });

    public async Task<OrdersModel?> Detail(int orderId)
    {
        var result = await _db.LoadData<OrdersModel, dynamic>("dbo.spOrder_Detail", new { Id = orderId });

        return result.FirstOrDefault();
    }

    public async Task<IEnumerable<OrdersModel>> GetOrders(int? driverId = null, int page = 1, int[]? status = null, int perPage = 10)
    {
        page = page < 0 ? 1 : page;
        perPage = perPage < 1 ? 1 : 10;
        page--;

        if (status == null) {
            status = new int[] { 
                OrdersModel.STATUS_CANCELED,
                OrdersModel.STATUS_NEW,
                OrdersModel.STATUS_ASSIGNED,
                OrdersModel.STATUS_PICKED,
                OrdersModel.STATUS_FINISHED
            };
        }

        if (driverId != null)
        {
            return await _db.LoadData<OrdersModel, dynamic>("dbp.spOrder_ListByUser", new { DriverId = driverId, Offset = page, Status = string.Join(",", status), Limit = perPage });
        }

        return await _db.LoadData<OrdersModel, dynamic>("dbp.spOrder_List", new { Offset = page, Status = string.Join(",", status), Limit = perPage });
    }
}
