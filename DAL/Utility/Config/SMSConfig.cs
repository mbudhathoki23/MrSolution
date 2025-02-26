using System.Collections.Specialized;
using System.Diagnostics.Contracts;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MrDAL.Utility.Config;

public class SMSConfig
{
    private static async Task Main(string[] args)
    {
        var responseTest = await SendSmsAsync(string.Empty, string.Empty, string.Empty);
    }

    public static Task<string> SendSmsAsync(string token, string to, string text)
    {
        Contract.Ensures(Contract.Result<Task<string>>() != null);
        var client = new WebClient();
        var values = new NameValueCollection { ["auth_token"] = token, ["to"] = to, ["text"] = text };
        var response = client.UploadValues("http://aakashsms.com/admin/public/sms/v3/send/", "Post", values);
        var responseString = Encoding.Default.GetString(response);
        return Task.FromResult(responseString);
    }
}