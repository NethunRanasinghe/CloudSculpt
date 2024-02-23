using System.Collections.Generic;
using System.Threading.Tasks;
using CloudSculpt.Models;
using Dapper;

namespace CloudSculpt.HelperClasses;

public static class DatabaseManage
{
    public static List<VmData> AllVms { get; private set; } = [];
    public static VmData SelectedConfig { get; set; } = new ();

    #region VM Management
    public static async Task GetAllVmData()
    {
        const string query = "SELECT * FROM vmDataTable";
        var param = new DynamicParameters();
        
        var vmData = await SqliteDataAccess.GetData<VmData>(query,param);
        AllVms = vmData;
    }
    public static void UpdateVmData()
    {
        
    }
    public static void InsertVmData()
    {
        
    }
    public static void RemoveVmData()
    {
        
    }
    #endregion
}