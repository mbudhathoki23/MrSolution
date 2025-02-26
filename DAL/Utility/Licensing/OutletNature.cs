using System.ComponentModel;

namespace MrDAL.Utility.Licensing;

// ReSharper disable InconsistentNaming
public enum OutletNature
{
    [Description("ACCOUNT & INVENTORY MANAGEMENT SYSTEM")]
    AIMS = 1,

    [Description("POINT OF SALES")] Pos = 2,

    [Description("HOSPITALITY MANAGEMENT SYSTEM")]
    Restaurant = 3,

    [Description("HOSPITAL MANAGEMENT SYSTEM")]
    Hospital = 4,

    [Description("HOTEL & HOSPITALITY MANAGEMENT SYSTEM")]
    Hotel = 5,

    [Description("TRAVEL & TOUR MANAGEMENT SYSTEM")]
    Travel = 6,

    [Description("VEHICLE MANAGEMENT SYSTEM")]
    Vehicle = 7,

    [Description("PHARMACY MANAGEMENT SYSTEM")]
    Pharmacy = 8,

    [Description("BOX OFFICE MANAGEMENT SYSTEM")]
    Cinema = 9,

    [Description("SCHOOL MANAGEMENT SYSTEM")]
    SchoolTime = 10,

    [Description("ASSEST MANAGEMENT SYSTEM")]
    Asset = 11,

    [Description("HUMAN RESOURCES PLANNING")]
    HumanResource = 12,

    [Description("STAFF MANAGEMENT SYSTEM")]
    Payroll = 13,

    [Description("ENTERPRISE RESOURCE PLANNING")]
    ERP = 14
}