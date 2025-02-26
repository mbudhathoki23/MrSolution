using DevExpress.XtraSplashScreen;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Word;
using MrDAL.Control.ControlsEx.NotifyPanel;
using MrDAL.Control.SplashScreen;
using MrDAL.Control.WinControl;
using MrDAL.Core.Utils;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Global.Restore;
using MrDAL.Global.WinForm;
using MrDAL.Master;
using MrDAL.Setup.BackupRestore;
using MrDAL.Setup.Interface;
using MrDAL.Utility.Server;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using Application = Microsoft.Office.Interop.Word.Application;
using DataTable = System.Data.DataTable;

namespace MrBLL.Utility.Restore;

public partial class FrmBackUpDataBase : MrForm
{
    public FrmBackUpDataBase(string module)
    {
        InitializeComponent();
        _module = module;
        ObjGlobal.DGridColorCombo(dgv_Query);
        _restoreRepository = new BackupRestoreRepository();
    }

    private void FrmBackUpDataBase_Load(object sender, EventArgs e)
    {
        BindTableName();
        ControlEnable();
        TxtBackupPath.Text = GetConnection.GetQueryData("SELECT ss.BackupLocation FROM AMS.SystemSetting ss");
        TxtBackupPath.Focus();
    }

    private void FrmBackUpDataBase_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == 5) //Action New
        {
            BtnExecute_Click(sender, e);
        }
        else if (e.KeyChar == (char)Keys.Escape && CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
        {
            Close();
        }
    }

    private void BtnBackupLocation_Click(object sender, EventArgs e)
    {
        if (fb_BackUpDataBase.ShowDialog() == DialogResult.OK)
        {
            TxtBackupPath.Text = fb_BackUpDataBase.SelectedPath;
        }
    }

    private void BtnBackup_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(TxtBackupPath.Text))
            {
                MessageBox.Show(@"BACKUP PATH IS EMPTY..!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtBackupPath.Focus();
                return;
            }
            else
            {
                if (Directory.Exists(TxtBackupPath.Text))
                {
                    SplashScreenManager.ShowForm(typeof(PleaseWait));
                    if (ObjGlobal.DataBaseBackup(ObjGlobal.InitialCatalog, TxtBackupPath.Text) == 0)
                    {
                        MessageBox.Show(@"ERROR OCCURS WHILE DATA BACKUP", ObjGlobal.Caption);
                        return;
                    }

                    DataBackupLog();
                    SplashScreenManager.CloseForm(false);
                    MessageBox.Show(@"DATA BACK UP SUCCESSFULLY COMPLETED..!!", ObjGlobal.Caption);
                    var result = GetConnection.IntExecuteNonQuery($"UPDATE AMS.SystemSetting  SET BackupLocation = '{TxtBackupPath.Text}'; ");
                    Close();
                }
                else
                {
                    this.NotifyValidationError(TxtBackupPath, "SELECTED LOCATION FOR BACKUP IS INVALID");
                }
            }
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            SplashScreenManager.CloseForm(false);
        }
    }

    private void BtnExecute_Click(object sender, EventArgs e)
    {
        var dt = new DataTable();
        if (cb_TableName != null) dt = GetConnection.SelectDataTableQuery($"select * from {cb_TableName.Text}");
        dgv_Query.DataSource = dt;
    }

    private void BtnCustomQuery_Click(object sender, EventArgs e)
    {
        var dt = GetConnection.SelectDataTableQuery(txt_Query.Text);
        if (dt.Rows.Count > 0)
        {
            MessageBox.Show(@"QUERY EXECUTE SUCCESSFULLY..!!", ObjGlobal.Caption);
            dgv_Query.DataSource = dt;
        }
        else
        {
            MessageBox.Show(@"ERROR OCCURS WHILE QUERY EXECUTE ..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            txt_Query.Focus();
        }
    }

    private void TxtBackupPath_Leave(object sender, EventArgs e)
    {
    }

    private void TxtBackupPath_Enter(object sender, EventArgs e)
    {
    }

    private void BtnExportExcel_Click(object sender, EventArgs e)
    {
        ExportReport();
        MessageBox.Show("Export Completed!");
    }

    private void TxtQuery_Enter(object sender, EventArgs e)
    {
    }

    private void TxtQuery_Leave(object sender, EventArgs e)
    {
    }

    private void TxtQuery_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter) btn_CustomQuery.Focus();
    }

    private void TxtBackupPath_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            BtnBackup.Focus();
        }
    }

    private void CmbTableName_Enter(object sender, EventArgs e)
    {
    }

    private void CmbTableName_Leave(object sender, EventArgs e)
    {
    }

    private void CmbTableName_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter) btn_Execute.Focus();
    }

    private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
    {
        WindowState = tabControl1.SelectedIndex == 1 ? FormWindowState.Maximized : FormWindowState.Normal;
    }

    private void LinkHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        _actionTag = "BACKUP";
        var frmPickList =
            new FrmAutoPopList("MAX", "BRLOG", _actionTag, ObjGlobal.SearchText, string.Empty, "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
            frmPickList.ShowDialog();
        else
            MessageBox.Show(@"CANNOT FIND ANY LOG HISTORY..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
    }

    //METHOD

    #region --------------- METHOD ---------------

    private void BindTableName()
    {
        var dt = ObjGlobal.BindSqlTableName();
        if (dt.Rows.Count <= 0) return;
        cb_TableName.DataSource = dt;
        cb_TableName.DisplayMember = "TableName";
        cb_TableName.ValueMember = "TableName";
    }

    private async Task<int> DataBackupLog()
    {
        _actionTag = "SAVE";
        _restoreRepository.BackupLog.Log_ID = _actionTag is "SAVE" ? ObjGlobal.ReturnInt(ClsMasterSetup.ReturnMaxIntValue("MASTER.AMS.BR_LOG", "Log_ID").ToString()) : LogId;
        _restoreRepository.BackupLog.DB_NAME = ObjGlobal.InitialCatalog;
        _restoreRepository.BackupLog.COMPANY = ObjGlobal.LogInCompany?.ToUpper();
        _restoreRepository.BackupLog.LOCATION = TxtBackupPath.Text.Trim().Replace("'", "''");
        _restoreRepository.BackupLog.USED_BY = ObjGlobal.LogInUser.ToUpper();
        _restoreRepository.BackupLog.ACTION = "B";
        _restoreRepository.BackupLog.SyncRowVersion = (short?)(_actionTag is "SAVE" ? ClsMasterSetup.ReturnMaxCountValue("DB_NAME", "MASTER.AMS.BR_LOG", ObjGlobal.InitialCatalog, _restoreRepository.BackupLog.ACTION) + 1 : 1);
        return await _restoreRepository.SaveBackupAndRestoreDatabaseLog(_actionTag);
    }

    private void ControlEnable()
    {
        TxtBackupPath.Enabled = BtnBackupLocation.Enabled = BtnBackup.Enabled = _module.ToUpper() != "RESTORE";
    }

    private void ExportReport()
    {
        var FileName = string.Empty;
        var ConvFileName = string.Empty;
        int co;
        int index;

        try
        {
            saveFile_Dialog.Title = @"Save";
            saveFile_Dialog.Filter = @"Excel File 1997-07 |*.Xls|Excel File 2010-13 |*.xlsx|PDF File |*.pdf|Word File|*.Doc|Html Page |*.Html";
            saveFile_Dialog.ShowDialog();
            FileName = saveFile_Dialog.FileName;
            if (saveFile_Dialog.FilterIndex == 2)
            {
                ConvFileName = FileName;
                FileName = FileName.Substring(0, FileName.Length - 4) + ".Doc";
            }
            var writer = new StreamWriter(FileName);
            //-----------Start Report Exporting------------
            writer.Write("<html>");
            //-----------Report Heading------------
            writer.Write("<head>");
            writer.Write("<title>" + ObjGlobal.LogInCompany + "</title>");
            writer.Write("</head>");
            writer.Write("<body>");
            writer.Write("<P align=center>");
            writer.Write($"<align=center><System.Drawing.Font face=Times New Roman size=5 color=#800000><b>{ObjGlobal.LogInCompany}</b></System.Drawing.Font><BR>");
            writer.Write($"<align=center><System.Drawing.Font face=Times New Roman size=3 color=#0000FF><b>{ObjGlobal.CompanyAddress}</b></System.Drawing.Font><BR></P>");
            writer.Write("<p align=center>");
            writer.Write($"<align=center><u><System.Drawing.Font face=Times New Roman size=3 color=#0000FF><b>{cb_TableName.Text}</b></System.Drawing.Font></u><BR>");
            writer.Write("<table border=1 width=100% cellspacing=0 bgcolor=#FFFFFF cellpadding=.5>");
            writer.Write("<tr>");
            for (var i = 0; i < dgv_Query.Columns.Count; i++)
            {
                if (dgv_Query.Columns[i].Visible)
                {
                    writer.Write($"<td align=center><b><System.Drawing.Font size=2>{dgv_Query.Columns[i].HeaderText.ToUpper()}</System.Drawing.Font></b></td>");
                }
            }
            writer.Write("</tr>");
            //-----------Report Details--------------------
            for (index = 0; index < dgv_Query.Rows.Count; index++)
            {
                writer.Write("<tr>");
                for (co = 0; co < dgv_Query.Columns.Count; co++)
                {
                    var fontBol = string.Empty;
                    if (dgv_Query.Rows[index].Cells[co].InheritedStyle.Font.Bold) fontBol = "<B>";
                    if (dgv_Query.Rows[index].Cells[co].InheritedStyle.Font.Italic) fontBol = "<I>";
                    var fontAlign = string.Empty;
                    if (dgv_Query.Rows[index].Cells[co].InheritedStyle.Alignment == DataGridViewContentAlignment.MiddleCenter)
                    {
                        fontAlign = "Align = Center";
                    }
                    else
                    {
                        var dataGridViewCellStyle = dgv_Query.Columns[co].InheritedStyle;
                        if (dataGridViewCellStyle is { Alignment: DataGridViewContentAlignment.MiddleLeft })
                        {
                            fontAlign = "Align = Left";
                        }
                        else
                        {
                            var gridViewCellStyle = dgv_Query.Columns[co].InheritedStyle;
                            fontAlign = gridViewCellStyle switch
                            {
                                { Alignment: DataGridViewContentAlignment.MiddleRight } => "Align = Right",
                                _ => fontAlign
                            };
                        }
                    }
                    if (dgv_Query.Rows[index].Cells[co].Visible)
                    {
                        writer.Write($"<td {fontAlign}>{fontBol}<System.Drawing.Font face=Courier New size=2>{dgv_Query.Rows[index].Cells[co].Value}</System.Drawing.Font></td></B>");
                    }
                }

                writer.Write("</tr>");
            }

            writer.Write("</table>");
            writer.Write("</body>");
            writer.Write("</html>");
            writer.Close();
            //-----------End Report Exporting------------
            if (saveFile_Dialog.FilterIndex != 2) return;
            if (FileName == string.Empty || ConvFileName == string.Empty) return;
            ConvertWordToPdf(FileName, ConvFileName);
            File.Delete(FileName);
        }
        catch (Exception ex)
        {
            ex.DialogResult();
        }
    }

    private string ConvertWordToPdf(string inputFile, string outputFileName)
    {
        var wordApp = new Application();
        Document wordDoc = null;
        object inputFileTemp = inputFile;
        try
        {
            wordDoc = wordApp.Documents.Open(inputFile);
            wordDoc.ExportAsFixedFormat(outputFileName, WdExportFormat.wdExportFormatPDF);
        }
        finally
        {
            wordDoc?.Close(WdSaveOptions.wdDoNotSaveChanges);
            wordApp.Quit(WdSaveOptions.wdDoNotSaveChanges);
        }

        return outputFileName;
    }

    private string ConvertExcelToPdf(string inputFile, string outputFileName)
    {
        var excelApp = new Microsoft.Office.Interop.Excel.Application { Visible = false };
        Workbook workbook = null;
        Workbooks workbooks = null;
        try
        {
            workbooks = excelApp.Workbooks;
            workbook = workbooks.Open(inputFile);
            workbook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, outputFileName, XlFixedFormatQuality.xlQualityStandard, true, true, Type.Missing, Type.Missing, false, Type.Missing);
        }
        finally
        {
            if (workbook != null)
            {
                workbook.Close(XlSaveAction.xlDoNotSaveChanges);
                while (Marshal.FinalReleaseComObject(workbook) != 0)
                {
                }
            }
            if (workbooks != null)
            {
                workbooks.Close();
                while (Marshal.FinalReleaseComObject(workbooks) != 0)
                {
                }
            }
            excelApp.Quit();
            excelApp.Application.Quit();
            while (Marshal.FinalReleaseComObject(excelApp) != 0)
            {
            }
        }

        return outputFileName;
    }

    private static SqlCommand GetSqlCommand(SqlServerInfo sqlServerInfo, string cmdText, int? timeout)
    {
        var sqlInfo = new SqlServerInfo();
        if (!string.IsNullOrEmpty(GetConnection.ServerDesc))
        {
            sqlInfo.Server = GetConnection.ServerDesc;
            sqlInfo.IntegratedSecurity = string.IsNullOrEmpty(GetConnection.ServerUserId);
            if (!string.IsNullOrEmpty(GetConnection.ServerUserId))
            {
                sqlInfo.UserName = GetConnection.ServerUserId;
                sqlInfo.Password = GetConnection.ServerUserPsw ?? string.Empty;
            }
        }
        sqlServerInfo = sqlInfo;
        var connectionString = GetConnectionString(sqlServerInfo, null, timeout);
        var sqlCommand = new SqlCommand(cmdText, new SqlConnection(connectionString)) { CommandTimeout = 10000 };
        return sqlCommand;
    }

    private static string GetConnectionString(SqlServerInfo server, string database, int? timeout)
    {
        var str1 = string.IsNullOrEmpty(database) ? "" : $"Initial Catalog={database};";
        var str2 = !server.IntegratedSecurity
            ? $"Data Source={server.Server};{str1}User ID={server.UserName};Password={server.Password}{(timeout.HasValue ? ";Connection Timeout=" + timeout.Value : "")}"
            : $"Data Source={server.Server};{str1}Integrated Security=True{(timeout.HasValue ? ";Connection Timeout=" + timeout.Value : "")}";
        return str2;
    }

    #endregion --------------- METHOD ---------------

    //OBJECT

    #region --------------- OBJECT ---------------

    private readonly string _module;
    private readonly IBackupRestore _restoreRepository;
    private string _actionTag;
    public int LogId = 0;

    #endregion --------------- OBJECT ---------------
}