using Ionic.Zip;
using Ionic.Zlib;
using MrDAL.Core.Logging;
using MrDAL.Core.Utils;
using MrDAL.Models.Common;
using MrDAL.Utility.Analytics.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
// ReSharper disable All

namespace MrDAL.Utility.Analytics;

public class AnalyticsService : IDisposable
{
    private const string GeoUrl = "http://ip-api.com/json";
    private const string LogPushUrl = "http://13.234.18.76:8080/analytics/log";
    private const string ImagePushUrl = "http://13.234.18.76:8080/analytics/log/analytics/log/img";


    private readonly AnalyticsHttpClient _httpClient;
    private readonly SqlLiteClientLogService _logService = new();

    public AnalyticsService()
    {
        _httpClient = new AnalyticsHttpClient();
    }

    public void Dispose()
    {
        _httpClient?.Dispose();
    }

    public async Task<NonQueryResult> SyncClientLogsAsync()
    {
        var result = new NonQueryResult();

        //var logsResponse = await _logService.GetLogsAsync(null, false);
        //if (!logsResponse.Success)
        //{
        //    result.ErrorMessage = "Error fetching logs from the persistence provider. " + logsResponse.ErrorMessage;
        //    result.ResultType = logsResponse.ResultType;
        //    return result;
        //}

        //if (!logsResponse.List.Any())
        //{
        //    result.Completed = result.Value = true;
        //    return result;
        //}

        //var geoInfo = string.Empty;

        //try
        //{
        //    var geoInfoRes = await _httpClient.GetAsync(GeoUrl);
        //    if (geoInfoRes.IsSuccessStatusCode)
        //    {
        //        geoInfo = await geoInfoRes.Content.ReadAsStringAsync();
        //    }

        //    var hwId = FingerPrint.GetMotherboardUuId();
        //    //var outletUqId = LogIn.Instance?.OutletDetail?.UniqId;

        //    var logs = logsResponse.List.Select(x => new ClientLogPushModel
        //    {
        //        ClientId = null,
        //        ClientLastUpdated = null,
        //        Dump = x.Dump?.Length > 7990 ? x.Dump.Substring(0, 7990) : x.Dump,
        //        GeoInfo = geoInfo,
        //        Message = x.Message,
        //        Id = x.Id,
        //        ImageId = x.ImageId,
        //        OtherData = x.OtherData,
        //        LogTime = x.LogTime,
        //        LogType = x.LogType,
        //        Machine = x.Machine,
        //        MachineUser = x.MachineUser,
        //        HwId = string.Empty,
        //        OutletUqId = null
        //    });

        //    var (content, jsonPayload) = JsonUtils.ToGZipJsonStreamContent(logs);
        //    var payloadLength = jsonPayload.Length * sizeof(char) + sizeof(int);

        //    HttpResponseMessage httpResponse;

        //    // if size greater than 300kb, zip it and send to the api
        //    // otherwise send the data as it is
        //    if (payloadLength >= 307200)
        //    {
        //        var mStream = new MemoryStream();
        //        var zipFile = new ZipFile
        //        {
        //            CompressionLevel = CompressionLevel.BestCompression
        //        };
        //        zipFile.AddEntry("data.json", jsonPayload);
        //        zipFile.Save(mStream);

        //        mStream.Seek(0, SeekOrigin.Begin);
        //        var rContent = new StreamContent(mStream);
        //        rContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

        //        httpResponse = await _httpClient.PostAsync(LogPushUrl, rContent);

        //        rContent.Dispose();
        //        mStream.Dispose();
        //        zipFile.Dispose();
        //    }
        //    else
        //    {
        //        httpResponse = await _httpClient.PostAsync(LogPushUrl, content);
        //    }

        //    if (httpResponse.IsSuccessStatusCode)
        //    {
        //        var json = await httpResponse.Content.ReadAsStringAsync();
        //        var listResult = JsonConvert.DeserializeObject<ListResult<Guid>>(json);

        //        if (listResult.List != null) await _logService.FlushSyncedLogsAsync(listResult.List.ToList());
        //        result.Completed = result.Value = listResult.Success;
        //    }
        //}
        //catch (Exception e)
        //{
        //    LogEngineFactory.GetLogEngine(LogEngineE.NLog).LogError(e, e.Message);
        //}

        //return result;

        return result;
    }

