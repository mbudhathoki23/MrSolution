using System;
using System.Collections.Generic;
using System.Linq;

namespace MrDAL.Domains.Shared.AppLogger.Models;

public static class AppLogTypeCollection
{
    private static readonly List<AppLogTypeModel> CachedList = new();

    public static IList<AppLogTypeModel> GetAppLogTypes()
    {
        if (CachedList.Any()) return CachedList;

        CachedList.Clear();
        var values = Enum.GetValues(typeof(AppLogType)).OfType<AppLogType>().ToList();

        CachedList.AddRange(values.Where(x => (short)x >= 101 && (short)x <= 199)
            .Select(x => new AppLogTypeModel(x, AppLogGroup.Administrative)));
        CachedList.AddRange(values.Where(x => (short)x >= 201 && (short)x <= 299)
            .Select(x => new AppLogTypeModel(x, AppLogGroup.Pos)));
        CachedList.AddRange(values.Where(x => (short)x >= 301 && (short)x <= 399)
            .Select(x => new AppLogTypeModel(x, AppLogGroup.Inventory)));
        CachedList.AddRange(values.Where(x => (short)x >= 401 && (short)x <= 499)
            .Select(x => new AppLogTypeModel(x, AppLogGroup.Account)));
        CachedList.AddRange(values.Where(x => (short)x >= 501 && (short)x <= 599)
            .Select(x => new AppLogTypeModel(x, AppLogGroup.Master)));
        CachedList.AddRange(values.Where(x => (short)x >= 801 && (short)x <= 899)
            .Select(x => new AppLogTypeModel(x, AppLogGroup.Other)));

        return CachedList;
    }

    public static AppLogTypeModel GetAppLogType(AppLogType logType)
    {
        if (CachedList == null || !CachedList.Any()) GetAppLogTypes();
        return CachedList?.FirstOrDefault(x => x.LogTypeE == logType);
    }
}