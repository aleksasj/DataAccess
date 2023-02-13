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