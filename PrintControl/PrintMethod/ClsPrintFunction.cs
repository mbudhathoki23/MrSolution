using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Global.Common;
using MrDAL.Utility.dbMaster;
using MrDAL.Utility.Server;
using System;
using PrintControl.PrintClass.DirectPrint;

namespace PrintControl.PrintMethod;

public class ClsPrintFunction
{
    public void PrintDirectSalesInvoice(string module, decimal billAmount, string printBillNo, string defaultPrinter, string counter = "", string getDesign = "", short noOfPrint = 1)
    {
        try
        {
            CreateDatabaseTable.DropTrigger();
            if (!string.IsNullOrEmpty(counter))
            {
                var cmd = $"SELECT c.Printer FROM AMS.Counter c WHERE c.CCode='{counter}'";
                defaultPrinter = GetConnection.GetQueryData(cmd);
            }

            var defaultDesign = string.Empty;
            if (getDesign.IsBlankOrEmpty())
            {
                if (module.Equals("SR"))
                {
                    defaultDesign = billAmount > 10000 ? "DefaultReturnInvoiceWithVAT" : "DefaultReturnInvoice";
                }
            }
            else
            {
                defaultDesign = getDesign;
            }

            #region --------------- Direct Print ---------------

            if (defaultDesign is "DefaultBarcode")
            {
                var bill = new PrintSalesBill
                {
                    BillNo = printBillNo,
                    Printer = defaultPrinter,
                    Printdesign = defaultDesign,
                    PrintedBy = ObjGlobal.LogInUser,
                    NoofPrint = noOfPrint,
                    PDiscountId = ObjGlobal.SalesDiscountTermId.ToString(),
                    BDiscountId = ObjGlobal.SalesSpecialDiscountTermId.ToString(),
                    ServiceChargeId = ObjGlobal.SalesServiceChargeTermId.ToString(),
                    SalesVatTermId = ObjGlobal.SalesVatTermId.ToString()
                };
                bill.PrintDocumentDesign();
            }
            else
            {
                var bill = new PrintSalesBill
                {
                    BillNo = printBillNo,
                    Printer = defaultPrinter,
                    Printdesign = defaultDesign,
                    PrintedBy = ObjGlobal.LogInUser,
                    NoofPrint = noOfPrint,
                    PDiscountId = ObjGlobal.SalesDiscountTermId.ToString(),
                    BDiscountId = ObjGlobal.SalesSpecialDiscountTermId.ToString(),
                    ServiceChargeId = ObjGlobal.SalesServiceChargeTermId.ToString(),
                    SalesVatTermId = ObjGlobal.SalesVatTermId.ToString()
                };
                bill.PrintDocumentDesign();
            }

            CreateDatabaseTable.CreateTrigger();

            #endregion --------------- Direct Print ---------------
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
        }
    }
}