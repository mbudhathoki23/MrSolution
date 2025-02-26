using Ionic.Zip;
using Ionic.Zlib;
using MrDAL.Core.Utils;
using MrDAL.Domains.Shared.SmsApi.Handlers;
using MrDAL.Domains.Shared.SmsApi.Models;
using MrDAL.Global.Common;
using MrDAL.Models.Common;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrDAL.Domains.Shared.SmsApi;

public class SmsApiService : IDisposable
{
    private readonly SmsApiConfigModel _config;
    private readonly SmsHttpClient _httpClient;

    public SmsApiService(SmsApiConfigModel config)
    {
        _config = config;
        _httpClient = new SmsHttpClient(config);
    }

    //public async Task<ListResult<SmsReportModel>> GetReportAsync(            SmsReportType reportType, DateTime? dateFrom, DateTime? dateTo)
    //{
    //    ListResult<SmsReportModel> result;

    //    try
    //    {
    //        var apiRes = await _httpClient.GetAsync($@"{_config.ReportUrl}?reportType={reportType}&dateFrom={dateFrom}&dateTo={dateTo}");
    //        var json = await apiRes.Content.ReadAsStringAsync();
    //        result = JsonConvert.DeserializeObject<ListResult<SmsReportModel>>(json);
    //    }
    //    catch (Exception e)
    //    {
    //        result = e.ToListErrorResult<SmsReportModel>(this);
    //    }

    //    return result;
    //}

    public void Dispose()
    {
        _httpClient?.Dispose();
    }

    public async Task<NonQueryResult> SendAsync(string number, string body)
    {
        NonQueryResult result;

        try
        {
            var httpRes = await _httpClient.PostAsync(_config.SendUrl, JsonUtils.ToJsonStringContent(new
            {
                To = number,
                Body = body
            }));
            var json = await httpRes.Content.ReadAsStringAsync();
            result = JsonConvert.DeserializeObject<NonQueryResult>(json);
        }
        catch (Exception e)
        {
            result = e.ToNonQueryErrorResult(e.StackTrace);
        }

        return result;
    }

    public async Task<NonQueryResult> CheckUpdatesAsync()
    {
        NonQueryResult result;

        try
        {
            var httpRes = await _httpClient.GetAsync(_config.CheckUpdatesUrl);
            var json = await httpRes.Content.ReadAsStringAsync();
            result = JsonConvert.DeserializeObject<NonQueryResult>(json);
        }
        catch (Exception e)
        {
            result = e.ToNonQueryErrorResult(e.StackTrace);
        }

        return result;
    }

    public async Task<InfoResult<SmsBalanceModel>> CheckCreditAsync()
    {
        InfoResult<SmsBalanceModel> result;

        try
        {
            var httpRes = await _httpClient.GetAsync(_config.CheckBalanceUrl);
            var json = await httpRes.Content.ReadAsStringAsync();
            result = JsonConvert.DeserializeObject<InfoResult<SmsBalanceModel>>(json);
        }
        catch (Exception e)
        {
            result = e.ToInfoErrorResult<SmsBalanceModel>(this);
        }

        return result;
    }

    #region --------------- Mail ---------------

    public static string _Email_Id = "info.mrsolution7@gmail.com";
    public static string _Email_Password = "#MrSolution#";

    public static void SendMail(string msg_UserFrom, string msg_To, string msg_Subject, string msg_Body)
    {
        var msg_From = _Email_Id;
        var Pwds = ObjGlobal.Decrypt(_Email_Password);

        var from = new MailAddress(msg_From, msg_UserFrom);
        var to = new MailAddress(msg_To, string.Empty);
        try
        {
            var message = new MailMessage
            {
                From = new MailAddress(msg_From)
            };
            message.To.Add(new MailAddress(msg_To));
            message.Subject = msg_Subject;
            message.Body = msg_Body;

            var client = new SmtpClient
            {
                Credentials = new NetworkCredential(msg_From, Pwds),
                Port = 587,
                Host = "smtp.gmail.com",
                EnableSsl = true
            };
            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ObjGlobal.Caption);
            }
        }
        catch (Exception ex)
        {
            var errMsg = ex;
        }
    }

    public static async Task SendMailAsync(string msg_UserFrom)
    {
        var msgFrom = string.IsNullOrWhiteSpace(_Email_Id) ? "info.mrsolution7@gmail.com" : _Email_Id;
        var puds = string.IsNullOrWhiteSpace(_Email_Id) ? _Email_Password : ObjGlobal.Decrypt(_Email_Password);

        var from = new MailAddress(msgFrom, msg_UserFrom);
        var to = new MailAddress("info@mrsolution.com.np", string.Empty);
        var message = new MailMessage
        {
            From = new MailAddress(msgFrom)
        };
        try
        {
            message.To.Add(new MailAddress("support@mrsolution.com.np"));
            message.CC.Add(new MailAddress("manish@mrsolution.com.np"));
            message.Bcc.Add(new MailAddress("mbudhathoki23@gmail.com"));
            message.Subject = $"ERROR LOGS ON {ObjGlobal.LogInCompany}";
            message.Body = "ERROR ON CLIENT SITES";
            BackupDirectory(Environment.CurrentDirectory + @"\logs");
            var attachment = new Attachment(Environment.CurrentDirectory + @"\logs.zip");
            message.Attachments.Add(attachment);

            using var client = new SmtpClient
            {
                Credentials = new NetworkCredential(msgFrom, puds),
                Port = 587,
                Host = "smtp.gmail.com",
                EnableSsl = true
            };
            try
            {
                await client.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ObjGlobal.Caption);
            }
        }
        catch (Exception ex)
        {
            var errMsg = ex;
        }
    }

    public static void BackupDirectory(string directory)
    {
        using var zip = new ZipFile
        {
            CompressionLevel = CompressionLevel.BestCompression
        };
        var files = Directory.GetFiles(directory, "*", SearchOption.AllDirectories)
            .Where(f => Path.GetExtension(f).ToLowerInvariant() != ".zip").ToArray();
        foreach (var f in files) zip.AddFile(f, Path.GetDirectoryName(f)?.Replace(directory, string.Empty));

        zip.Save(Path.ChangeExtension(directory, ".zip"));
    }

    #endregion --------------- Mail ---------------
}