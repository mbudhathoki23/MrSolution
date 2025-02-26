using Dapper;
using System.Data.SqlClient;
using MrSolutionTable.Methods;

namespace MrSolutionTable;

public partial class FirmIdGenerator : Form
{
    public FirmIdGenerator()
    {
        InitializeComponent();
    }

    private async void button1_Click(object sender, EventArgs e)
    {
        try
        {
            using var conn = new SqlConnection("Server=DESKTOP-32ANRQC\\MSSQL2008;Database=CPSM_Laptop;Trusted_Connection=True;");
            await conn.OpenAsync();
            using var trans = await conn.BeginTransactionAsync();


            var tables = (await conn.QueryAsync<SysSchobJs>(
                @"SELECT t.name AS TableName, s.name AS SchemaName FROM sys.tables t
                    JOIN sys.schemas s ON s.schema_id = t.schema_id
                    WHERE s.principal_id = 1 ", transaction: trans)).AsList();

            progressBar1.Minimum = 0;
            progressBar1.Maximum = tables.Count;

            foreach (var table in tables)
            {
                var columnSize = await conn.QueryFirstAsync<byte?>($@"SELECT COL_LENGTH('{table.SchemaName}.{table.TableName}', 'firm_id')", transaction: trans);

                if (columnSize != null) { continue; }

                // create column and index
                var rowCount = await conn.QueryFirstOrDefaultAsync<int>($@"SELECT COUNT(*) FROM {table.SchemaName}.{table.TableName} ", transaction: trans);
                if (rowCount > 0)
                {
                    await conn.ExecuteAsync($@"ALTER TABLE {table.SchemaName}.{table.TableName} ADD firm_id INT NULL ", transaction: trans);
                    await conn.ExecuteAsync($@"UPDATE {table.SchemaName}.{table.TableName} SET firm_id  = 0 ", transaction: trans);
                    await conn.ExecuteAsync($@"ALTER TABLE {table.SchemaName}.{table.TableName} ALTER COLUMN firm_id INT NOT NULL ", transaction: trans);

                    await conn.ExecuteAsync(
                        $@"CREATE NONCLUSTERED INDEX IX_{table.TableName}_FirmId 
                        ON [{table.SchemaName}].[{table.TableName}] (firm_id)", transaction: trans);
                }
                else
                {
                    await conn.ExecuteAsync($@"ALTER TABLE {table.SchemaName}.{table.TableName} ADD firm_id INT NOT NULL ", transaction: trans);
                    await conn.ExecuteAsync(
                        $@"CREATE NONCLUSTERED INDEX IX_{table.TableName}_FirmId 
                        ON [{table.SchemaName}].[{table.TableName}] (firm_id)", transaction: trans);
                }

                progressBar1.Value += 1;
            }

            await trans.CommitAsync();
            MessageBox.Show("All Done");
        }
        catch (Exception ex)
        {

            throw;
        }
    }
}