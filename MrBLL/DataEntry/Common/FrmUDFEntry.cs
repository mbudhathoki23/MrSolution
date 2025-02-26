using MrDAL.Control.WinControl;
using MrDAL.Global.Common;
using MrDAL.Global.Common.RawQuery;
using MrDAL.Utility.Server;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace MrBLL.DataEntry.Common;

public partial class FrmUDFEntry : MrForm
{
    public FrmUDFEntry()
    {
        InitializeComponent();
        dtPUdf.Reset();
        dtPUdf.Columns.Add("UDF_Id");
        dtPUdf.Columns.Add("Sn");
        dtPUdf.Columns.Add("Product_Id");
        dtPUdf.Columns.Add("Transaction_Data");
        dtPUdf.Columns.Add("Station");
    }

    private void FrmUDFEntry_Load(object sender, EventArgs e)
    {
        varTop = 10;
        VarTop1 = 10;
        VarLeft = 5;
        VarTabIndex = 0;

        dtUDF.Reset();
        Query =
            $"SELECT * FROM AMS.UDF Where Form_Name= '{Module_Name}' and Status=1 and Branch_Id='{ObjGlobal.SysBranchId}' Order By Sequance ";
        dtUDF = GetConnection.SelectDataTableQuery(Query);

        GetUDFControl();
        if (DtBwiseDetlUdf != null)
            if (DtBwiseDetlUdf.Rows.Count > 0)
                BindData();
        if (DtPwiseDetlUdf != null)
            if (DtPwiseDetlUdf.Rows.Count > 0)
                BindData();
    }

