using System.Collections.Generic;
using System.Threading.Tasks;
using CloudSculpt.Models;
using Dapper;

namespace CloudSculpt.HelperClasses;

public static class DatabaseManage
{
    public static List<VmData> AllVms { get; private set; } = [];
    public static VmData SelectedConfigTemp { get; set; } = new ();
    public static VmData SelectedConfig { get; set; } = new ();

    #region VM Management
    public static async Task GetAllVmData()
    {
        const string query = "SELECT * FROM vmDataTable";
        var param = new DynamicParameters();
        
        var vmData = await SqliteDataAccess.GetData<VmData>(query,param);
        AllVms = vmData;
    }
    public static async Task UpdateVmData(string columnName, string condition)
    {
        const string query = "UPDATE vmDataTable SET @column WHERE @condition";
        var param = new DynamicParameters();
        param.Add("@column",columnName);
        param.Add("@condition",condition);

        var rows = await SqliteDataAccess.InsertOrUpdateData(query, param);
    }
    public static async Task<int> InsertVmData(string vmNameValue, string vmIpValue)
    {
        const string query = "INSERT INTO vmDataTable(vmName, vmIp) VALUES(@vmNameValue,@vmIpValue)";
        var param = new DynamicParameters();
        param.Add("@vmNameValue",vmNameValue);
        param.Add("@vmIpValue",vmIpValue);

        var rows = await SqliteDataAccess.InsertOrUpdateData(query, param);
        return rows;
    }
    public static async Task<int> RemoveVmData(string condition)
    {
        const string query = "DELETE FROM vmDataTable WHERE vmIp = @condition";
        var param = new DynamicParameters();
        param.Add("@condition",condition);

        var rows = await SqliteDataAccess.DeleteData(query, param);
        return rows;
    }
    #endregion
}