using DataAccess.DbAccess;
using DataAccess.Models;

namespace DataAccess.Data;

public interface IUserRepository
{
    Task<UsersModel?> GetUserByCredentials(string username, string password);
    Task<UsersModel?> Get(int id);
    Task Create(string username, string password, string role = UsersModel.ROLE_DRIVER);
    Task ChangePassword(int userId, string password);
    Task<IEnumerable<UsersModel>> GetAll();

}
public class UserRepository : IUserRepository
{
    private readonly ISqlDataAccess _db;
    public UserRepository(ISqlDataAccess db) => _db = db;
    public async Task<UsersModel?> GetUserByCredentials(string username, string password)
    {
        var result = await _db.Execute<UsersModel, dynamic>("dbo.spUser_Auth",
            new { Username = username, Password = password });

        return result.FirstOrDefault();
    }
    public async Task Create(string username, string password, string role = UsersModel.ROLE_DRIVER)
        => await _db.Execute("dbo.spUser_Create", new { Username = username, Password = password, Role = role });

    public async Task<UsersModel?> Get(int id)
    {
        var result = await _db.Execute<UsersModel, dynamic>("dbo.spUser_Get", new { Id = id });

        return result.FirstOrDefault();
    }

    public Task ChangePassword(int userId, string password) => _db.Execute("dbo.spUser_ChangePassword", new
    {
        Id = userId,
        Password = password,
    }
    );

    public async Task<IEnumerable<UsersModel>> GetAll() => await _db.Execute<UsersModel, dynamic>("dbo.spUser_All", new { });
}
