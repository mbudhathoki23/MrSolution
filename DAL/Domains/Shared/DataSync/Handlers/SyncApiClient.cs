using MrDAL.Domains.Shared.DataSync.Common;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace MrDAL.Domains.Shared.DataSync.Handlers;

public sealed class SyncApiClient : HttpClient
{
    public SyncApiClient() : base(new CompressHandler(), true)
    {
        try
        {
            var config = DataSyncHelper.GetConfig();
            if (config == null)
                throw new InvalidOperationException("ApiConfiguration value not set.");

            BaseAddress = new Uri(config.BaseUrl);

            Timeout = TimeSpan.FromSeconds(600);
            DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));

            if (string.IsNullOrWhiteSpace(config.Username)
                && !string.IsNullOrWhiteSpace(config.Password))
            {
                var byteArray = Encoding.ASCII.GetBytes($@"{config.Username}:{config.Password}");
                DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            }

            DefaultRequestHeaders.Add("apiKey", Convert.ToString(config.Apikey));
            DefaultRequestHeaders.Add("userName", config.Username);
            DefaultRequestHeaders.Add("branchId", config.BranchId.ToString());
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}