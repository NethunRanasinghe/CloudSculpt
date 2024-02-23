using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace CloudSculpt.HelperClasses;

public static class SqliteDataAccess
{
    /*
     * --Nuget Packages--
     * "System.Data.SQLite.Core"
     * "Dapper"
     * 
     * --Connection String--
     * "Data Source=./cloudSculpt.db;Version=3;"
     */

    public static async Task<List<T>> GetData<T>(string query, DynamicParameters parameters)
    {
        using IDbConnection connection = new SQLiteConnection(LoadConnectionString());
        var output = await connection.QueryAsync<T>(query, parameters);
        return output.ToList();
    }
    
    public static async Task<int> InsertOrUpdateData<T>(string query, T data)
    {
        using IDbConnection connection = new SQLiteConnection(LoadConnectionString());
        var affectedRows = await connection.ExecuteAsync(query, data);
        return affectedRows;
    }

    public static async Task<int> DeleteData(string query, DynamicParameters parameters)
    {
        using IDbConnection connection = new SQLiteConnection(LoadConnectionString());
        var affectedRows = await connection.ExecuteAsync(query, parameters);
        return affectedRows;
    }

    
    private static string LoadConnectionString()
    {
        return "Data Source=./cloudSculpt.db;Version=3;";
    }
}