using System.ComponentModel;

namespace MrDAL.Models.Custom;

public enum SalesInvoiceActionTag
{
    [Description("NEW")] New,

    [Description("Return")] Return,

    [Description("CANCEL")] Cancel
}