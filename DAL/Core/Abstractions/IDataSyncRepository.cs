using MrDAL.Models.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrDAL.Core.Abstractions;

public interface IDataSyncRepository<T>
{
    /// <summary>
    ///     Gets all the data from external origin, doesn't matter if those data have already been synced with the local
    ///     database
    /// </summary>
    /// <returns></returns>
    Task<InfoResult<T>> GetExternalDataAsync();

    /// <summary>
    ///     Gets all the data stored in the local database. The data contains records from both local origin and external
    ///     server if they have been synced.
    /// </summary>
    /// <returns></returns>
    Task<InfoResult<T>> GetLocalDataAsync();

    /// <summary>
    ///     Gets all the data stored in the local database. The data contains records from both local origin and external
    ///     server if they have been synced.
    /// </summary>
    /// <returns></returns>
    Task<ListResult<T>> GetUnSynchronizedDataAsync();

    /// <summary>
    ///     Gets all the data from local origin, doesn't matter if those data have already been pushed to server
    /// </summary>
    /// <returns></returns>
    Task<InfoResult<T>> GetLocalOriginDataAsync();

    /// <summary>
    ///     Fetches external data and compares with the provided local data and
    ///     filters to have only the data that can be pulled to local database
    /// </summary>
    /// <param name="localData">Data from local origin</param>
    /// <returns></returns>
    Task<InfoResult<T>> GetIncomingNewDataAsync(T localData);

    Task<InfoResult<T>> GetIncomingPatchedDataAsync(T localData);

    /// <summary>
    ///     Fetches local origin data and compares with the provided external data and
    ///     filters to have only the data that can be pushed to external database
    /// </summary>
    /// <param name="externalData">Data from external origin</param>
    /// <returns></returns>
    Task<InfoResult<T>> GetOutgoingNewDataAsync(T externalData);

    Task<InfoResult<T>> GetOutgoingPatchedDataAsync(T externalData);

    /// <summary>
    ///     Saves the provided data in the database as external data being imported or merged
    /// </summary>
    /// <param name="newData">The data to be saved in the database</param>
    /// <returns></returns>
    Task<NonQueryResult> PullNewAsync(T newData);

    Task<NonQueryResult> PullPatchedAsync(T patchedData);

    /// <summary>
    ///     Pushes the provided local data to remote or external database. This method exports the data.
    /// </summary>
    /// <param name="localData">The data to be pushed or exported</param>
    /// <returns></returns>
    Task<NonQueryResult> PushNewAsync(T localData);

    Task<NonQueryResult> PushNewListAsync(List<T> localData);

    /// <summary>
    ///     update the provided local data to remote or external database. This method update the data.
    /// </summary>
    /// <param name="localData">The data to be updateed</param>
    /// <returns></returns>
    Task<NonQueryResult> PutNewAsync(T localData);

    Task<NonQueryResult> PutNewListAsync(List<T> localData);

    /// <summary>
    ///     update the provided local data to remote or external database. This method update the data.
    /// </summary>
    /// <param name="localData">The data to be updateed</param>
    /// <returns></returns>
    Task<NonQueryResult> DeleteNewAsync();

    /// <summary>
    ///     Pulls all the new data from external server and saves into the local database
    /// </summary>
    /// <returns></returns>
    Task<NonQueryResult> PullAllNewAsync();

    Task<NonQueryResult> PullAllPatchedAsync();

    /// <summary>
    ///     Pushes all the new data in the local db server that are not present in the external server
    /// </summary>
    /// <returns></returns>
    Task<NonQueryResult> PushAllNewAsync();

    Task<NonQueryResult> PushAllPatchedAsync();
}