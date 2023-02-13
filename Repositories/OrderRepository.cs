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

    public Task Assign(int orderId, int driverId) =>
        _db.SaveData("dbo.spOrder_Assign", new { Id = orderId, DriverId = driverId });

    public Task Cancel(int orderId) =>
       _db.SaveData("dbo.spOrder_Cancel", new { Id = orderId });

    public Task Picked(int orderId) =>
       _db.SaveData("dbo.spOrder_Picked", new { Id = orderId });

    public Task Finish(int orderId) =>
       _db.SaveData("dbo.spOrder_Finish", new { Id = orderId });

    public async Task<UsersModel?> Detail(int orderId)
    {
        var result = await _db.LoadData<UsersModel, dynamic>("dbo.spOrder_Detaip", new { Id = orderId });

        return result.FirstOrDefault();
    }

}
