
namespace DataAccess.Models;

public class OrdersModel
{
    public const int STATUS_CANCELED = -1;
    public const int STATUS_NEW = 0;
    public const int STATUS_ASSIGNED = 1;
    public const int STATUS_PICKED = 2;
    public const int STATUS_FINISHED = 3;

    public int Id { get; internal set; }
    internal UsersModel? Driver { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public AddressModel Pickup { get; set; }
    public AddressModel Destination { get; set; }
    public string? Comment { get; set; }
    public int Status { get; internal set; }
    public DateTime UpdatedAt { get; internal set; }
    public DateTime CreatedAt { get; internal set; }
}
