using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.DataEntry.FinanceMaster
{
    partial class FrmPDCVoucher
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPDCVoucher));
            this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.MskMiti = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.MskDate = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtVno = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.CmbStatus = new System.Windows.Forms.ComboBox();
            this.TxtRemarks = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.TxtAmount = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtAgent = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.TxtSubledger = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.TxtLedger = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.TxtBankBranch = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.TxtClientBank = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.TxtDrawnon = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.MskChequeMiti = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.MskChequeDate = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.TxtBank = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.TxtChequeNo = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TxtDepartment = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.CmbType = new System.Windows.Forms.ComboBox();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.PanelHeader = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.ChkAttachment = new System.Windows.Forms.CheckBox();
            this.clsSeparator1 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.TbControl = new System.Windows.Forms.TabControl();
            this.TbVoucherDetails = new System.Windows.Forms.TabPage();
            this.BtnDrawOn = new System.Windows.Forms.Button();
            this.BtnBranch = new System.Windows.Forms.Button();
            this.BtnBankDesc = new System.Windows.Forms.Button();
            this.BtnRemarks = new System.Windows.Forms.Button();
            this.BtnAgent = new System.Windows.Forms.Button();
            this.BtnDepartment = new System.Windows.Forms.Button();
            this.BtnSubledger = new System.Windows.Forms.Button();
            this.BtnLedger = new System.Windows.Forms.Button();
            this.BtnBank = new System.Windows.Forms.Button();
            this.BtnVno = new System.Windows.Forms.Button();
            this.clsSeparator2 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.TbAttachment = new System.Windows.Forms.TabPage();
            this.LinkAttachment1 = new System.Windows.Forms.LinkLabel();
            this.BtnAttachment1 = new System.Windows.Forms.Button();
            this.LblAttachment1 = new System.Windows.Forms.Label();
            this.PAttachment1 = new System.Windows.Forms.PictureBox();
            this.clsSeparator3 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.PanelHeader.SuspendLayout();
            this.TbControl.SuspendLayout();
            this.TbVoucherDetails.SuspendLayout();
            this.TbAttachment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PAttachment1)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnExit
            // 
            this.BtnExit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExit.Appearance.Options.UseFont = true;
            this.BtnExit.ImageOptions.Image = global::MrBLL.Properties.Resources.Exit;
            this.BtnExit.Location = new System.Drawing.Point(639, 5);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(72, 34);
            this.BtnExit.TabIndex = 3;
            this.BtnExit.Text = "E&XIT";
            // 
            // BtnDelete
            // 
            this.BtnDelete.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDelete.Appearance.Options.UseFont = true;
            this.BtnDelete.ImageOptions.Image = global::MrBLL.Properties.Resources.Delete;
            this.BtnDelete.Location = new System.Drawing.Point(154, 5);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(107, 34);
            this.BtnDelete.TabIndex = 2;
            this.BtnDelete.Text = "&DELETE";
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // BtnEdit
            // 
            this.BtnEdit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnEdit.Appearance.Options.UseFont = true;
            this.BtnEdit.ImageOptions.Image = global::MrBLL.Properties.Resources.Edit;
            this.BtnEdit.Location = new System.Drawing.Point(79, 5);
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(75, 34);
            this.BtnEdit.TabIndex = 1;
            this.BtnEdit.Text = "&EDIT";
            this.BtnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // BtnNew
            // 
            this.BtnNew.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnNew.Appearance.Options.UseFont = true;
            this.BtnNew.ImageOptions.Image = global::MrBLL.Properties.Resources.Add;
            this.BtnNew.Location = new System.Drawing.Point(5, 5);
            this.BtnNew.Name = "BtnNew";
            this.BtnNew.Size = new System.Drawing.Size(73, 34);
            this.BtnNew.TabIndex = 0;
            this.BtnNew.Text = "&NEW";
            this.BtnNew.Click += new System.EventHandler(this.BtnNew_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "PDC Vno";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(506, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 19);
            this.label3.TabIndex = 6;
            this.label3.Text = "Date";
            // 
            // MskMiti
            // 
            this.MskMiti.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskMiti.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskMiti.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskMiti.Location = new System.Drawing.Point(389, 4);
            this.MskMiti.Mask = "00/00/0000";
            this.MskMiti.Name = "MskMiti";
            this.MskMiti.Size = new System.Drawing.Size(117, 25);
            this.MskMiti.TabIndex = 1;
            this.MskMiti.ValidatingType = typeof(System.DateTime);
            this.MskMiti.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.MskMiti_MaskInputRejected);
            this.MskMiti.Enter += new System.EventHandler(this.MskMiti_Enter);
            this.MskMiti.Validating += new System.ComponentModel.CancelEventHandler(this.MskMiti_Validating);
            // 
            // MskDate
            // 
            this.MskDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskDate.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskDate.Location = new System.Drawing.Point(555, 4);
            this.MskDate.Mask = "00/00/0000";
            this.MskDate.Name = "MskDate";
            this.MskDate.Size = new System.Drawing.Size(114, 25);
            this.MskDate.TabIndex = 2;
            this.MskDate.ValidatingType = typeof(System.DateTime);
            this.MskDate.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.MskDate_MaskInputRejected);
            this.MskDate.Enter += new System.EventHandler(this.MskDate_Enter);
            this.MskDate.Validating += new System.ComponentModel.CancelEventHandler(this.MskDate_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(333, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 19);
            this.label2.TabIndex = 4;
            this.label2.Text = "Miti";
            // 
            // TxtVno
            // 
            this.TxtVno.BackColor = System.Drawing.Color.White;
            this.TxtVno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtVno.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtVno.ForeColor = System.Drawing.Color.Black;
            this.TxtVno.Location = new System.Drawing.Point(106, 3);
            this.TxtVno.Name = "TxtVno";
            this.TxtVno.Size = new System.Drawing.Size(181, 25);
            this.TxtVno.TabIndex = 0;
            this.TxtVno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtVno_KeyDown);
            this.TxtVno.Leave += new System.EventHandler(this.TxtVno_Leave);
            this.TxtVno.Validating += new System.ComponentModel.CancelEventHandler(this.TxtVno_Validating);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label18.Location = new System.Drawing.Point(400, 38);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(58, 19);
            this.label18.TabIndex = 32;
            this.label18.Text = "Status";
            // 
            // CmbStatus
            // 
            this.CmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CmbStatus.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.CmbStatus.FormattingEnabled = true;
            this.CmbStatus.Items.AddRange(new object[] {
            "Due",
            "Clearing",
            "Deposite",
            "Bounce",
            "Cancel"});
            this.CmbStatus.Location = new System.Drawing.Point(464, 35);
            this.CmbStatus.Name = "CmbStatus";
            this.CmbStatus.Size = new System.Drawing.Size(205, 27);
            this.CmbStatus.TabIndex = 4;
            this.CmbStatus.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CmbStatus_KeyPress);
            // 
            // TxtRemarks
            // 
            this.TxtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtRemarks.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtRemarks.Location = new System.Drawing.Point(107, 256);
            this.TxtRemarks.Name = "TxtRemarks";
            this.TxtRemarks.Size = new System.Drawing.Size(561, 25);
            this.TxtRemarks.TabIndex = 17;
            this.TxtRemarks.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtRemarks_KeyDown);
            this.TxtRemarks.Validating += new System.ComponentModel.CancelEventHandler(this.TxtRemarks_Validating);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label17.Location = new System.Drawing.Point(5, 259);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(76, 19);
            this.label17.TabIndex = 30;
            this.label17.Text = "Remarks";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label16.Location = new System.Drawing.Point(388, 231);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(67, 19);
            this.label16.TabIndex = 28;
            this.label16.Text = "Amount";
            // 
            // TxtAmount
            // 
            this.TxtAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtAmount.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtAmount.Location = new System.Drawing.Point(461, 228);
            this.TxtAmount.Name = "TxtAmount";
            this.TxtAmount.Size = new System.Drawing.Size(205, 25);
            this.TxtAmount.TabIndex = 16;
            this.TxtAmount.Tag = "0";
            this.TxtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtAmount_KeyPress);
            this.TxtAmount.Leave += new System.EventHandler(this.TxtAmount_Leave);
            this.TxtAmount.Validated += new System.EventHandler(this.TxtAmount_Validated);
            // 
            // TxtAgent
            // 
            this.TxtAgent.BackColor = System.Drawing.Color.White;
            this.TxtAgent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtAgent.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtAgent.Location = new System.Drawing.Point(461, 200);
            this.TxtAgent.Name = "TxtAgent";
            this.TxtAgent.ReadOnly = true;
            this.TxtAgent.Size = new System.Drawing.Size(205, 25);
            this.TxtAgent.TabIndex = 14;
            this.TxtAgent.Tag = "0";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label15.Location = new System.Drawing.Point(388, 203);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(52, 19);
            this.label15.TabIndex = 26;
            this.label15.Text = "Agent";
            // 
            // TxtSubledger
            // 
            this.TxtSubledger.BackColor = System.Drawing.Color.White;
            this.TxtSubledger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtSubledger.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtSubledger.Location = new System.Drawing.Point(105, 200);
            this.TxtSubledger.Name = "TxtSubledger";
            this.TxtSubledger.ReadOnly = true;
            this.TxtSubledger.Size = new System.Drawing.Size(232, 25);
            this.TxtSubledger.TabIndex = 13;
            this.TxtSubledger.Tag = "0";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label14.Location = new System.Drawing.Point(5, 203);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(88, 19);
            this.label14.TabIndex = 23;
            this.label14.Text = "SubLedger";
            // 
            // TxtLedger
            // 
            this.TxtLedger.BackColor = System.Drawing.Color.White;
            this.TxtLedger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtLedger.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtLedger.Location = new System.Drawing.Point(105, 119);
            this.TxtLedger.Name = "TxtLedger";
            this.TxtLedger.ReadOnly = true;
            this.TxtLedger.Size = new System.Drawing.Size(561, 25);
            this.TxtLedger.TabIndex = 9;
            this.TxtLedger.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtLedger_KeyDown);
            this.TxtLedger.Leave += new System.EventHandler(this.TxtLedger_Leave);
            this.TxtLedger.Validating += new System.ComponentModel.CancelEventHandler(this.TxtLedger_Validating);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label13.Location = new System.Drawing.Point(5, 124);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(60, 19);
            this.label13.TabIndex = 20;
            this.label13.Text = "Ledger";
            // 
            // TxtBankBranch
            // 
            this.TxtBankBranch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtBankBranch.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtBankBranch.Location = new System.Drawing.Point(461, 173);
            this.TxtBankBranch.Name = "TxtBankBranch";
            this.TxtBankBranch.Size = new System.Drawing.Size(205, 25);
            this.TxtBankBranch.TabIndex = 12;
            this.TxtBankBranch.Enter += new System.EventHandler(this.TxtBankBranch_Enter);
            this.TxtBankBranch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtBankBranch_KeyDown);
            this.TxtBankBranch.Leave += new System.EventHandler(this.TxtBankBranch_Leave);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label12.Location = new System.Drawing.Point(388, 176);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(64, 19);
            this.label12.TabIndex = 18;
            this.label12.Text = "Branch";
            // 
            // TxtClientBank
            // 
            this.TxtClientBank.BackColor = System.Drawing.Color.White;
            this.TxtClientBank.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtClientBank.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtClientBank.Location = new System.Drawing.Point(105, 173);
            this.TxtClientBank.Name = "TxtClientBank";
            this.TxtClientBank.Size = new System.Drawing.Size(232, 25);
            this.TxtClientBank.TabIndex = 11;
            this.TxtClientBank.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtClientBank_KeyDown);
            this.TxtClientBank.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtClientBank_KeyPress);
            this.TxtClientBank.Leave += new System.EventHandler(this.TxtClientBank_Leave);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label11.Location = new System.Drawing.Point(5, 176);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(90, 19);
            this.label11.TabIndex = 16;
            this.label11.Text = "Bank Desc";
            // 
            // TxtDrawnon
            // 
            this.TxtDrawnon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDrawnon.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtDrawnon.Location = new System.Drawing.Point(105, 146);
            this.TxtDrawnon.Name = "TxtDrawnon";
            this.TxtDrawnon.Size = new System.Drawing.Size(561, 25);
            this.TxtDrawnon.TabIndex = 10;
            this.TxtDrawnon.Enter += new System.EventHandler(this.TxtDrawnon_Enter);
            this.TxtDrawnon.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtDrawnon_KeyDown);
            this.TxtDrawnon.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtDrawnon_KeyPress);
            this.TxtDrawnon.Leave += new System.EventHandler(this.TxtDrawnon_Leave);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label10.Location = new System.Drawing.Point(5, 149);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(86, 19);
            this.label10.TabIndex = 14;
            this.label10.Text = "Drawn On";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label8.Location = new System.Drawing.Point(507, 95);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 19);
            this.label8.TabIndex = 10;
            this.label8.Text = "Date";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label7.Location = new System.Drawing.Point(5, 95);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 19);
            this.label7.TabIndex = 8;
            this.label7.Text = "ChequeNo";
            // 
            // MskChequeMiti
            // 
            this.MskChequeMiti.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskChequeMiti.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.MskChequeMiti.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskChequeMiti.Location = new System.Drawing.Point(389, 92);
            this.MskChequeMiti.Mask = "00/00/0000";
            this.MskChequeMiti.Name = "MskChequeMiti";
            this.MskChequeMiti.Size = new System.Drawing.Size(117, 25);
            this.MskChequeMiti.TabIndex = 7;
            this.MskChequeMiti.ValidatingType = typeof(System.DateTime);
            this.MskChequeMiti.Enter += new System.EventHandler(this.MskChequeMiti_Enter);
            this.MskChequeMiti.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MskChequeMiti_KeyPress);
            this.MskChequeMiti.Leave += new System.EventHandler(this.MskChequeMiti_Leave);
            this.MskChequeMiti.Validating += new System.ComponentModel.CancelEventHandler(this.MskChequeMiti_Validating);
            // 
            // MskChequeDate
            // 
            this.MskChequeDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskChequeDate.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.MskChequeDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskChequeDate.Location = new System.Drawing.Point(555, 92);
            this.MskChequeDate.Mask = "00/00/0000";
            this.MskChequeDate.Name = "MskChequeDate";
            this.MskChequeDate.Size = new System.Drawing.Size(111, 25);
            this.MskChequeDate.TabIndex = 8;
            this.MskChequeDate.ValidatingType = typeof(System.DateTime);
            this.MskChequeDate.Enter += new System.EventHandler(this.MskChequeDate_Enter);
            this.MskChequeDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MskChequeDate_KeyPress);
            this.MskChequeDate.Leave += new System.EventHandler(this.MskChequeDate_Leave);
            this.MskChequeDate.Validating += new System.ComponentModel.CancelEventHandler(this.MskChequeDate_Validating);
            // 
            // TxtBank
            // 
            this.TxtBank.BackColor = System.Drawing.Color.White;
            this.TxtBank.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtBank.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtBank.Location = new System.Drawing.Point(106, 65);
            this.TxtBank.Name = "TxtBank";
            this.TxtBank.ReadOnly = true;
            this.TxtBank.Size = new System.Drawing.Size(563, 25);
            this.TxtBank.TabIndex = 5;
            this.TxtBank.Enter += new System.EventHandler(this.TxtBank_Enter);
            this.TxtBank.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtBank_KeyDown);
            this.TxtBank.Leave += new System.EventHandler(this.TxtBank_Leave);
            this.TxtBank.Validating += new System.ComponentModel.CancelEventHandler(this.TxtBank_Validating);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label9.Location = new System.Drawing.Point(334, 95);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(38, 19);
            this.label9.TabIndex = 8;
            this.label9.Text = "Miti";
            // 
            // TxtChequeNo
            // 
            this.TxtChequeNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtChequeNo.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtChequeNo.Location = new System.Drawing.Point(106, 92);
            this.TxtChequeNo.Name = "TxtChequeNo";
            this.TxtChequeNo.Size = new System.Drawing.Size(207, 25);
            this.TxtChequeNo.TabIndex = 6;
            this.TxtChequeNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtChequeNo_KeyPress);
            this.TxtChequeNo.Leave += new System.EventHandler(this.TxtChequeNo_Leave);
            this.TxtChequeNo.Validating += new System.ComponentModel.CancelEventHandler(this.TxtChequeNo_Validating);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label6.Location = new System.Drawing.Point(5, 66);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 19);
            this.label6.TabIndex = 12;
            this.label6.Text = "Bank";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label4.Location = new System.Drawing.Point(5, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 19);
            this.label4.TabIndex = 8;
            this.label4.Text = "Type";
            // 
            // TxtDepartment
            // 
            this.TxtDepartment.BackColor = System.Drawing.Color.White;
            this.TxtDepartment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDepartment.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtDepartment.Location = new System.Drawing.Point(106, 228);
            this.TxtDepartment.Name = "TxtDepartment";
            this.TxtDepartment.ReadOnly = true;
            this.TxtDepartment.Size = new System.Drawing.Size(231, 25);
            this.TxtDepartment.TabIndex = 15;
            this.TxtDepartment.Tag = "0";
            this.TxtDepartment.Enter += new System.EventHandler(this.TxtDepartment_Enter);
            this.TxtDepartment.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtDepartment_KeyPress);
            this.TxtDepartment.Leave += new System.EventHandler(this.TxtDepartment_Leave);
            this.TxtDepartment.Validating += new System.ComponentModel.CancelEventHandler(this.TxtDepartment_Validating);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label5.Location = new System.Drawing.Point(5, 232);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 19);
            this.label5.TabIndex = 10;
            this.label5.Text = "Department";
            // 
            // CmbType
            // 
            this.CmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CmbType.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.CmbType.FormattingEnabled = true;
            this.CmbType.Items.AddRange(new object[] {
            "Payment",
            "Receipt"});
            this.CmbType.Location = new System.Drawing.Point(106, 35);
            this.CmbType.Name = "CmbType";
            this.CmbType.Size = new System.Drawing.Size(282, 27);
            this.CmbType.TabIndex = 3;
            this.CmbType.SelectedIndexChanged += new System.EventHandler(this.CmbType_SelectedIndexChanged);
            this.CmbType.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CmbType_KeyPress);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.BtnCancel24;
            this.BtnCancel.Location = new System.Drawing.Point(576, 374);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(102, 34);
            this.BtnCancel.TabIndex = 6;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnSave.Location = new System.Drawing.Point(486, 374);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(88, 34);
            this.BtnSave.TabIndex = 5;
            this.BtnSave.Text = "&SAVE";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // PanelHeader
            // 
            this.PanelHeader.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.PanelHeader.Controls.Add(this.ChkAttachment);
            this.PanelHeader.Controls.Add(this.clsSeparator1);
            this.PanelHeader.Controls.Add(this.TbControl);
            this.PanelHeader.Controls.Add(this.clsSeparator3);
            this.PanelHeader.Controls.Add(this.BtnExit);
            this.PanelHeader.Controls.Add(this.BtnCancel);
            this.PanelHeader.Controls.Add(this.BtnDelete);
            this.PanelHeader.Controls.Add(this.BtnSave);
            this.PanelHeader.Controls.Add(this.BtnEdit);
            this.PanelHeader.Controls.Add(this.BtnNew);
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelHeader.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PanelHeader.Location = new System.Drawing.Point(0, 0);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(719, 412);
            this.PanelHeader.TabIndex = 0;
            // 
            // ChkAttachment
            // 
            this.ChkAttachment.AutoSize = true;
            this.ChkAttachment.Location = new System.Drawing.Point(16, 380);
            this.ChkAttachment.Name = "ChkAttachment";
            this.ChkAttachment.Size = new System.Drawing.Size(215, 24);
            this.ChkAttachment.TabIndex = 15;
            this.ChkAttachment.Text = "Is Required Attachment";
            this.ChkAttachment.UseVisualStyleBackColor = true;
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator1.Location = new System.Drawing.Point(12, 41);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(703, 2);
            this.clsSeparator1.TabIndex = 14;
            this.clsSeparator1.TabStop = false;
            // 
            // TbControl
            // 
            this.TbControl.CausesValidation = false;
            this.TbControl.Controls.Add(this.TbVoucherDetails);
            this.TbControl.Controls.Add(this.TbAttachment);
            this.TbControl.HotTrack = true;
            this.TbControl.Location = new System.Drawing.Point(6, 46);
            this.TbControl.Name = "TbControl";
            this.TbControl.SelectedIndex = 0;
            this.TbControl.ShowToolTips = true;
            this.TbControl.Size = new System.Drawing.Size(709, 321);
            this.TbControl.TabIndex = 4;
            // 
            // TbVoucherDetails
            // 
            this.TbVoucherDetails.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.TbVoucherDetails.Controls.Add(this.TxtVno);
            this.TbVoucherDetails.Controls.Add(this.BtnDrawOn);
            this.TbVoucherDetails.Controls.Add(this.TxtChequeNo);
            this.TbVoucherDetails.Controls.Add(this.BtnBranch);
            this.TbVoucherDetails.Controls.Add(this.TxtDrawnon);
            this.TbVoucherDetails.Controls.Add(this.BtnBankDesc);
            this.TbVoucherDetails.Controls.Add(this.TxtClientBank);
            this.TbVoucherDetails.Controls.Add(this.BtnRemarks);
            this.TbVoucherDetails.Controls.Add(this.BtnAgent);
            this.TbVoucherDetails.Controls.Add(this.CmbType);
            this.TbVoucherDetails.Controls.Add(this.BtnDepartment);
            this.TbVoucherDetails.Controls.Add(this.TxtLedger);
            this.TbVoucherDetails.Controls.Add(this.BtnSubledger);
            this.TbVoucherDetails.Controls.Add(this.TxtAgent);
            this.TbVoucherDetails.Controls.Add(this.BtnLedger);
            this.TbVoucherDetails.Controls.Add(this.TxtRemarks);
            this.TbVoucherDetails.Controls.Add(this.BtnBank);
            this.TbVoucherDetails.Controls.Add(this.TxtDepartment);
            this.TbVoucherDetails.Controls.Add(this.BtnVno);
            this.TbVoucherDetails.Controls.Add(this.MskChequeDate);
            this.TbVoucherDetails.Controls.Add(this.label9);
            this.TbVoucherDetails.Controls.Add(this.clsSeparator2);
            this.TbVoucherDetails.Controls.Add(this.MskChequeMiti);
            this.TbVoucherDetails.Controls.Add(this.label5);
            this.TbVoucherDetails.Controls.Add(this.label7);
            this.TbVoucherDetails.Controls.Add(this.label6);
            this.TbVoucherDetails.Controls.Add(this.label8);
            this.TbVoucherDetails.Controls.Add(this.label18);
            this.TbVoucherDetails.Controls.Add(this.TxtBank);
            this.TbVoucherDetails.Controls.Add(this.label1);
            this.TbVoucherDetails.Controls.Add(this.label10);
            this.TbVoucherDetails.Controls.Add(this.CmbStatus);
            this.TbVoucherDetails.Controls.Add(this.label4);
            this.TbVoucherDetails.Controls.Add(this.label17);
            this.TbVoucherDetails.Controls.Add(this.label11);
            this.TbVoucherDetails.Controls.Add(this.label12);
            this.TbVoucherDetails.Controls.Add(this.label16);
            this.TbVoucherDetails.Controls.Add(this.TxtBankBranch);
            this.TbVoucherDetails.Controls.Add(this.label3);
            this.TbVoucherDetails.Controls.Add(this.label13);
            this.TbVoucherDetails.Controls.Add(this.TxtAmount);
            this.TbVoucherDetails.Controls.Add(this.label14);
            this.TbVoucherDetails.Controls.Add(this.TxtSubledger);
            this.TbVoucherDetails.Controls.Add(this.MskMiti);
            this.TbVoucherDetails.Controls.Add(this.label2);
            this.TbVoucherDetails.Controls.Add(this.MskDate);
            this.TbVoucherDetails.Controls.Add(this.label15);
            this.TbVoucherDetails.Location = new System.Drawing.Point(4, 29);
            this.TbVoucherDetails.Name = "TbVoucherDetails";
            this.TbVoucherDetails.Padding = new System.Windows.Forms.Padding(3);
            this.TbVoucherDetails.Size = new System.Drawing.Size(701, 288);
            this.TbVoucherDetails.TabIndex = 0;
            this.TbVoucherDetails.Text = "VOUCHER DETAILS";
            // 
            // BtnDrawOn
            // 
            this.BtnDrawOn.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDrawOn.Image = ((System.Drawing.Image)(resources.GetObject("BtnDrawOn.Image")));
            this.BtnDrawOn.Location = new System.Drawing.Point(668, 146);
            this.BtnDrawOn.Name = "BtnDrawOn";
            this.BtnDrawOn.Size = new System.Drawing.Size(26, 24);
            this.BtnDrawOn.TabIndex = 186;
            this.BtnDrawOn.TabStop = false;
            this.BtnDrawOn.UseVisualStyleBackColor = false;
            this.BtnDrawOn.Click += new System.EventHandler(this.BtnDrawOn_Click);
            // 
            // BtnBranch
            // 
            this.BtnBranch.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBranch.Image = ((System.Drawing.Image)(resources.GetObject("BtnBranch.Image")));
            this.BtnBranch.Location = new System.Drawing.Point(668, 173);
            this.BtnBranch.Name = "BtnBranch";
            this.BtnBranch.Size = new System.Drawing.Size(26, 24);
            this.BtnBranch.TabIndex = 185;
            this.BtnBranch.TabStop = false;
            this.BtnBranch.UseVisualStyleBackColor = false;
            this.BtnBranch.Click += new System.EventHandler(this.BtnBranch_Click);
            // 
            // BtnBankDesc
            // 
            this.BtnBankDesc.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBankDesc.Image = ((System.Drawing.Image)(resources.GetObject("BtnBankDesc.Image")));
            this.BtnBankDesc.Location = new System.Drawing.Point(339, 173);
            this.BtnBankDesc.Name = "BtnBankDesc";
            this.BtnBankDesc.Size = new System.Drawing.Size(26, 24);
            this.BtnBankDesc.TabIndex = 184;
            this.BtnBankDesc.TabStop = false;
            this.BtnBankDesc.UseVisualStyleBackColor = false;
            this.BtnBankDesc.Click += new System.EventHandler(this.BtnBankDesc_Click);
            // 
            // BtnRemarks
            // 
            this.BtnRemarks.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnRemarks.Image = ((System.Drawing.Image)(resources.GetObject("BtnRemarks.Image")));
            this.BtnRemarks.Location = new System.Drawing.Point(669, 258);
            this.BtnRemarks.Name = "BtnRemarks";
            this.BtnRemarks.Size = new System.Drawing.Size(26, 24);
            this.BtnRemarks.TabIndex = 183;
            this.BtnRemarks.TabStop = false;
            this.BtnRemarks.UseVisualStyleBackColor = false;
            this.BtnRemarks.Click += new System.EventHandler(this.BtnRemarks_Click);
            // 
            // BtnAgent
            // 
            this.BtnAgent.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAgent.Image = ((System.Drawing.Image)(resources.GetObject("BtnAgent.Image")));
            this.BtnAgent.Location = new System.Drawing.Point(668, 200);
            this.BtnAgent.Name = "BtnAgent";
            this.BtnAgent.Size = new System.Drawing.Size(26, 24);
            this.BtnAgent.TabIndex = 183;
            this.BtnAgent.TabStop = false;
            this.BtnAgent.UseVisualStyleBackColor = false;
            this.BtnAgent.Click += new System.EventHandler(this.BtnAgent_Click);
            // 
            // BtnDepartment
            // 
            this.BtnDepartment.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDepartment.Image = ((System.Drawing.Image)(resources.GetObject("BtnDepartment.Image")));
            this.BtnDepartment.Location = new System.Drawing.Point(340, 228);
            this.BtnDepartment.Name = "BtnDepartment";
            this.BtnDepartment.Size = new System.Drawing.Size(26, 24);
            this.BtnDepartment.TabIndex = 182;
            this.BtnDepartment.TabStop = false;
            this.BtnDepartment.UseVisualStyleBackColor = false;
            this.BtnDepartment.Click += new System.EventHandler(this.BtnDepartment_Click);
            // 
            // BtnSubledger
            // 
            this.BtnSubledger.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSubledger.Image = ((System.Drawing.Image)(resources.GetObject("BtnSubledger.Image")));
            this.BtnSubledger.Location = new System.Drawing.Point(339, 201);
            this.BtnSubledger.Name = "BtnSubledger";
            this.BtnSubledger.Size = new System.Drawing.Size(26, 24);
            this.BtnSubledger.TabIndex = 181;
            this.BtnSubledger.TabStop = false;
            this.BtnSubledger.UseVisualStyleBackColor = false;
            this.BtnSubledger.Click += new System.EventHandler(this.BtnSubledger_Click);
            // 
            // BtnLedger
            // 
            this.BtnLedger.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnLedger.Image = ((System.Drawing.Image)(resources.GetObject("BtnLedger.Image")));
            this.BtnLedger.Location = new System.Drawing.Point(668, 119);
            this.BtnLedger.Name = "BtnLedger";
            this.BtnLedger.Size = new System.Drawing.Size(26, 24);
            this.BtnLedger.TabIndex = 180;
            this.BtnLedger.TabStop = false;
            this.BtnLedger.UseVisualStyleBackColor = false;
            this.BtnLedger.Click += new System.EventHandler(this.BtnLedger_Click);
            // 
            // BtnBank
            // 
            this.BtnBank.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBank.Image = ((System.Drawing.Image)(resources.GetObject("BtnBank.Image")));
            this.BtnBank.Location = new System.Drawing.Point(668, 65);
            this.BtnBank.Name = "BtnBank";
            this.BtnBank.Size = new System.Drawing.Size(26, 24);
            this.BtnBank.TabIndex = 179;
            this.BtnBank.TabStop = false;
            this.BtnBank.UseVisualStyleBackColor = false;
            this.BtnBank.Click += new System.EventHandler(this.BtnBank_Click);
            // 
            // BtnVno
            // 
            this.BtnVno.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnVno.Image = ((System.Drawing.Image)(resources.GetObject("BtnVno.Image")));
            this.BtnVno.Location = new System.Drawing.Point(287, 4);
            this.BtnVno.Name = "BtnVno";
            this.BtnVno.Size = new System.Drawing.Size(26, 24);
            this.BtnVno.TabIndex = 178;
            this.BtnVno.TabStop = false;
            this.BtnVno.UseVisualStyleBackColor = false;
            this.BtnVno.Click += new System.EventHandler(this.BtnVno_Click);
            // 
            // clsSeparator2
            // 
            this.clsSeparator2.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator2.Location = new System.Drawing.Point(6, 29);
            this.clsSeparator2.Name = "clsSeparator2";
            this.clsSeparator2.Size = new System.Drawing.Size(689, 2);
            this.clsSeparator2.TabIndex = 12;
            this.clsSeparator2.TabStop = false;
            // 
            // TbAttachment
            // 
            this.TbAttachment.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.TbAttachment.Controls.Add(this.LinkAttachment1);
            this.TbAttachment.Controls.Add(this.BtnAttachment1);
            this.TbAttachment.Controls.Add(this.LblAttachment1);
            this.TbAttachment.Controls.Add(this.PAttachment1);
            this.TbAttachment.Location = new System.Drawing.Point(4, 29);
            this.TbAttachment.Name = "TbAttachment";
            this.TbAttachment.Padding = new System.Windows.Forms.Padding(3);
            this.TbAttachment.Size = new System.Drawing.Size(701, 288);
            this.TbAttachment.TabIndex = 1;
            this.TbAttachment.Text = "ATTACHMENT";
            // 
            // LinkAttachment1
            // 
            this.LinkAttachment1.AutoSize = true;
            this.LinkAttachment1.Location = new System.Drawing.Point(14, 218);
            this.LinkAttachment1.Name = "LinkAttachment1";
            this.LinkAttachment1.Size = new System.Drawing.Size(68, 20);
            this.LinkAttachment1.TabIndex = 359;
            this.LinkAttachment1.TabStop = true;
            this.LinkAttachment1.Text = "Preview";
            this.LinkAttachment1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkAttachment1_LinkClicked);
            // 
            // BtnAttachment1
            // 
            this.BtnAttachment1.Location = new System.Drawing.Point(6, 253);
            this.BtnAttachment1.Name = "BtnAttachment1";
            this.BtnAttachment1.Size = new System.Drawing.Size(116, 32);
            this.BtnAttachment1.TabIndex = 357;
            this.BtnAttachment1.Text = "Attachment ";
            this.BtnAttachment1.UseVisualStyleBackColor = true;
            this.BtnAttachment1.Click += new System.EventHandler(this.BtnAttachment1_Click);
            // 
            // LblAttachment1
            // 
            this.LblAttachment1.AutoSize = true;
            this.LblAttachment1.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblAttachment1.Location = new System.Drawing.Point(12, 9);
            this.LblAttachment1.Name = "LblAttachment1";
            this.LblAttachment1.Size = new System.Drawing.Size(90, 19);
            this.LblAttachment1.TabIndex = 358;
            this.LblAttachment1.Text = "Attachment";
            // 
            // PAttachment1
            // 
            this.PAttachment1.Location = new System.Drawing.Point(6, 31);
            this.PAttachment1.Name = "PAttachment1";
            this.PAttachment1.Size = new System.Drawing.Size(681, 216);
            this.PAttachment1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PAttachment1.TabIndex = 356;
            this.PAttachment1.TabStop = false;
            this.PAttachment1.DoubleClick += new System.EventHandler(this.PAttachment1_DoubleClick);
            // 
            // clsSeparator3
            // 
            this.clsSeparator3.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator3.Location = new System.Drawing.Point(7, 370);
            this.clsSeparator3.Name = "clsSeparator3";
            this.clsSeparator3.Size = new System.Drawing.Size(706, 2);
            this.clsSeparator3.TabIndex = 13;
            this.clsSeparator3.TabStop = false;
            // 
            // FrmPDCVoucher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(719, 412);
            this.Controls.Add(this.PanelHeader);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "FrmPDCVoucher";
            this.ShowIcon = false;
            this.Text = "POST DATED CHEQUE";
            this.Load += new System.EventHandler(this.FrmProvisionPDC_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmProvisionPDC_KeyPress);
            this.PanelHeader.ResumeLayout(false);
            this.PanelHeader.PerformLayout();
            this.TbControl.ResumeLayout(false);
            this.TbVoucherDetails.ResumeLayout(false);
            this.TbVoucherDetails.PerformLayout();
            this.TbAttachment.ResumeLayout(false);
            this.TbAttachment.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PAttachment1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton BtnNew;
        private DevExpress.XtraEditors.SimpleButton BtnDelete;
        private DevExpress.XtraEditors.SimpleButton BtnEdit;
        private DevExpress.XtraEditors.SimpleButton BtnExit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox CmbType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox CmbStatus;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private ClsSeparator clsSeparator2;
        private ClsSeparator clsSeparator3;
        private System.Windows.Forms.Button BtnVno;
        private System.Windows.Forms.Button BtnAgent;
        private System.Windows.Forms.Button BtnDepartment;
        private System.Windows.Forms.Button BtnSubledger;
        private System.Windows.Forms.Button BtnLedger;
        private System.Windows.Forms.Button BtnBank;
        private System.Windows.Forms.Button BtnBankDesc;
        private System.Windows.Forms.Button BtnBranch;
        private System.Windows.Forms.Button BtnDrawOn;
        private System.Windows.Forms.TabControl TbControl;
        private System.Windows.Forms.TabPage TbVoucherDetails;
        private System.Windows.Forms.TabPage TbAttachment;
        private System.Windows.Forms.LinkLabel LinkAttachment1;
        private System.Windows.Forms.Button BtnAttachment1;
        public System.Windows.Forms.Label LblAttachment1;
        private System.Windows.Forms.PictureBox PAttachment1;
        private ClsSeparator clsSeparator1;
        private System.Windows.Forms.Button BtnRemarks;
        private System.Windows.Forms.CheckBox ChkAttachment;
        private MrTextBox TxtVno;
        private MrMaskedTextBox MskMiti;
        private MrMaskedTextBox MskDate;
        private MrTextBox TxtDepartment;
        private MrTextBox TxtBank;
        private MrTextBox TxtChequeNo;
        private MrMaskedTextBox MskChequeMiti;
        private MrMaskedTextBox MskChequeDate;
        private MrTextBox TxtDrawnon;
        private MrTextBox TxtClientBank;
        private MrTextBox TxtBankBranch;
        private MrTextBox TxtLedger;
        private MrTextBox TxtSubledger;
        private MrTextBox TxtAgent;
        private MrTextBox TxtAmount;
        private MrTextBox TxtRemarks;
        private MrPanel PanelHeader;
    }
}