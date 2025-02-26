using System.ComponentModel;

namespace MrDAL.Master.Interface;

public enum EntryModule
{
    [Description("JOURNAL VOUCHER")] JV = 1,
    [Description("CASH/BANK VOUCHER")] CB = 2,
    [Description("RECEIPT VOUCHER")] RV = 3,
    [Description("PAYMENT VOUCHER")] PV = 4,
    [Description("CONTRA VOUCHER")] CV = 5,

    [Description("RECEIPT/PAYMENT VOUCHER")]
    RPV = 6,
    [Description("DEBIT NOTE")] DN = 7,
    [Description("CREDIT NOTE")] CN = 8,
    [Description("PURCHASE INDENT")] PIN = 9,
    [Description("PURCHASE ORDER")] PO = 10,

    [Description("PURCHASE ORDER CANCELLATION")]
    POC = 11,
    [Description("PURCHASE CHALLAN")] PC = 12,

    [Description("PURCHASE CHALLAN CANCELLATION")]
    PCC = 13,

    [Description("PURCHASE QUALITY CONTROL")]
    PQC = 14,
    [Description("PURCHASE INVOICE")] PB = 15,

    [Description("PURCHASE ADDITIONAL INVOICE")]
    PAB = 16,

    [Description("PURCHASE RETURN INVOICE")]
    PR = 17,

    [Description("PURCHASE EXPIRY/BREAKAGE")]
    PEB = 18,

    [Description("SALES QUOTATION")] SQ = 19,

    [Description("SALES QUOTATION CANCELLATION")]
    SQC = 20,

    [Description("SALES ORDER")] SO = 21,
    [Description("RESTRO ORDER")] RSO = 22,
    [Description("SALES ORDER CANCEL")] SOC = 23,
    [Description("SALES CHALLAN")] SC = 24,
    [Description("SALES CHALLAN CANCEL")] SCC = 25,
    [Description("SALES INVOICE")] SB = 26,

    [Description("SALES INVOICE CANCELLATION")]
    SBC = 27,
    [Description("RESTAURANT INVOICE")] RSB = 28,

    [Description("RESTAURANT INVOICE CANCELLATION")]
    RSBC = 29,
    [Description("POINT OF SALES")] POS = 30,

    [Description("ABBREVIATED TAX INVOICE")]
    ATI = 31,

    [Description("SALES ADDITIONAL INVOICE")]
    SAB = 32,
    [Description("SALES RETURN")] SR = 33,

    [Description("SALES RETURN CANCELLLATION")]
    SRC = 34,
    [Description("SALES EXPIRY/BREAKAGE")] SEB = 35,
    [Description("STOCK TRANSFER")] ST = 36,
    [Description("STOCK ADJUSTMENT")] SA = 37,
    [Description("STOCK EXPIRY/BREAKAGE")] STEB = 38,
    [Description("INVENTORY MEMO")] BOM = 39,
    [Description("INVENTORY ISSUE")] MI = 40,

    [Description("INVENTORY ISSUE RETURN")]
    MIR = 41,
    [Description("INVENTORY RECEIVE")] MR = 42,

    [Description("INVENTORY RECEIVE RETURN")]
    MRR = 43,
    [Description("PRODUCT MEMO")] IBOM = 44,
    [Description("RAW MATERIAL ISSUE")] RMI = 45,

    [Description("RAW MATERIAL ISSUE RETURN")]
    RMIR = 46,

    [Description("FINISHED GOODS RECEIVE")]
    FGR = 47,

    [Description("FINISHED GOODS RECEIVE RETURN")]
    FGRR = 48,
    [Description("DAY CLOSING")] DC = 49
}