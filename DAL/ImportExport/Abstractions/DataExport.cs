using MrDAL.Models.Common;
using System.Configuration.Provider;

namespace MrDAL.ImportExport.Abstractions;

public abstract class DataExport : ProviderBase
{
    public abstract NonQueryResult ExportToFile(string filename);

    public abstract void Load(string filename);
}