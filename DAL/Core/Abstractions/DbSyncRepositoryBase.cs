using DatabaseModule.CloudSync;
using Ionic.Zip;
using Ionic.Zlib;
using MrDAL.Core.Utils;
using MrDAL.Domains.Shared.DataSync.Common;
using MrDAL.Domains.Shared.DataSync.Handlers;
using MrDAL.Models.Common;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
// ReSharper disable All

namespace MrDAL.Core.Abstractions;

public abstract class DbSyncRepositoryBase
{
    protected readonly DbSyncRepoInjectData InjectData;

    protected DbSyncRepositoryBase(DbSyncRepoInjectData injectData)
    {
        InjectData = injectData;
    }

    /// <summary>
    ///     Gets all the data from external origin, doesn't matter if those data have already been synced with the local
    ///     database
    /// </summary>
    /// <typeparam name="T">The type of data to be returned. The T instance must have inherited from class 'BaseSyncData'</typeparam>
    /// <param name="repoType">The sync repository type defining what type of data to be fetched</param>
    /// <returns></returns>
    protected async Task<InfoResult<T>> GetExternalDataAsync<T>(SyncRepoType repoType) where T : BaseSyncData
    {
        var result = new InfoResult<T>();

        var apiClient = new SyncApiClient();
        try
        {
            var url = $@"{InjectData.ApiConfig.GetUrl}?localOriginId={InjectData.LocalOriginId}&repoType={repoType}";
            var response = await apiClient?.GetAsync(url);

            var stream = await response.Content.ReadAsStreamAsync();
            stream.Seek(0, SeekOrigin.Begin);

            var zip = ZipFile.Read(stream);
            if (!zip.Entries.Any())
            {
                zip.Dispose();
                stream.Dispose();

                result.ErrorMessage = "Invalid response from server.";
                result.ResultType = ResultType.InternalError;
                return result;
            }

            var jsonFile = zip.Entries.First();
            var jsonItemStream = new MemoryStream();
            jsonFile.Extract(jsonItemStream);

            jsonItemStream.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(jsonItemStream);
            var apiJson = await reader.ReadToEndAsync();

            result = JsonConvert.DeserializeObject<InfoResult<T>>(apiJson);

            jsonItemStream.Dispose();
            reader.Dispose();
            zip.Dispose();
            stream.Dispose();
        }
        catch (Exception e)
        {
            result = e.ToInfoErrorResult<T>(this);
        }
        finally
        {
            apiClient.Dispose();
        }

        return result;
    }

    /// <summary>
    ///     Gets all the data from external origin, doesn't matter if those data have already been synced with the local
    ///     database
    /// </summary>
    /// <typeparam name="T">The type of data to be returned. The T instance must have inherited from class 'BaseSyncData'</typeparam>
    /// <param name="repoType">The sync repository type defining what type of data to be fetched</param>
    /// <returns></returns>
    protected async Task<ListResult<T>> GetUnSynchronizedDataAsync<T>() where T : BaseSyncData
    {
        var result = new ListResult<T>();

        var apiClient = new SyncApiClient();
        try
        {
            var response = await apiClient.GetAsync(InjectData.ApiConfig.GetUrl);
            var json = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode) result = JsonConvert.DeserializeObject<ListResult<T>>(json);

            //jsonItemStream.Dispose();
            //reader.Dispose();
            //zip.Dispose();
            //stream.Dispose();
        }
        catch (Exception e)
        {
            result = e.ToListErrorResult<T>(this);
        }
        finally
        {
            apiClient.Dispose();
        }