    private void FrmUDFEntry_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter) SendKeys.Send("{TAB}");
        if (e.KeyChar == (char)Keys.Escape)
        {
            var dialogResult = MessageBox.Show("Are you sure want to Close Form!", ObjGlobal.Caption,
                MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes) Close();
        }
    }

    private void BindData()
    {
        dtData.Reset();
        if (DtBwiseDetlUdf != null)
            if (DtBwiseDetlUdf.Rows.Count > 0)
                dtData = DtBwiseDetlUdf;
        if (DtPwiseDetlUdf != null)
            if (DtPwiseDetlUdf.Rows.Count > 0)
                dtData = DtPwiseDetlUdf;
        foreach (Control c in Udf_Pnl.Controls)
        {
            if (c is Label) goto Outer;

            if (c is TextBox)
            {
                var TextBoxControl = (TextBox)c;
                var result = dtData.Select("Udf_Id='" + TextBoxControl.Name.Substring(4) + "'");
                foreach (var row in result) TextBoxControl.Text = row["Transaction_Data"].ToString();
            }
            else if (c is MaskedTextBox)
            {
                var MaskedTextBoxControl = (MaskedTextBox)c;
                var result = dtData.Select("Udf_Id='" + MaskedTextBoxControl.Name.Substring(8) + "'");
                foreach (var row in result) MaskedTextBoxControl.Text = row["Transaction_Data"].ToString();
            }
            else if (c is ComboBox)
            {
                var ComboBoxControl = (ComboBox)c;
                var result = dtData.Select("Udf_Id='" + ComboBoxControl.Name.Substring(3) + "'");
                foreach (var row in result) ComboBoxControl.Text = row["Transaction_Data"].ToString();
            }
            else if (c is CheckBox)
            {
                var CheckBoxControl = (CheckBox)c;
                var result = dtData.Select("Udf_Id='" + CheckBoxControl.Name.Substring(4) + "'");
                foreach (var row in result)
                    CheckBoxControl.Checked = Convert.ToBoolean(row["Transaction_Data"].ToString());
            }

            Outer:;
        }
    }

    private void GetUDFControl()
    {
        if (dtUDF.Rows.Count > 0)
            foreach (DataRow ro in dtUDF.Rows)
            {
                CreateLabel(Convert.ToInt16(ro["UDF_Id"].ToString()), ro["Column_Name"].ToString());
                if (ro["Data_Type"].ToString() == "String" || ro["Data_Type"].ToString() == "Number" ||
                    ro["Data_Type"].ToString() == "Decimal")
                    CreateTextBox(Convert.ToInt16(ro["UDF_Id"].ToString()), ro["Column_Name"].ToString(),
                        ro["Data_Type"].ToString(), Convert.ToInt16(ro["Size"].ToString()));
                else if (ro["Data_Type"].ToString() == "Date")
                    CreateMaskedTextBox(Convert.ToInt16(ro["UDF_Id"].ToString()), ro["Column_Name"].ToString(),
                        ro["Data_Type"].ToString(), ro["Date_Formate"].ToString(),
                        Convert.ToInt16(ro["Size"].ToString()));
                else if (ro["Data_Type"].ToString() == "Yes/No")
                    CreateCheckBox(Convert.ToInt16(ro["UDF_Id"].ToString()), ro["Column_Name"].ToString());
                else if (ro["Data_Type"].ToString() == "Memo")
                    CreateMemo(Convert.ToInt16(ro["UDF_Id"].ToString()), ro["Column_Name"].ToString(),
                        ro["Data_Type"].ToString(), Convert.ToInt16(ro["Size"].ToString()));
                else if (ro["Data_Type"].ToString() == "List")
                    CreateComboBox(Convert.ToInt16(ro["UDF_Id"].ToString()), ro["Column_Name"].ToString(),
                        ro["Data_Type"].ToString(), Convert.ToInt16(ro["Size"].ToString()));
            }
    }

    private void CreateLabel(int UDF_Id, string Column_Name)
    {
        var lbl = new Label
        {
            Name = "lbl_" + UDF_Id,
            Height = 285,
            Font = new Font("Arial", 9, FontStyle.Regular),
            AutoSize = true,
            Text = Column_Name
        };
        Udf_Pnl.Controls.Add(lbl);
        lbl.Location = new Point(VarLeft, varTop);
        varTop = varTop + 30;
    }

    private void CreateTextBox(int UDF_Id, string Column_Name, string Data_Type, int Size)
    {
        var txt = new TextBox
        {
            Name = "txt_" + UDF_Id,
            Tag = Column_Name,
            Height = 21,
            Width = Size + 50,
            Font = new Font("Arial", 9, FontStyle.Regular),
            BackColor = SystemColors.HighlightText,
            BorderStyle = BorderStyle.FixedSingle,
            Location = new Point(VarLeft + 100, VarTop1) //add lbl width in left
        };
        if (Data_Type == "Decimal" || Data_Type == "Number")
            txt.RightToLeft = RightToLeft.Yes;
        else
            txt.RightToLeft = RightToLeft.No;

        if (Size.ToString() != null)
            txt.MaxLength = Size;
        else
            txt.MaxLength = 10;

        txt.TabIndex = VarTabIndex + 1;
        txt.Visible = true;
        txt.Enter += TextBox_Enter;
        txt.Leave += TextBox_Leave;
        txt.KeyDown += TextBox_KeyDown;
        txt.KeyPress += TextBox_KeyPress;
        txt.TextChanged += TextBox_Changed;
        txt.Validated += TextBox_Validated;
        txt.Validating += TextBox_Validating;
        VarTabIndex = VarTabIndex + 1;
        Udf_Pnl.Controls.Add(txt);
        VarTop1 = VarTop1 + 30;
    }

    private void CreateMaskedTextBox(int UDF_Id, string Column_Name, string Data_Type, string Date_Formate,
        int Size)
    {
        var msk_Date = new MaskedTextBox
        {
            Name = "msk_Date" + UDF_Id,
            Tag = Column_Name,
            Height = 285,
            Width = Size + 70,
            Font = new Font("Arial", 9, FontStyle.Regular),
            BackColor = SystemColors.HighlightText,
            BorderStyle = BorderStyle.FixedSingle
        };
        if (Date_Formate.Length == 4)
        {
            msk_Date.Mask = string.Empty;
            msk_Date.Text = string.Empty;
            msk_Date.Mask = "0000";
        }
        else if (Date_Formate.Length == 7)
        {
            msk_Date.Mask = string.Empty;
            msk_Date.Text = string.Empty;
            msk_Date.Mask = "00/0000";
        }
        else
        {
            msk_Date.Mask = string.Empty;
            msk_Date.Text = string.Empty;
            msk_Date.Mask = "00/00/0000";
        }

        msk_Date.Location = new Point(VarLeft + 100, VarTop1); //add lbl width in left
        if (Size.ToString() != null)
            msk_Date.MaxLength = Size;
        else
            msk_Date.MaxLength = 10;

        msk_Date.TabIndex = VarTabIndex + 1;
        ObjGlobal.PageLoadDateType(msk_Date);
        msk_Date.Visible = true;
        msk_Date.Enter += msk_Date_Enter;
        msk_Date.Leave += msk_Date_Leave;
        msk_Date.KeyDown += msk_Date_KeyDown;
        msk_Date.TextChanged += msk_Date_Changed;
        msk_Date.Validated += msk_Date_Validated;
        VarTabIndex = VarTabIndex + 1;
        Udf_Pnl.Controls.Add(msk_Date);
        VarTop1 = VarTop1 + 30;
    }

    private void CreateCheckBox(int UDF_Id, string Column_Name)
    {
        intcnt = intcnt + 1;
        var chk = new CheckBox
        {
            Name = "chk_" + UDF_Id,
            Tag = Column_Name,
            Location = new Point(VarLeft + 100, VarTop1),
            TabIndex = VarTabIndex + 1
        };
        Udf_Pnl.Controls.Add(chk);
        VarTop1 = VarTop1 + 30;
        VarTabIndex = VarTabIndex + 1;
    }

    private void CreateMemo(int UDF_Id, string Column_Name, string Data_Type, int Size)
    {
        var txt2 = new TextBox
        {
            Name = "txt_" + UDF_Id,
            Tag = Column_Name,
            Height = 21,
            Width = Size + 50,
            Font = new Font("Arial", 9, FontStyle.Regular),
            BackColor = SystemColors.HighlightText,
            BorderStyle = BorderStyle.FixedSingle,
            Location = new Point(VarLeft + 100, VarTop1) //add lbl width in left
        };
#pragma warning disable CS0472 // The result of the expression is always 'true' since a value of type 'int' is never equal to 'null' of type 'int?'
        if (Size != null)
#pragma warning restore CS0472 // The result of the expression is always 'true' since a value of type 'int' is never equal to 'null' of type 'int?'
            txt2.MaxLength = Size;
        else
            txt2.MaxLength = 10;

        txt2.TabIndex = VarTabIndex + 1;
        txt2.Visible = true;
        txt2.Enter += TextBox2_Enter;
        txt2.Leave += TextBox2_Leave;
        txt2.KeyDown += TextBox2_KeyDown;
        txt2.TextChanged += TextBox2_Changed;
        txt2.Validated += TextBox2_Validated;
        VarTabIndex = VarTabIndex + 1;
        Udf_Pnl.Controls.Add(txt2);
        VarTop1 = VarTop1 + 30;
    }

    private void CreateComboBox(int UDF_Id, string Column_Name, string Data_Type, int Size)
    {
        intcnt = intcnt + 1;
        var cb = new ComboBox
        {
            Name = "cb_" + UDF_Id,
            Tag = Column_Name,
            DropDownStyle = ComboBoxStyle.DropDownList,
            Location = new Point(VarLeft + 100, VarTop1)
        };
        cb.Items.Clear();
        dtlist.Reset();
        Query = "SELECT UdfList_Id,List_Item  FROM PIE.UDF_ListItem Where UDF_Id=" + UDF_Id + " and Branch_Id='" +
                ObjGlobal.SysBranchId + "' ";
        dtlist = GetConnection.SelectDataTableQuery(Query);
        if (dtlist.Rows.Count > 0)
        {
            cb.DataSource = dtlist;
            cb.DisplayMember = "List_Item";
            cb.ValueMember = "UdfList_Id";
        }
        else
        {
            cb.DataSource = null;
        }

        cb.TabIndex = VarTabIndex + 1;
        Udf_Pnl.Controls.Add(cb);
        VarTop1 = VarTop1 + 30;
        VarTabIndex = VarTabIndex + 1;
    }

    #region Global

    private ClsDocumentPrinting Obj = new();
    public DataTable dtPUdf = new("Temp");
    private DataTable dt = new();
    private DataTable dtlist = new();
    public DataTable dtUDF = new();
    private DataTable dtControl = new();
    public DataTable dtData = new();
    private DataTable dtudftemp = new();
    private string Query = string.Empty;

    public string ProductId;
    public int Sno;
