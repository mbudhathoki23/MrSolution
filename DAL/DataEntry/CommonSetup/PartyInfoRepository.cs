using MrDAL.Core.Extensions;
using MrDAL.DataEntry.Interface.Common;
using System.Data;

namespace MrDAL.DataEntry.CommonSetup;

public class PartyInfoRepository : IPartyInfoRepository
{
    public PartyInfoRepository()
    {

    }

    // RETURN VALUE IN DATA TABLE
    public DataTable GetPartyInfo()
    {
        var dtPartyInfo = new DataTable();
        dtPartyInfo.AddStringColumns(new[]
        {
            "PartyLedgerId",
            "PartyName",
            "ChequeNo",
            "ChequeDate",
            "ChequeMiti",
            "VatNo",
            "ContactPerson",
            "Address",
            "City",
            "Mob",
            "Email"
        });
        return dtPartyInfo;
    }

}