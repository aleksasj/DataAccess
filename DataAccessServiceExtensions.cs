
using DataAccess.Data;
using DataAccess.DbAccess;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess;

public static class DataAccessServiceExtension
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services)
    {
        services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
        services.AddSingleton<IUserRepository, UserRepository>();
        services.AddSingleton<IAddressRepository, AddressRepository>();
        services.AddSingleton<IDriverRepository, DriverRepository>();
        services.AddSingleton<IOrderRepository, OrderRepository>();

        return services;
    }
}