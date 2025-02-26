using MrDAL.Domains.VehicleManagement;
using System.Data;

namespace MrDAL.Master.Interface;

public interface IVMaster
{
    ClsVMaster.ObjVMasterSetup ObjVMaster { get; set; }

    DataTable Get_DataVehicleNumber(string _Tag, int SelectedId = 0);

    DataTable Get_DataVehicleColor(string _Tag, int SelectedId = 0);

    DataTable Get_DataVehicleModel(string _ActionTag, int SelectedId = 0);

    DataTable GetCombineDataValue();
}