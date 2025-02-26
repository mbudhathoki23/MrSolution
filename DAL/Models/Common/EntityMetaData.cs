using System;
using System.Data;

namespace MrDAL.Models.Common;

public class EntityMetaData
{
    public EntityMetaData(string tableName, string pkColumn, Type pkColumnDataType,
        SqlDbType pkColumnSqlType, bool pkExplicit)
    {
        TableName = tableName;
        PkColumn = pkColumn;
        PkColumnSqlType = pkColumnSqlType;
        PkExplicit = pkExplicit;
        PkColumnDataType = pkColumnDataType;
    }

    public string TableName { get; set; }
    public string PkColumn { get; set; }
    public SqlDbType PkColumnSqlType { get; set; }
    public Type PkColumnDataType { get; set; }
    public bool PkExplicit { get; set; }
}