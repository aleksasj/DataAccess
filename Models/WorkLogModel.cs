namespace DataAccess.Models;

public class WorkLogModel
{
    public int Id { get; set; }
    public UsersModel? User { get; set; }
    public int Working { get; set; }
    public DateTime CreatedAt { get; internal set; }
}
