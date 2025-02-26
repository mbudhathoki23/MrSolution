using System.Net;
using System.Threading.Tasks;

namespace MrDAL.Core.Utils;

public class NetUtils
{
    public static async Task<bool> InternetConnectedAsync()
    {
        var result = await Task.Run(() =>
        {
            try
            {
                using var client = new WebClient { Proxy = null };
                using (client.OpenRead("http://google.com/generate_204"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        });

        return result;
    }
}