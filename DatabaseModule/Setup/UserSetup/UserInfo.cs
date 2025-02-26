using System;

namespace DatabaseModule.Setup.UserSetup;

public class UserInfo
{
    public int User_Id { get; set; }
    public string Full_Name { get; set; }
    public string User_Name { get; set; }
    public string Password { get; set; }
    public string Address { get; set; }
    public string Mobile_No { get; set; }
    public string Phone_No { get; set; }
    public string Email_Id { get; set; }
    public int? Role_Id { get; set; }
    public int? Branch_Id { get; set; } = 0;
    public bool? Allow_Posting { get; set; } = false;
    public System.DateTime? Posting_StartDate { get; set; } = DateTime.Now;
    public System.DateTime? Posting_EndDate { get; set; } = DateTime.Now;
    public System.DateTime? Modify_StartDate { get; set; } = DateTime.Now;
    public System.DateTime? Modify_EndDate { get; set; } = DateTime.Now;
    public bool Auditors_Lock { get; set; } = false;
    public int Valid_Days { get; set; } = 0;
    public string Created_By { get; set; } = "ADMIN";
    public System.DateTime Created_Date { get; set; } = DateTime.Now;
    public bool? Status { get; set; } = true;
    public long? Ledger_Id { get; set; }
    public string Category { get; set; } = "ADMINISTRATOR";
    public bool? Authorized { get; set; } = false;
    public bool? IsQtyChange { get; set; } = false;
    public bool? IsDefault { get; set; } = false;
    public bool? IsModify { get; set; } = false;
    public bool? IsDeleted { get; set; } = false;
    public bool? IsRateChange { get; set; } = false;
    public bool? IsPDCDashBoard { get; set; }
}