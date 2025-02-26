using System.Data.SqlClient;
using System.IO;
using System.Text;
using MrSolutionTable.Methods;

namespace MrSolutionTable;

public partial class FrmPocoGenerator : Form
{
    private string _connString;

    public FrmPocoGenerator()
    {
        InitializeComponent();
        _connString = txtConnString.Text;
        if (string.IsNullOrEmpty(_connString))
        {
            var connectionTxt = Application.StartupPath + @"\Connection.txt";
            if (!File.Exists(connectionTxt))
            {
                return;
            }

            var fileStream = new FileStream(connectionTxt, FileMode.Open, FileAccess.Read);
            using var streamReader = new StreamReader(fileStream, Encoding.UTF8);
            var text = streamReader.ReadToEnd();
            fileStream.Close();
            if (text is not { Length: > 3 })
            {
                return;
            }

            var split = text.Split(';');
            var o = split.Length;
            _connString = $"data source={split[0]}; Initial Catalog={split[3]}; User Id={split[1]}; pwd={split[2]};MultipleActiveResultSets=True;Connection Timeout= 500";
            txtConnString.Text = _connString;
        }
        else
        {
            txtConnString.TextChanged += (s, e) => _connString = txtConnString.Text.Trim();
        }
    }

    private async void btnConnect_Click(object sender, EventArgs e)
    {
        var conn = new SqlConnection(_connString);
        await conn.OpenAsync();

        var classCode = conn.GenerateAllTables(GeneratorBehavior.DapperContrib);
        richTextBox1.Text = (classCode);
    }

    private async void btnExec_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtSql.Text))
        {
            MessageBox.Show(@"Enter sql query here");
            return;
        }

        var conn = new SqlConnection(_connString);

        await conn.OpenAsync();

        var classCode = conn.GenerateClass(txtSql.Text.Trim(), GeneratorBehavior.DapperContrib);
        richTextBox1.Text = (classCode);
    }

    private void richTextBox1_Enter(object sender, EventArgs e)
    {
        richTextBox1.SelectAll();
    }

    private void btnCopy_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(richTextBox1.Text)) return;
        Clipboard.SetText(richTextBox1.Text);
    }

    private void txtConnString_TextChanged(object sender, EventArgs e)
    {

    }
}