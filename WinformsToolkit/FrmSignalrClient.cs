using Dapper;
using Microsoft.AspNetCore.SignalR.Client;
using System.Data.SqlClient;
using System.Text.Json;
using MrSolutionTable.Methods;

namespace MrSolutionTable;

public partial class FrmSignalrClient : Form
{
    private HubConnection _hubConnection;
    private string _connString;


    public FrmSignalrClient()
    {
        InitializeComponent();
        _connString = txtConnstring.Text;
        txtConnstring.TextChanged += (s, e) =>
        {
            _connString = txtConnstring.Text;
        };

        dataGridView1.AutoGenerateColumns = true;
        dataGridView1.DataSource = bindingSource1;
    }

    private async void btnConnect_Click(object sender, EventArgs e)
    {
        _hubConnection = new HubConnectionBuilder()
            .WithUrl(txtBaseAddress.Text.Trim())
            .Build();

        _hubConnection.Reconnected += (error) =>
        {
            this.Invoke(() => richTextBox1.Text += "Reconnected"); ;
            return Task.CompletedTask;
        };

        _hubConnection.Closed += async (error) =>
        {
            await Task.Delay(new Random().Next(0, 5) * 1000);
            await _hubConnection.StartAsync();
        };

        _hubConnection.On<string, string>("ReceiveMessage", async (user, message) =>
        {
            await OnMessageReceived(message, "SendMessage");
            //this.Invoke(() =>
            //{
            //    bindingSource1.DataSource = null;


            //});
        });

        _hubConnection.Reconnected += _hubConnection_Reconnected;

        try
        {
            await _hubConnection.StartAsync();
            richTextBox1.Text += "Connection started";
            sendButton.Enabled = true;
        }
        catch (Exception ex)
        {
            richTextBox1.Text += ex.Message;
        }
    }

    private Task _hubConnection_Reconnected(string? arg)
    {
        if (arg != null)
            richTextBox1.Text += arg;
        return Task.CompletedTask;
    }

    private async void sendButton_Click(object sender, EventArgs e)
    {
        if (_hubConnection.State != HubConnectionState.Connected)
            return;

        using var conn = new SqlConnection(_connString);
        var rows = await conn.QueryAsync<Device>("SELECT TOP 500 * FROM lkup.Device ");
        var data = rows.AsList();

        await _hubConnection.SendAsync("SendMessage", nameof(Device), JsonSerializer.Serialize(rows));
    }

    private async Task OnMessageReceived(string sql, string hubProcedure)
    {
        if (string.IsNullOrWhiteSpace(sql) || string.IsNullOrWhiteSpace(hubProcedure)) return;

        if (await IsSqlValidAsync(sql) == false) return;

        this.Invoke(() => bindingSource1.DataSource = null);
        ;

        try
        {
            using var conn = new SqlConnection(_connString);
            var rows = await conn.QueryAsync(sql);

            var data = rows.AsList();
            var json = JsonSerializer.Serialize(data);

            this.Invoke(() => bindingSource1.DataSource = data);
            var payload = JsonSerializer.Serialize(new RemoteCommandMessageModel(true, json, null, RemoteCommandType.SqlQuery, null));

            await _hubConnection.SendAsync(hubProcedure, "fromClient", payload);
        }
        catch (Exception e)
        {
            await _hubConnection.SendAsync(hubProcedure, "fromClient",
                JsonSerializer.Serialize(new RemoteCommandMessageModel(false, e.Message, e.Message, RemoteCommandType.SqlQuery, e.ToString())));

        }
    }

    private async Task<bool> IsSqlValidAsync(string sql)
    {
        await using var conn = new SqlConnection(_connString);
        try
        {
            await conn.ExecuteAsync(@"SET PARSEONLY ON " + sql);
        }
        catch (Exception)
        {
            // supress
            return false;
        }
        finally
        {
            await conn.ExecuteAsync(@"SET PARSEONLY OFF ");
        }
        return true;
    }

    private async void btnReconnect_Click(object sender, EventArgs e)
    {
        if (_hubConnection.State != HubConnectionState.Connected)
            await _hubConnection.StartAsync();
    }
}