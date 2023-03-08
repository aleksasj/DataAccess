using DataAccess.DbAccess;
using DataAccess.Models;

namespace DataAccess.Data;

public interface IOrderRepository
{
    Task Assign(int orderId, int? driverId);
    Task Cancel(int orderId);
    Task<OrderListModel?> Create(string name, string phone, int pickupId, int destinationId, string comment = "");
    Task<OrderListModel?> Detail(int orderId);
    Task Finish(int orderId);
    Task Picked(int orderId);
    Task<IEnumerable<OrdersModel>> GetOrders(int? driverId = null, int page = 1, int[] status = null, int perPage = 10);
    Task CancelPendingTooLong(int minutes);
    Task<IEnumerable<OrderListModel>> getPendingList();
}

public class OrderRepository : IOrderRepository
{
    private readonly ISqlDataAccess _db;
    public OrderRepository(ISqlDataAccess db)
    {
        _db = db;
    }
    public async Task<OrderListModel?> Create(string name, string phone, int pickupId, int destinationId, string comment = "") {
        var order = await _db.Execute<OrdersModel, dynamic>("dbo.spOrder_Create", new { Name = name, Phone = phone, PickupId = pickupId, DestinationId = destinationId, Comment = comment });
        var orderData = order.FirstOrDefault();

        if (orderData != null)
        {
            return await Detail(orderData.Id);
        }

        return null;
    }

    public Task Assign(int orderId, int? driverId) =>
        _db.Execute("dbo.spOrder_Assign", new { Id = orderId, DriverId = driverId });

    public Task Cancel(int orderId) =>
       _db.Execute("dbo.spOrder_Cancel", new { Id = orderId });

    public Task Picked(int orderId) =>
       _db.Execute("dbo.spOrder_Picked", new { Id = orderId });

    public Task Finish(int orderId) =>
       _db.Execute("dbo.spOrder_Finish", new { Id = orderId });

    public async Task<OrderListModel?> Detail(int orderId)
    {
        var result = await _db.Execute<OrderListModel, dynamic>("dbo.spOrder_Detail", new { Id = orderId });

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
            return await _db.Execute<OrdersModel, dynamic>("dbp.spOrder_ListByUser", new { DriverId = driverId, Offset = page, Status = string.Join(",", status), Limit = perPage });
        }

        return await _db.Execute<OrdersModel, dynamic>("dbp.spOrder_List", new { Offset = page, Status = string.Join(",", status), Limit = perPage });
    }

    public async Task CancelPendingTooLong(int min) => await _db.Execute("dbo.spOrder_CancelPendingTooLong", new { CancelTime = DateTime.Now.AddMinutes(min * -1) });

    public async Task<IEnumerable<OrderListModel>> getPendingList()
        => await _db.Execute<OrderListModel>("dbp.spOrder_PendingList");
}
