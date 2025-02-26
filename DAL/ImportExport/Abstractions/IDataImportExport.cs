using MrDAL.Models.Common;
using System.Data;
using System.IO;

namespace MrDAL.ImportExport.Abstractions;

public interface IDataImportExport
{
    NonQueryResult ExportToFile(string filename);

    ListResult<T> Load<T>(string filename);

    ListResult<T> Load<T>(DataTable dataTable, string filename);

    ListResult<T> Load<T>(Stream stream);
}