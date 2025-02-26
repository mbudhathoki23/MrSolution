using System;
using System.Data;
using System.Data.SqlClient;
using MrDAL.Global.Common;
using MrDAL.Utility.Server;

namespace MrDAL.Utility.Config;

public class ClsUserInfo
{
    public string Iud_UserInfo()
    {
        string msg;

        var con = GetConnection.GetConnectionMaster();
        try
        {
            var cmd = new SqlCommand("AMS.Usp_IUD_UserInfo", con)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@Event", Event);
            cmd.Parameters.AddWithValue("@Id", User_Id);
            cmd.Parameters.AddWithValue("@Name", Name);
            cmd.Parameters.AddWithValue("@userName", User_Name);
            cmd.Parameters.AddWithValue("@Old_Password", Password);
            cmd.Parameters.AddWithValue("@Password", ObjGlobal.Encrypt(Password));
            cmd.Parameters.AddWithValue("@Address", Address);
            cmd.Parameters.AddWithValue("@Mobile_No", Mobile_No);
            cmd.Parameters.AddWithValue("@Tel_PhoneNo", Tel_PhoneNo);
            cmd.Parameters.AddWithValue("@Email_Id", Email_Id);
            cmd.Parameters.AddWithValue("@Role_Id", Role_Id);
            cmd.Parameters.AddWithValue("@Branch_Id", Branch_Id);
            cmd.Parameters.AddWithValue("@Allow_Posting", Allow_Posting);
            cmd.Parameters.AddWithValue("@Posting_StartDate", Posting_StartDate);
            cmd.Parameters.AddWithValue("@Posting_EndDate", Posting_EndDate);
            cmd.Parameters.AddWithValue("@Modify_StartDate", Modify_StartDate);
            cmd.Parameters.AddWithValue("@Modify_EndDate", Modify_EndDate);
            cmd.Parameters.AddWithValue("@Auditors_Lock", Auditors_Lock);
            cmd.Parameters.AddWithValue("@Valid_Days", Valid_Days);
            cmd.Parameters.AddWithValue("@Created_By", Created_By);
            cmd.Parameters.AddWithValue("@Created_Date", Created_Date);
            cmd.Parameters.AddWithValue("@DEFAULT_DATABASE", ObjGlobal.InitialCatalog);
            var parameter = new SqlParameter
            {
                DbType = DbType.String,
                Direction = ParameterDirection.Output,
                Size = 200,
                ParameterName = "@Msg"
            };
            cmd.Parameters.Add(parameter);

            var value = new SqlParameter("@Return_Id", SqlDbType.VarChar, 100)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(value);

            cmd.ExecuteNonQuery();
            msg = cmd.Parameters["@Msg"].Value.ToString();
            return msg;
        }
        catch (Exception ex)
        {
            msg = ex.Message;
            throw new ArgumentException(ex.Message);
        }
        finally
        {
            con.Close();
        }
    }

    

    public DataTable CheckUserName_UserType(int User_Id, string usertype)
    {
        var con = GetConnection.GetConnectionMaster();
        using (con)
        {
            const string sql = "select * from AMS.UserInfo where User_Id=@User_Id and User_Type=@User_Type";
            var cmd = new SqlCommand(sql, con);

            cmd.Parameters.AddWithValue("@User_Id", User_Id);
            cmd.Parameters.AddWithValue("@User_Type", usertype);
            var da = new SqlDataAdapter(cmd);
            var ds = new DataSet();
            da.Fill(ds, "checkUsername_Usertype");
            return ds.Tables["checkUsername_Usertype"];
        }
    }

    public DataTable GetAllUser()
    {
        var con = GetConnection.GetConnectionMaster();
        using (con)
        {
            const string sql = "Select userName,User_Type from AMS.UserInfo";
            var da = new SqlDataAdapter(sql, con);
            var ds = new DataSet();
            da.Fill(ds, "user");
            return ds.Tables["user"];
        }
    }

    #region MyRegion

    public char Event { get; set; }

    public int User_Id { get; set; }

    public string Name { get; set; }

    public string User_Name { get; set; }

    public string Password { get; set; }

    public string Address { get; set; }

    public string Mobile_No { get; set; }

    public string Tel_PhoneNo { get; set; }

    public string Email_Id { get; set; }

    public int Role_Id { get; set; }

    public int Branch_Id { get; set; }

    public string Posting_StartDate { get; set; }

    public string Posting_EndDate { get; set; }

    public string Modify_StartDate { get; set; }

    public string Modify_EndDate { get; set; }

    public bool Auditors_Lock { get; set; }

    public int? Valid_Days { get; set; }

    public int Created_By { get; set; }

    public DateTime Created_Date { get; set; }

    public bool Allow_Posting { get; set; }

    #endregion MyRegion
}