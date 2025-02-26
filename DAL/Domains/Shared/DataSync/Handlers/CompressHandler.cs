using System.Net;
using System.Net.Http;

namespace MrDAL.Domains.Shared.DataSync.Handlers;

public class CompressHandler : HttpClientHandler
{
    public CompressHandler()
    {
        AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
    }
}