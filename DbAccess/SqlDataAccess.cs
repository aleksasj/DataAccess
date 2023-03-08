﻿using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Dapper;
using System.Data;

namespace DataAccess.DbAccess;

public class SqlDataAccess : ISqlDataAccess
{
    private readonly IConfiguration _config;

    public SqlDataAccess(IConfiguration config)
    {
        _config = config;
    }

    public async Task<IEnumerable<T>> Execute<T, U>(
        string storedProcedure,
        U parameters,
        string connectionId = "Default")
    {
        using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

        return await connection.QueryAsync<T>(
            storedProcedure,
            parameters,
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<T>> Execute<T>(string storedProcedure, T parameters, string connectionId = "Default")
    {
        return await Execute<T, dynamic>(storedProcedure, parameters, connectionId);
    }
    public async Task<IEnumerable<T>> Execute<T>(string storedProcedure, string connectionId = "Default")
    {
        return await Execute<T, dynamic>(storedProcedure, new { }, connectionId);
    }
}

