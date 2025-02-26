using MrDAL.Global.Common.RawQuery.Interface;

namespace MrDAL.Global.Common.RawQuery;

public class GetSalesInvoiceQuery : ISalesInvoiceQuery
{
    public string GetSalesInvoiceMaster(string voucherNo)
    {
        var cmdString = $@"
            SELECT sm.SB_Invoice, sm.Invoice_Date, sm.Invoice_Miti, sm.PB_Vno, gl.GLName, gl.NepaliDesc, gl.PanNo, gl.GLAddress, gl.GLCode, gl.ACCode, gl.GLType, sm.PartyLedgerId, sm.Party_Name, sm.Vat_No, sm.Contact_Person, sm.Mobile_No, sm.Address, sm.ChqNo, sm.ChqDate, sm.ChqMiti, sm.Invoice_Type, sm.Invoice_Mode, sm.Payment_Mode, sm.DueDays, sm.DueDate, ja.AgentName, sl.SLName SubLedgerName, sm.SO_Invoice, sm.SO_Date, sm.SC_Invoice, sm.SC_Date, sm.Cls1, sm.Cls2, sm.Cls3, sm.Cls4, c.CName CounterName, c.CCode CurrencyName, sm.Cur_Rate, sm.B_Amount, sm.T_Amount, sm.N_Amount, sm.LN_Amount, sm.V_Amount, sm.Tbl_Amount, sm.Tender_Amount, sm.Return_Amount, sm.Action_Type, sm.In_Words, sm.Remarks, sm.Is_Printed, sm.No_Print, sm.Printed_By, sm.Printed_Date, sm.Enter_By, sm.Enter_Date, d.DrName DoctorName, pm.PaitentDesc PatientName, pm.ContactNo PaitentContactNo, pm.TAddress PaitentTAddress, hd.DName HospitalDepartmentName, ms.MShipDesc MemberName, ms.MemberId, tm.TableName TableName
            FROM AMS.SB_Master sm
                 LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID=sm.Customer_Id
                 LEFT OUTER JOIN AMS.GeneralLedger pl ON pl.GLID=sm.PartyLedgerId
                 LEFT OUTER JOIN AMS.JuniorAgent ja ON ja.AgentId=gl.AgentId
                 LEFT OUTER JOIN AMS.SubLedger sl ON sl.SLId=sm.Subledger_Id
                 LEFT OUTER JOIN AMS.Counter c ON c.CId=sm.CounterId
                 LEFT OUTER JOIN HOS.Doctor d ON d.DrId=sm.DoctorId
                 LEFT OUTER JOIN HOS.PatientMaster pm ON pm.PaitentId=sm.PatientId
                 LEFT OUTER JOIN HOS.Department hd ON hd.DId=sm.HDepartmentId
                 LEFT OUTER JOIN AMS.MemberShipSetup ms ON ms.MShipId=sm.MShipId
                 LEFT OUTER JOIN AMS.TableMaster tm ON tm.TableId=sm.TableId
                 sm.SB_Invoice = '{voucherNo}'";
        return cmdString;
    }

    public string GetSalesInvoiceDetails(string voucherNo)
    {
        var cmdString = $@"
            SELECT sm.SB_Invoice, sm.Invoice_Date, sm.Invoice_Miti, sm.PB_Vno, gl.GLName, gl.NepaliDesc, gl.PanNo, gl.GLAddress, gl.GLCode, gl.ACCode, gl.GLType, sm.PartyLedgerId, sm.Party_Name, sm.Vat_No, sm.Contact_Person, sm.Mobile_No, sm.Address, sm.ChqNo, sm.ChqDate, sm.ChqMiti, sm.Invoice_Type, sm.Invoice_Mode, sm.Payment_Mode, sm.DueDays, sm.DueDate, ja.AgentName, sl.SLName SubLedgerName, sm.SO_Invoice, sm.SO_Date, sm.SC_Invoice, sm.SC_Date, sm.Cls1, sm.Cls2, sm.Cls3, sm.Cls4, c.CName CounterName, c.CCode CurrencyName, sm.Cur_Rate, sm.B_Amount, sm.T_Amount, sm.N_Amount, sm.LN_Amount, sm.V_Amount, sm.Tbl_Amount, sm.Tender_Amount, sm.Return_Amount, sm.Action_Type, sm.In_Words, sm.Remarks, sm.Is_Printed, sm.No_Print, sm.Printed_By, sm.Printed_Date, sm.Enter_By, sm.Enter_Date, d.DrName DoctorName, pm.PaitentDesc PatientName, pm.ContactNo PaitentContactNo, pm.TAddress PaitentTAddress, hd.DName HospitalDepartmentName, ms.MShipDesc MemberName, ms.MemberId, tm.TableName TableName
            FROM AMS.SB_Master sm
                 LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID=sm.Customer_Id
                 LEFT OUTER JOIN AMS.GeneralLedger pl ON pl.GLID=sm.PartyLedgerId
                 LEFT OUTER JOIN AMS.JuniorAgent ja ON ja.AgentId=gl.AgentId
                 LEFT OUTER JOIN AMS.SubLedger sl ON sl.SLId=sm.Subledger_Id
                 LEFT OUTER JOIN AMS.Counter c ON c.CId=sm.CounterId
                 LEFT OUTER JOIN HOS.Doctor d ON d.DrId=sm.DoctorId
                 LEFT OUTER JOIN HOS.PatientMaster pm ON pm.PaitentId=sm.PatientId
                 LEFT OUTER JOIN HOS.Department hd ON hd.DId=sm.HDepartmentId
                 LEFT OUTER JOIN AMS.MemberShipSetup ms ON ms.MShipId=sm.MShipId
                 LEFT OUTER JOIN AMS.TableMaster tm ON tm.TableId=sm.TableId
                 sm.SB_Invoice = '{voucherNo}'";
        return cmdString;
    }

