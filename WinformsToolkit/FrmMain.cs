using Dapper;
using Dapper.Contrib.Extensions;
using System.Data.SqlClient;
using System.IO;
using System.Net.Http;
using MrSolutionTable.Methods;

namespace MrSolutionTable;

public partial class FrmMain : Form
{
    public FrmMain()
    {
        InitializeComponent();
    }

    private async void button1_Click(object sender, EventArgs e)
    {
        try
        {
            await using var conn = new SqlConnection(@"Server=DESKTOP-32ANRQC\MSSQLSERVER2014;Database=ServerApi;Trusted_Connection=True;");
            await conn.OpenAsync();

            var rows = (await conn.GetAllAsync<Device>()).Where(x => x.photo_bin == null).AsList();
            using var httpClient = new HttpClient();
            httpClient.Timeout = Timeout.InfiniteTimeSpan;

            foreach (var row in rows)
            {
                richTextBox1.Text += Environment.NewLine + ($"{row.Name} - {row.DeviceType}");

                var response = await httpClient.GetByteArrayAsync(row.ImageUrl);
                if (response.Length > 0)
                {
                    await conn.ExecuteAsync(@$"UPDATE lkup.Device SET photo_bin = @prBin where Id = @prId ",
                        new { prBin = response, prId = row.Id });
                    richTextBox1.Text += " [Success]";
                }
                else
                {
                    richTextBox1.Text += " FAIL";
                }
            }

            MessageBox.Show("All done");
        }
        catch (Exception ex)
        {
            richTextBox1.Lines.Append(ex.Message);
        }
    }

    private async void button2_Click(object sender, EventArgs e)
    {
        flowLayoutPanel1.Controls.Clear();


        try
        {
            await using var conn = new SqlConnection(@"Server=DESKTOP-32ANRQC\MSSQLSERVER2014;Database=ServerApi;Trusted_Connection=True;");
            await conn.OpenAsync();

            var rows = (await conn.QueryAsync<Device>("SELECT TOP 100 * FROM lkup.Device")).AsList();


            rows.ForEach(x =>
            {
                using var stream = new MemoryStream(x.photo_bin);
                var control = new Button
                {
                    Size = new Size(150, 200),
                    Image = Image.FromStream(stream),
                    ImageAlign = ContentAlignment.MiddleCenter,
                };

                flowLayoutPanel1.Controls.Add(control);
            });



            MessageBox.Show("All done");
        }
        catch (Exception ex)
        {
            richTextBox1.Lines.Append(ex.Message);
        }
    }
}