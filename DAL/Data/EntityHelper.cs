using MrDAL.Models.Common;
using System.Collections.Generic;
using System.Data;

namespace MrDAL.Data;

public class EntityHelper
{
    private static readonly List<EntityMetaData> MetaDataList = new()
    {
        new EntityMetaData("AMS.Product", "PID", typeof(long),
            SqlDbType.BigInt, true),
        new EntityMetaData("AMS.ProductGroup", "PGrpID", typeof(int),
            SqlDbType.Int, true),
        new EntityMetaData("AMS.ProductSubGroup", "PSubGrpId", typeof(int),
            SqlDbType.Int, true),
        new EntityMetaData("AMS.ProductUnit", "UID", typeof(int),
            SqlDbType.Int, true)
    };

    public static IReadOnlyList<EntityMetaData> GetMetaDataList()
    {
        return MetaDataList;
    }
}