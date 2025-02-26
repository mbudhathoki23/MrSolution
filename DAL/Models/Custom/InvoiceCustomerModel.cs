namespace MrDAL.Models.Custom;

public class InvoiceCustomerModel
{
    public string PartyName { get; set; }
    public string Address { get; set; }
    public string VatNo { get; set; }
    public string Phone { get; set; }
    public string ContactPerson { get; set; }
    public int? LedgerId { get; set; }
}