using MrDAL.Domains.Hospital.ViewModule;
using System.Data;
using System.Windows.Forms;

namespace MrDAL.Master.Interface;

public interface IHMaster
{
    #region --------------- I_U_D ---------------

    int SaveBedNo();

    int SaveDoctor();

    int SaveBedType();

    int SaveDoctorType();

    int SavePatient();

    #endregion --------------- I_U_D ---------------

    #region --------------- DataTable ---------------

    DataTable CheckPatientIdExitsOrNot(string patientId);

    DataTable ReturnPatientInformation(long patientId);

    DataTable MasterDoctor(string actionTag, int selectedId = 0);

    DataTable MasterHDepartment(int selectedId = 0);

    DataTable GetPatientListOnDashboard();

    DataTable GetDoctorTypeInformation(int doctorTypeId);

    // COMBO BOX
    ComboBox ReturnTitleOfName(ComboBox box);

    ComboBox ReturnAgeType(ComboBox box);

    ComboBox ReturnGender(ComboBox box);

    ComboBox ReturnMartial(ComboBox box);

    ComboBox ReturnRegistrationType(ComboBox box);

    ComboBox ReturnReligion(ComboBox box);

    ComboBox ReturnNationality(ComboBox box);

    ComboBox ReturnBloodGroup(ComboBox box);

    #endregion --------------- DataTable ---------------

    #region ---------------VM_HOSPITAL ---------------

    VmBedMaster ObjBedMaster { get; set; }
    VmBedType ObjBedType { get; set; }
    VmDoctor ObjDoctor { get; set; }
    VmDoctorType VmDoctorType { get; set; }
    VmPatient VmPatient { get; set; }

    #endregion ---------------VM_HOSPITAL ---------------
}