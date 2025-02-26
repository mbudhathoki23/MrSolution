using System.ComponentModel;

namespace DatabaseModule.DataEntry.SalesMaster.SalesInvoice;

public enum SalesInvoicePaymentMode
{
    [Description("Cash")] Cash,

    [Description("Credit")] Credit,

    [Description("Card")] Card
}