    public string GetSalesInvoiceTerm(string voucherNo)
    {
        var cmdString = $@"
            SELECT sm.SB_Invoice, sm.Invoice_Date, sm.Invoice_Miti, sm.PB_Vno, gl.GLName, gl.NepaliDesc, gl.PanNo, gl.GLAddress, gl.GLCode, gl.ACCode, gl.GLType, sm.PartyLedgerId, sm.Party_Name, sm.Vat_No, sm.Contact_Person, sm.Mobile_No, sm.Address, sm.ChqNo, sm.ChqDate, sm.ChqMiti, sm.Invoice_Type, sm.Invoice_Mode, sm.Payment_Mode, sm.DueDays, sm.DueDate, ja.AgentName, sl.SLName SubLedgerName, sm.SO_Invoice, sm.SO_Date, sm.SC_Invoice, sm.SC_Date, sm.Cls1, sm.Cls2, sm.Cls3, sm.Cls4, c.CName CounterName, c.CCode CurrencyName, sm.Cur_Rate, sm.B_Amount, sm.T_Amount, sm.N_Amount, sm.LN_Amount, sm.V_Amount, sm.Tbl_Amount, sm.Tender_Amount, sm.Return_Amount, sm.Action_Type, sm.In_Words, sm.Remarks, sm.Is_Printed, sm.No_Print, sm.Printed_By, sm.Printed_Date, sm.Enter_By, sm.Enter_Date, d.DrName DoctorName, pm.PaitentDesc PatientName, pm.ContactNo PaitentContactNo, pm.TAddress PaitentTAddress, hd.DName HospitalDepartmentName, ms.MShipDesc MemberName, ms.MemberId, tm.TableName TableName
            FROM AMS.SB_Master sm
                 LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID=sm.Customer_Id
                 LEFT OUTER JOIN AMS.GeneralLedger pl ON pl.GLID=sm.PartyLedgerId
                 LEFT OUTER JOIN AMS.JuniorAgent ja ON ja.AgentId=gl.AgentId
                 LEFT OUTER JOIN AMS.SubLedger sl ON sl.SLId=sm.Subledger_Id
                 LEFT OUTER JOIN AMS.Counter c ON c.CId=sm.CounterId
                 LEFT OUTER JOIN HOS.Doctor d ON d.DrId=sm.DoctorId
                 LEFT OUTER JOIN HOS.PatientMaster pm ON pm.PaitentId=sm.PatientId
                 LEFT OUTER JOIN HOS.Department hd ON hd.DId=sm.HDepartmentId
                 LEFT OUTER JOIN AMS.MemberShipSetup ms ON ms.MShipId=sm.MShipId
                 LEFT OUTER JOIN AMS.TableMaster tm ON tm.TableId=sm.TableId
                 sm.SB_Invoice = '{voucherNo}'";
        return cmdString;
    }

    public string GetSalesInvoiceProductTerm(string voucherNo)
    {
        var cmdString = $@"
            SELECT sm.SB_Invoice, sm.Invoice_Date, sm.Invoice_Miti, sm.PB_Vno, gl.GLName, gl.NepaliDesc, gl.PanNo, gl.GLAddress, gl.GLCode, gl.ACCode, gl.GLType, sm.PartyLedgerId, sm.Party_Name, sm.Vat_No, sm.Contact_Person, sm.Mobile_No, sm.Address, sm.ChqNo, sm.ChqDate, sm.ChqMiti, sm.Invoice_Type, sm.Invoice_Mode, sm.Payment_Mode, sm.DueDays, sm.DueDate, ja.AgentName, sl.SLName SubLedgerName, sm.SO_Invoice, sm.SO_Date, sm.SC_Invoice, sm.SC_Date, sm.Cls1, sm.Cls2, sm.Cls3, sm.Cls4, c.CName CounterName, c.CCode CurrencyName, sm.Cur_Rate, sm.B_Amount, sm.T_Amount, sm.N_Amount, sm.LN_Amount, sm.V_Amount, sm.Tbl_Amount, sm.Tender_Amount, sm.Return_Amount, sm.Action_Type, sm.In_Words, sm.Remarks, sm.Is_Printed, sm.No_Print, sm.Printed_By, sm.Printed_Date, sm.Enter_By, sm.Enter_Date, d.DrName DoctorName, pm.PaitentDesc PatientName, pm.ContactNo PaitentContactNo, pm.TAddress PaitentTAddress, hd.DName HospitalDepartmentName, ms.MShipDesc MemberName, ms.MemberId, tm.TableName TableName
            FROM AMS.SB_Master sm
                 LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID=sm.Customer_Id
                 LEFT OUTER JOIN AMS.GeneralLedger pl ON pl.GLID=sm.PartyLedgerId
                 LEFT OUTER JOIN AMS.JuniorAgent ja ON ja.AgentId=gl.AgentId
                 LEFT OUTER JOIN AMS.SubLedger sl ON sl.SLId=sm.Subledger_Id
                 LEFT OUTER JOIN AMS.Counter c ON c.CId=sm.CounterId
                 LEFT OUTER JOIN HOS.Doctor d ON d.DrId=sm.DoctorId
                 LEFT OUTER JOIN HOS.PatientMaster pm ON pm.PaitentId=sm.PatientId
                 LEFT OUTER JOIN HOS.Department hd ON hd.DId=sm.HDepartmentId
                 LEFT OUTER JOIN AMS.MemberShipSetup ms ON ms.MShipId=sm.MShipId
                 LEFT OUTER JOIN AMS.TableMaster tm ON tm.TableId=sm.TableId
                 sm.SB_Invoice = '{voucherNo}'";
        return cmdString;
    }
}