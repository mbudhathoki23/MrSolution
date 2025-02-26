using MrDAL.Domains.CRM.Master.Model;
using System.Data;

namespace MrDAL.Domains.CRM.Master;

public interface ICrmMaster
{
    //OBJECT FOR THIS FORM
    ClientCollection ClientInfo { get; set; }

    ClientSource ClientSource { get; set; }

    // INT VALUE
    int SaveClientSource();

    int SaveClientCollection();

    // LONG VALUE
    long ReturnMaxIdFromTable(string module);

    long GetIdFromValue(string val, string module);

    // BOOLEAN VALUE
    bool AlreadyExits(string value, string module, string column);

    //STRING VALUE
    string GetValueFromId(long id, string module);

    //DATA TABLE VALUE
    DataTable GetClientSource(string description);

    DataTable GetClientCollectionInformation(long id);
}