        return result;
    }

    /// <summary>
    ///     Pushes the provided local data to remote or external database. This method exports the data.
    /// </summary>
    /// <param name="repoType">The sync repository type defining what type of data to be pushed or exported</param>
    /// <param name="localData">The data to be pushed or exported</param>
    /// <returns></returns>
    protected async Task<NonQueryResult> PushAsync(SyncRepoType repoType, object localData)
    {
        NonQueryResult result;

        var client = new SyncApiClient();
        try
        {
            var model = new DataSyncPushModel
            {
                LocalOriginId = InjectData.LocalOriginId,
                RepoType = repoType.ToString(),
                Data = localData
            };

            var (content, jsonPayload) = JsonUtils.ToGZipJsonStreamContent(model);
            var payloadLength = jsonPayload.Length * sizeof(char) + sizeof(int);

            HttpResponseMessage response;

            // if size greater than 300kb, zip it and send to the api
            // otherwise send the data as it is
            if (payloadLength >= 307200)
            {
                var mStream = new MemoryStream();
                var zipFile = new ZipFile
                {
                    CompressionLevel = CompressionLevel.BestCompression
                };
                zipFile.AddEntry("model.json", jsonPayload);
                zipFile.Save(mStream);

                mStream.Seek(0, SeekOrigin.Begin);
                var rContent = new StreamContent(mStream);
                rContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                response = await client.PostAsync(InjectData.ApiConfig.InsertUrl, rContent);

                rContent.Dispose();
                mStream.Dispose();
                zipFile.Dispose();
            }
            else
            {
                response = await client.PostAsync(InjectData.ApiConfig.InsertUrl, content);
            }

            var json = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<NonQueryResult>(json);

            result = new NonQueryResult(false, false, json, ResultType.InternalError);
        }
        catch (Exception e)
        {
            result = e.ToNonQueryErrorResult(e.StackTrace);
        }
        finally
        {
            client.Dispose();
        }

        return result;
    }

    /// <summary>
    ///     Pushes the provided local data to remote or external database. This method exports the data.
    /// </summary>
    /// <param name="localData">The data to be pushed or exported</param>
    /// <returns></returns>
    protected async Task<NonQueryResult> PushAsync(object localData)
    {
        NonQueryResult result;

        var client = new SyncApiClient();
        try
        {
            var content = JsonUtils.ToJsonStringContent(localData);

            var response = await client.PostAsync(InjectData.ApiConfig.InsertUrl, content);

            var json = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<NonQueryResult>(json);

            result = new NonQueryResult(false, false, json, ResultType.InternalError);
        }
        catch (Exception e)
        {
            result = e.ToNonQueryErrorResult(e.StackTrace);
        }
        finally
        {
            client.Dispose();
        }

        return result;
    }

    /// <summary>
    ///     Pushes the provided local data to remote or external database. This method exports the data.
    /// </summary>
    /// <param name="localData">The data to be pushed or exported</param>
    /// <returns></returns>
    protected async Task<NonQueryResult> PushListAsync(object localData)
    {
        NonQueryResult result;

        var client = new SyncApiClient();
        try
        {
            var (content, jsonPayload) = JsonUtils.ToGZipJsonStreamContent(localData);
            var payloadLength = jsonPayload.Length * sizeof(char) + sizeof(int);

            HttpResponseMessage response;

            ////if size greater than 300kb, zip it and send to the api
            ////otherwise send the data as it is
            //if (payloadLength >= 307200)
            //{
            //    var mStream = new MemoryStream();
            //    var zipFile = new ZipFile
            //    {
            //        CompressionLevel = CompressionLevel.BestCompression
            //    };
            //    zipFile.AddEntry("model.json", jsonPayload);
            //    zipFile.Save(mStream);

            //    mStream.Seek(0, SeekOrigin.Begin);
            //    var rContent = new StreamContent(mStream);
            //    rContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            //    response = await client.PostAsync(InjectData.ApiConfig.InsertUrl, rContent);

            //    rContent.Dispose();
            //    mStream.Dispose();
            //    zipFile.Dispose();
            //}
            //else
            //{
            response = await client.PostAsync(InjectData.ApiConfig.InsertUrl, content);
            //}

            var json = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<NonQueryResult>(json);

            result = new NonQueryResult(false, false, json, ResultType.InternalError);
        }
        catch (Exception e)
        {
            result = e.ToNonQueryErrorResult(e.StackTrace);
        }
        finally
        {
            client.Dispose();
        }

        return result;
    }

    /// <summary>
    ///     Patches the provided local data to remote or external database. This method exports the data.
    /// </summary>
    /// <param name="repoType">The sync repository type defining what type of data to be patched or exported</param>
    /// <param name="localData">The data to be pushed or exported</param>
    /// <returns></returns>
    protected async Task<NonQueryResult> PatchAsync(SyncRepoType repoType, object localData)
    {
        NonQueryResult result;

        var client = new SyncApiClient();
        try
        {
            var model = new DataSyncPushModel
            {
                LocalOriginId = InjectData.LocalOriginId,
                RepoType = repoType.ToString(),
                Data = localData
            };

            var (content, jsonPayload) = JsonUtils.ToGZipJsonStreamContent(model);
            var payloadLength = jsonPayload.Length * sizeof(char) + sizeof(int);

            HttpResponseMessage response;

            // if size greater than 300kb, zip it and send to the api
            // otherwise send the data as it is
            if (payloadLength >= 307200)
            {
                var mStream = new MemoryStream();
                var zipFile = new ZipFile
                {
                    CompressionLevel = CompressionLevel.BestCompression
                };
                zipFile.AddEntry("model.json", jsonPayload);
                zipFile.Save(mStream);

                mStream.Seek(0, SeekOrigin.Begin);
                var rContent = new StreamContent(mStream);
                rContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                response = await client.PostAsync(InjectData.ApiConfig.UpdateUrl, rContent);

                rContent.Dispose();
                mStream.Dispose();
                zipFile.Dispose();
            }
            else
            {
                response = await client.PostAsync(InjectData.ApiConfig.UpdateUrl, content);
            }

            var json = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<NonQueryResult>(json);

            result = new NonQueryResult(false, false, json, ResultType.InternalError);
        }
        catch (Exception e)
        {
            result = e.ToNonQueryErrorResult(e.StackTrace);
        }
        finally
        {
            client.Dispose();
        }

        return result;
    }

    /// <summary>
    ///     update the provided local data to remote or external database. This method updates the data.
    /// </summary>
    /// <param name="localData">The data to be updated</param>
    /// <returns></returns>
    protected async Task<NonQueryResult> PutAsync(object localData)
    {
        NonQueryResult result;

        var client = new SyncApiClient();
        try
        {
            var content = JsonUtils.ToJsonStringContent(localData);

            HttpResponseMessage response;
            response = await client.PutAsync(InjectData.ApiConfig.UpdateUrl, content);

            var json = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<NonQueryResult>(json);

            result = new NonQueryResult(false, false, json, ResultType.InternalError);
        }
        catch (Exception e)
        {
            result = e.ToNonQueryErrorResult(e.StackTrace);
        }
        finally
        {
            client.Dispose();
        }

        return result;
    }

    /// <summary>
    ///     update the provided local data to remote or external database. This method updates the data.
    /// </summary>
    /// <param name="localData">The data to be updated</param>
    /// <returns></returns>
    protected async Task<NonQueryResult> PutListAsync(object localData)
    {
        NonQueryResult result;

        var client = new SyncApiClient();
        try
        {
            //    var model = new DataSyncPushModel
            //    {
            //        LocalOriginId = InjectData.LocalOriginId,
            //        RepoType = repoType.ToString(),
            //        Data = localData
            //    };

            var (content, jsonPayload) = JsonUtils.ToGZipJsonStreamContent(localData);
            var payloadLength = jsonPayload.Length * sizeof(char) + sizeof(int);

            HttpResponseMessage response;

            // if size greater than 300kb, zip it and send to the api
            // otherwise send the data as it is
            if (payloadLength >= 307200)
            {
                var mStream = new MemoryStream();
                var zipFile = new ZipFile
                {
                    CompressionLevel = CompressionLevel.BestCompression
                };
                zipFile.AddEntry("model.json", jsonPayload);
                zipFile.Save(mStream);

                mStream.Seek(0, SeekOrigin.Begin);
                var rContent = new StreamContent(mStream);
                rContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                response = await client.PutAsync(InjectData.ApiConfig.UpdateUrl, rContent);

                rContent.Dispose();
                mStream.Dispose();
                zipFile.Dispose();
            }
            else
            {
                response = await client.PutAsync(InjectData.ApiConfig.UpdateUrl, content);
            }

            var json = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<NonQueryResult>(json);

            result = new NonQueryResult(false, false, json, ResultType.InternalError);
        }
        catch (Exception e)
        {
            result = e.ToNonQueryErrorResult(e.StackTrace);
        }
        finally
        {
            client.Dispose();
        }

        return result;
    }

    /// <summary>
    ///     delete the provided local data to remote or external database. This method deletes the data.
    /// </summary>
    /// <param name="localData">The data to be deleted</param>
    /// <returns></returns>
    protected async Task<NonQueryResult> DeleteAsync()
    {
        NonQueryResult result;

        var client = new SyncApiClient();
        try
        {
            var response = await client.DeleteAsync(InjectData.ApiConfig.DeleteUrl);
            var json = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<NonQueryResult>(json);

            result = new NonQueryResult(false, false, json, ResultType.InternalError);
        }
        catch (Exception e)
        {
            result = e.ToNonQueryErrorResult(e.StackTrace);
        }
        finally
        {
            client.Dispose();
        }

        return result;
    }
}