namespace DataAccess.Models;

public class UsersModel
{
    public const string ROLE_ADMIN = "Admin";
    public const string ROLE_DRIVER = "Driver";

    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
}