#pragma warning disable CS0169 // The field 'FrmUDFEntry.i' is never used
    private int i;
#pragma warning restore CS0169 // The field 'FrmUDFEntry.i' is never used
#pragma warning disable CS0169 // The field 'FrmUDFEntry.CurrPage' is never used
    private int CurrPage;
#pragma warning restore CS0169 // The field 'FrmUDFEntry.CurrPage' is never used
    private int VarTabIndex;
#pragma warning disable CS0169 // The field 'FrmUDFEntry.totpage' is never used
    private int totpage;
#pragma warning restore CS0169 // The field 'FrmUDFEntry.totpage' is never used
    private int varTop;
    private int VarLeft;
    private int VarTop1;
#pragma warning disable CS0169 // The field 'FrmUDFEntry.OpenFor' is never used
    private string OpenFor;
#pragma warning restore CS0169 // The field 'FrmUDFEntry.OpenFor' is never used
    public string Vno;
    public string Station1;
#pragma warning disable CS0169 // The field 'FrmUDFEntry.controlwid' is never used
    private int controlwid;
#pragma warning restore CS0169 // The field 'FrmUDFEntry.controlwid' is never used
#pragma warning disable CS0169 // The field 'FrmUDFEntry.intpg' is never used
    private int intpg;
#pragma warning restore CS0169 // The field 'FrmUDFEntry.intpg' is never used
    private int intcnt;
    public string Clm_Name;
    public string UdfId;
    public string TagType;

    public DataTable DtBwiseDetlUdf { get; set; }

    public DataTable DtPwiseDetlUdf { get; set; }

    public string FrmName { get; set; }

    public string Station { get; set; }

    public string Module_Name { get; set; }

    public string SelectQuery { get; set; }

    public int PSn { get; set; }

    public long ProSNo { get; set; }

    public string Product_Id { get; set; }

    public string PUDF_Id { get; set; }

    #endregion Global

    //private static Int16 Getindex(int Udf_Id)
    //{
    //ReportTable.Reset();
    //Query = "SELECT * FROM PIE.UDF Where Form_Name= '" + Module_Name + "' and Status=1 and Branch_Id='" + ObjGlobal._Branch_Id + "' Order By Sequance ";
    //ReportTable = ObjGlobal.SelectQuery(Query);
    //rsudftemp.Filter = 0
    //rsudftemp.Filter = "udf_code = '" & UdfCode & "'"

    //Getindex = rsudftemp.Fields("udf_schedule")

    //}

    #region Event

    private void TextBox_Enter(object sender, EventArgs e)
    {
        var textbox = sender as TextBox;
        ObjGlobal.TxtBackColor(textbox, 'E');
    }

    private void TextBox_Leave(object sender, EventArgs e)
    {
        var textbox = sender as TextBox;
        ObjGlobal.TxtBackColor(textbox, 'L');
    }

    private void TextBox_Changed(object sender, EventArgs e)
    {
        var textbox = sender as TextBox;
        //MessageBox.Show(textbox.Name + " text changed. Value " + textbox.Text);
    }

    private void TextBox_KeyDown(object sender, KeyEventArgs e)
    {
        var textbox = sender as TextBox;
        if (e.KeyCode == Keys.Enter)
        {
            //SendKeys.Send("{TAB}");
        }
    }

    private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
    {
        var textbox = sender as TextBox;
        Clm_Name = ActiveControl.Tag.ToString();
        dt.Reset();
        Query = "SELECT * FROM PIE.UDF Where Form_Name= '" + Module_Name + "' and Column_Name='" +
                ActiveControl.Tag + "'  and Status=1 and Branch_Id='" + ObjGlobal.SysBranchId +
                "' Order By Sequance ";
        dt = GetConnection.SelectDataTableQuery(Query);
        if (dt.Rows.Count > 0)
        {
            UdfId = dt.Rows[0]["UDF_Id"].ToString();
            if (dt.Rows[0]["Data_Type"].ToString() == "Decimal")
            {
                if (e.KeyChar == (char)Keys.Back ||
                    e.KeyChar == '.' && !(sender as TextBox).Text.Contains(".")) return;
                e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var isNumber);
                return;
            }

            if (dt.Rows[0]["Data_Type"].ToString() == "Number")
            {
                if (e.KeyChar == (char)Keys.Back ||
                    e.KeyChar == '.' && !(sender as TextBox).Text.Contains(".")) return;
                e.Handled = !int.TryParse(e.KeyChar.ToString(), out var isNumber);
            }
        }
    }

    private void TextBox_Validated(object sender, EventArgs e)
    {
    }

    private void TextBox_Validating(object sender, CancelEventArgs e)
    {
        var textbox = sender as TextBox;
        dt.Reset();
        Query = "SELECT * FROM PIE.UDF Where Form_Name= '" + Module_Name + "' and Column_Name='" + Clm_Name +
                "'  and Is_Mandatory=1 and Status=1 and Branch_Id='" + ObjGlobal.SysBranchId +
                "' Order By Sequance ";
        dt = GetConnection.SelectDataTableQuery(Query);
        if (dt.Rows.Count > 0 && textbox.Text == string.Empty)
        {
            MessageBox.Show("This is a Mandotary Field Please Enter Value!");
            if (textbox.Enabled) textbox.Focus();

            return;
        }

        if (TagType == "NEW")
            if (textbox.Text != string.Empty)
            {
                dt.Reset();
                Query =
                    "Select * from PIE.UDF_Transaction as UT , PIE.UDF as U where UT.UDF_Id = U.UDF_Id and Is_AllowDuplicate=0 and Transaction_Data ='" +
                    textbox.Text + "' and UT.UDF_Id =" + UdfId + " and Station +  Invoice_No <>'" + Station1 + Vno +
                    "' and Data_Type <>'Number' ";
                dt = GetConnection.SelectDataTableQuery(Query);
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Duplicate entry has been not allowed!");
                    if (textbox.Enabled) textbox.Focus();

                    e.Cancel = true;
                }
            }
    }

    private void TextBox2_Enter(object sender, EventArgs e)
    {
        var textbox2 = sender as TextBox;
        ObjGlobal.TxtBackColor(textbox2, 'E');
    }

    private void TextBox2_Leave(object sender, EventArgs e)
    {
        var textbox2 = sender as TextBox;
        ObjGlobal.TxtBackColor(textbox2, 'L');
    }

    private void TextBox2_Changed(object sender, EventArgs e)
    {
        var textbox2 = sender as TextBox;
        //MessageBox.Show(textbox2.Name + " text changed. Value " + textbox2.Text);
    }

    private void TextBox2_KeyDown(object sender, KeyEventArgs e)
    {
        Clm_Name = ActiveControl.Tag.ToString();
        var textbox2 = sender as TextBox;
        if (e.KeyCode == Keys.Enter)
        {
            //SendKeys.Send("{TAB}");
        }
    }

    private void TextBox2_Validated(object sender, EventArgs e)
    {
        var textbox2 = sender as TextBox;
        dt.Reset();
        Query = "SELECT * FROM PIE.UDF Where Form_Name= '" + Module_Name + "' and Column_Name='" + Clm_Name +
                "'  and Is_Mandatory=1 and Status=1 and Branch_Id='" + ObjGlobal.SysBranchId +
                "' Order By Sequance ";
        dt = GetConnection.SelectDataTableQuery(Query);
        if (dt.Rows.Count > 0 && textbox2.Text == string.Empty)
        {
            MessageBox.Show("This is a Mandotary Field Please Enter Value!");
            if (textbox2.Enabled) textbox2.Focus();
        }
    }

    private void msk_Date_Enter(object sender, EventArgs e)
    {
        var msk_Date = sender as MaskedTextBox;
        ObjGlobal.MskTxtBackColor(msk_Date, 'E');
    }

    private void msk_Date_Leave(object sender, EventArgs e)
    {
        var msk_Date = sender as MaskedTextBox;
        ObjGlobal.MskTxtBackColor(msk_Date, 'L');
    }

    private void msk_Date_Changed(object sender, EventArgs e)
    {
        var msk_Date = sender as MaskedTextBox;
        //MessageBox.Show(msk_Date.Name + " text changed. Value " + msk_Date.Text);
    }

    private void msk_Date_KeyDown(object sender, KeyEventArgs e)
    {
        Clm_Name = ActiveControl.Tag.ToString();
        var msk_Date = sender as MaskedTextBox;
        if (e.KeyCode == Keys.Enter)
        {
            //SendKeys.Send("{TAB}");
        }
    }

    private void msk_Date_Validated(object sender, EventArgs e)
    {
        var msk_Date = sender as MaskedTextBox;
        dt.Reset();
        Query = "SELECT * FROM PIE.UDF Where Form_Name= '" + Module_Name + "' and Column_Name='" + Clm_Name +
                "'  and Is_Mandatory=1 and Status=1 and Branch_Id='" + ObjGlobal.SysBranchId +
                "' Order By Sequance ";
        dt = GetConnection.SelectDataTableQuery(Query);
        if (dt.Rows.Count > 0 && msk_Date.Text == string.Empty)
        {
            MessageBox.Show("This is a Mandotary Field Please Enter Value!");
            if (msk_Date.Enabled) msk_Date.Focus();

            return;
        }

        if (msk_Date.Mask.Trim() == "00/00/0000" && msk_Date.Text.Trim() != string.Empty)
        {
            if (ObjGlobal.SysDateType == "D")
                //if (ObjGlobal.ValidDateRange(msk_Date.Text) == true)
                //{
                //    if (ObjGlobal.ValidDateRange(Convert.ToDateTime(msk_Date.Text)) == false)
                //    {
                //        MessageBox.Show("Date Must be Between " + ObjGlobal.CfStartAdDate + " and " + ObjGlobal.CfEndAdDate + " ", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        msk_Date.Focus();
                //        return;
                //    }
                //}
                //else
            {
                MessageBox.Show(@"Plz. Enter Valid Date !", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                msk_Date.Focus();
                return;
            }

            MessageBox.Show(@"Plz. Enter Valid Date !", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            msk_Date.Focus();
        }
    }

    private void btn_Save_Click(object sender, EventArgs e)
    {
        DataRow row;
        PUDF_Id = string.Empty;
        //for (int i = 0; i < this.Controls.Count; i++)
        //{
        //    if (this.Controls[i].Name.ToUpper() != "btn_Save" && this.Controls[i].Name.ToUpper() != "btn_Cancel" && this.Controls[i].Name.ToUpper() != "Udf_Pnl" && this.Controls[i].Name.ToUpper() != "")
        //    {
        //        //if (this.Controls[i] > 0 )
        //        //{
        //        //}

        //    }
        //}
        foreach (Control c in Udf_Pnl.Controls)
        {
            if (c is Label) goto Outer;

            if (c is TextBox)
            {
                if (dtPUdf.Rows.Count > 0)
                {
#pragma warning disable CS1717 // Assignment made to same variable; did you mean to assign something else?
                    dtPUdf = dtPUdf;
#pragma warning restore CS1717 // Assignment made to same variable; did you mean to assign something else?
                }

                var TextBoxControl = (TextBox)c;
                row = dtPUdf.NewRow();
                row["UDF_Id"] = TextBoxControl.Name.Substring(4);
                if (PUDF_Id == string.Empty)
                    PUDF_Id = TextBoxControl.Name.Substring(4);
                else
                    PUDF_Id = PUDF_Id + "," + TextBoxControl.Name.Substring(4);

                row["Sn"] = PSn;
                row["Product_Id"] = Product_Id;
                row["Transaction_Data"] = TextBoxControl.Text;
                row["Station"] = Station;
                dtPUdf.Rows.Add(row);
            }
            else if (c is MaskedTextBox)
            {
                if (dtPUdf.Rows.Count > 0)
                {
#pragma warning disable CS1717 // Assignment made to same variable; did you mean to assign something else?
                    dtPUdf = dtPUdf;
#pragma warning restore CS1717 // Assignment made to same variable; did you mean to assign something else?
                }

                var MaskedTextBoxControl = (MaskedTextBox)c;
                row = dtPUdf.NewRow();
                row["UDF_Id"] = MaskedTextBoxControl.Name.Substring(8);
                row["Sn"] = PSn;
                row["Product_Id"] = Product_Id;
                row["Transaction_Data"] = MaskedTextBoxControl.Text;
                row["Station"] = Station;
                dtPUdf.Rows.Add(row);
            }
            else if (c is ComboBox)
            {
                if (dtPUdf.Rows.Count > 0)
                {
#pragma warning disable CS1717 // Assignment made to same variable; did you mean to assign something else?
                    dtPUdf = dtPUdf;
#pragma warning restore CS1717 // Assignment made to same variable; did you mean to assign something else?
                }

                var ComboBoxControl = (ComboBox)c;
                row = dtPUdf.NewRow();
                row["UDF_Id"] = ComboBoxControl.Name.Substring(3);
                row["Sn"] = PSn;
                row["Product_Id"] = Product_Id;
                row["Transaction_Data"] = ComboBoxControl.Text;
                row["Station"] = Station;
                dtPUdf.Rows.Add(row);
            }
            else if (c is CheckBox)
            {
                if (dtPUdf.Rows.Count > 0)
                {
#pragma warning disable CS1717 // Assignment made to same variable; did you mean to assign something else?
                    dtPUdf = dtPUdf;
#pragma warning restore CS1717 // Assignment made to same variable; did you mean to assign something else?
                }

                var CheckBoxControl = (CheckBox)c;
                row = dtPUdf.NewRow();
                row["UDF_Id"] = CheckBoxControl.Name.Substring(4);
                row["Sn"] = PSn;
                row["Product_Id"] = Product_Id;
                row["Transaction_Data"] = CheckBoxControl.Checked;
                row["Station"] = Station;
                dtPUdf.Rows.Add(row);
            }

            Outer:;
        }

        DialogResult = DialogResult.OK;
        Close();
    }

    private void btn_Cancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    #endregion Event
}