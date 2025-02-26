using DatabaseModule.Setup.DocumentNumberings;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using System.Data;
using System.Threading.Tasks;

namespace MrDAL.Master.Interface.SystemSetup;

public interface IDocumentNumberingRepository
{
    // INSERT UPDATE DELETE
    int SaveDocumentNumbering(string actionTag);
    Task<bool> PullDocumentNumberingServerToClientByRowCount(IDataSyncRepository<DocumentNumbering> membershipSetupRepo, int callCount);
    string GetDocumentNumberingScript(int docId = 0);
    // RETURN VALUE IN DATA TABLE
    DataTable GetMasterDocumentNumbering(string actionTag, string category, int selectedId = 0);

    // OBJECT FOR THIS FORM
    DocumentNumbering ObjDocumentNumbering { get; set; }
}