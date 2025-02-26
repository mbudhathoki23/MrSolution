using MrDAL.Domains.Shared.SmsApi.Models;
using System;
using System.Net;
using System.Net.Http;

namespace MrDAL.Domains.Shared.SmsApi.Handlers;

public sealed class SmsHttpClient : HttpClient
{
    public SmsHttpClient(SmsApiConfigModel config) : base(new HttpClientHandler
    {
        AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
    }, true)
    {
        BaseAddress = new Uri(config.BaseUrl);
    }
}