    public async Task<NonQueryResult> SyncBlobObjectsAsync()
    {
        var result = new NonQueryResult();

        //var imgResponse = await _logService.GetImagesAsync();
        //if (!imgResponse.Success)
        //{
        //    result.ErrorMessage = "Error fetching blob objects from the persistence provider. " +
        //                          imgResponse.ErrorMessage;
        //    result.ResultType = imgResponse.ResultType;
        //    return result;
        //}

        //if (!imgResponse.List.Any())
        //{
        //    result.Completed = result.Value = true;
        //    return result;
        //}

        //try
        //{
        //    var images = imgResponse.List.Select(x => new ClientImgPushModel
        //    {
        //        ClientId = null,
        //        ClientDateTime = x.DateTime,
        //        ClientImageId = x.Id,
        //        Content = Convert.ToBase64String(x.Image),
        //        Machine = x.Machine,
        //        MachineUser = x.MachineUser
        //    }).ToList();

        //    var (content, jsonPayload) = JsonUtils.ToGZipJsonStreamContent(images);
        //    var payloadLength = jsonPayload.Length * sizeof(char) + sizeof(int);

        //    HttpResponseMessage httpResponse;

        //    // if size greater than 300kb, zip it and send to the api
        //    // otherwise send the data as it is
        //    if (payloadLength >= 307200)
        //    {
        //        var mStream = new MemoryStream();
        //        var zipFile = new ZipFile
        //        {
        //            CompressionLevel = CompressionLevel.BestCompression
        //        };
        //        zipFile.AddEntry("data.json", jsonPayload);
        //        zipFile.Save(mStream);

        //        mStream.Seek(0, SeekOrigin.Begin);
        //        var rContent = new StreamContent(mStream);
        //        rContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

        //        httpResponse = await _httpClient.PostAsync(ImagePushUrl, rContent);

        //        rContent.Dispose();
        //        mStream.Dispose();
        //        zipFile.Dispose();
        //    }
        //    else
        //    {
        //        httpResponse = await _httpClient.PostAsync(ImagePushUrl, content);
        //    }

        //    if (httpResponse.IsSuccessStatusCode)
        //    {
        //        var json = await httpResponse.Content.ReadAsStringAsync();
        //        var listResult = JsonConvert.DeserializeObject<ListResult<Guid>>(json);

        //        if (listResult.List != null)
        //            await _logService.FlushSyncedBinaryContentsAsync(listResult.List.ToList());
        //        result.Completed = result.Value = listResult.Success;
        //    }
        //}
        //catch (Exception e)
        //{
        //    LogEngineFactory.GetLogEngine(LogEngineE.NLog).LogError(e, e.Message);
        //}

        return result;
    }

    public async Task SyncAllAsync()
    {
        if (!await NetUtils.InternetConnectedAsync())
        {
            return;
        }


        _ = SyncClientLogsAsync();
        _ = SyncBlobObjectsAsync();
    }

    public async Task<NonQueryResult> CleanUpAsync()
    {
        try
        {
            return await _logService.CleanAllAsync();
        }
        catch
        {
            await Task.CompletedTask;
        }
        return new NonQueryResult();
    }

    private class AnalyticsHttpClient : HttpClient
    {
        public AnalyticsHttpClient() : base(new AnalyticsHandler(), true)
        {
            Timeout = TimeSpan.FromMinutes(10);
        }
    }

    private class AnalyticsHandler : HttpClientHandler
    {
        public AnalyticsHandler()
        {
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
        }
    }
}