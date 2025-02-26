using MrDAL.Domains.MrSchoolTime.ViewModule;
using MrDAL.Utility.Server;
using System.Text;

namespace MrDAL.Master;

public class ClsSchMaster
{
    public ClsSchMaster()
    {
        ObjMaster = new ObjSchMaster();
    }

    #region --------------- Global ---------------

    public ObjSchMaster ObjMaster { get; set; }

    #endregion --------------- Global ---------------

    private int IUDValue(StringBuilder _Query)
    {
        var cmdString = $@"
            BEGIN TRY
			    BEGIN TRANSACTION
			        {_Query}
			COMMIT TRANSACTION
			END TRY
			BEGIN CATCH
			 ROLLBACK TRANSACTION
			 END CATCH;";
        return SqlExtensions.ExecuteNonQuery(cmdString);
    }

    public int IUDSection()
    {
        var cmdString = new StringBuilder();
        if (ObjMaster._ActionTag.ToUpper() == "SAVE")
        {
            cmdString.Append("Insert into SCH.Section \n");
            cmdString.Append("Values ( (Select Max(SectionId)+1 Section from SCH.Section), \n");
            cmdString.Append(" '" + ObjMaster.TxtDescription.Trim().Replace("'", "''") + "',");
            cmdString.Append(" '" + ObjMaster.TxtShortName.Trim().Replace("'", "''") + "',");
        }
        else if (ObjMaster._ActionTag.ToUpper() == "UPDATE")
        {
            cmdString.Append("Update SCH.Section  set");
            cmdString.Append("SName ='" + ObjMaster.TxtDescription.Trim().Replace("'", "''") + "',");
            cmdString.Append("SShort Name='" + ObjMaster.TxtShortName.Trim().Replace(" '", "''") + "',");
        }
        else if (ObjMaster._ActionTag.ToUpper() == "DELETE")
        {
            cmdString.Append("Delete from SCH.Section where SId =" + ObjMaster.MasterId + string.Empty);
        }

        return IUDValue(cmdString);
    }
}