namespace DataAccess.DbAccess;

public interface ISqlDataAccess
{
    Task<IEnumerable<T>> Execute<T, U>(string storedProcedure, U parameters, string connectionId = "Default");
    Task<IEnumerable<T>> Execute<T>(string storedProcedure, T parameters, string connectionId = "Default");
}