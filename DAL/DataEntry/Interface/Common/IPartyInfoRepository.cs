using System.Data;

namespace MrDAL.DataEntry.Interface.Common;

public interface IPartyInfoRepository
{
    // RETURN VALUE IN DATA TABLE
    DataTable GetPartyInfo();
}