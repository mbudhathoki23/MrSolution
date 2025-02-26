namespace MrBLL.Utility.Common
{
    partial class FrmDocClassTagList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.CmdCancel = new System.Windows.Forms.Button();
            this.CmdOk = new System.Windows.Forms.Button();
            this.lbl_SearchValue = new System.Windows.Forms.Label();
            this.lbl_Search = new System.Windows.Forms.Label();
            this.dgv_ClassList = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ClassList)).BeginInit();
            this.SuspendLayout();
            // 
            // CmdCancel
            // 
            this.CmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CmdCancel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmdCancel.Location = new System.Drawing.Point(203, 303);
            this.CmdCancel.Name = "CmdCancel";
            this.CmdCancel.Size = new System.Drawing.Size(60, 25);
            this.CmdCancel.TabIndex = 4;
            this.CmdCancel.Text = "&Cancel";
            this.CmdCancel.UseVisualStyleBackColor = true;
            this.CmdCancel.Click += new System.EventHandler(this.CmdCancel_Click);
            // 
            // CmdOk
            // 
            this.CmdOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.CmdOk.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmdOk.Location = new System.Drawing.Point(137, 303);
            this.CmdOk.Name = "CmdOk";
            this.CmdOk.Size = new System.Drawing.Size(60, 25);
            this.CmdOk.TabIndex = 3;
            this.CmdOk.Text = "&OK";
            this.CmdOk.UseVisualStyleBackColor = true;
            this.CmdOk.Click += new System.EventHandler(this.CmdOk_Click);
            // 
            // lbl_SearchValue
            // 
            this.lbl_SearchValue.BackColor = System.Drawing.SystemColors.HighlightText;
            this.lbl_SearchValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_SearchValue.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_SearchValue.Location = new System.Drawing.Point(65, 275);
            this.lbl_SearchValue.Name = "lbl_SearchValue";
            this.lbl_SearchValue.Size = new System.Drawing.Size(240, 20);
            this.lbl_SearchValue.TabIndex = 2;
            this.lbl_SearchValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_SearchValue.TextChanged += new System.EventHandler(this.lbl_SearchValue_TextChanged);
            // 
            // lbl_Search
            // 
            this.lbl_Search.AutoSize = true;
            this.lbl_Search.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Search.Location = new System.Drawing.Point(12, 277);
            this.lbl_Search.Name = "lbl_Search";
            this.lbl_Search.Size = new System.Drawing.Size(46, 15);
            this.lbl_Search.TabIndex = 21;
            this.lbl_Search.Text = "Search";
            // 
            // dgv_ClassList
            // 
            this.dgv_ClassList.AllowUserToAddRows = false;
            this.dgv_ClassList.AllowUserToDeleteRows = false;
            this.dgv_ClassList.AllowUserToResizeColumns = false;
            this.dgv_ClassList.AllowUserToResizeRows = false;
            this.dgv_ClassList.BackgroundColor = System.Drawing.Color.AliceBlue;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_ClassList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_ClassList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_ClassList.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_ClassList.Location = new System.Drawing.Point(2, 5);
            this.dgv_ClassList.Margin = new System.Windows.Forms.Padding(2);
            this.dgv_ClassList.Name = "dgv_ClassList";
            this.dgv_ClassList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Green;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_ClassList.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.dgv_ClassList.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgv_ClassList.RowTemplate.Height = 24;
            this.dgv_ClassList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_ClassList.Size = new System.Drawing.Size(368, 261);
            this.dgv_ClassList.TabIndex = 1;
            this.dgv_ClassList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_TagList_CellClick);
            this.dgv_ClassList.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_TagList_CellEnter);
            this.dgv_ClassList.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_TagList_CellMouseDoubleClick);
            this.dgv_ClassList.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_TagList_RowEnter);
            this.dgv_ClassList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgv_TagList_KeyDown);
            this.dgv_ClassList.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dgv_TagList_KeyPress);
            // 
            // FrmDocClassTagList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(372, 331);
            this.Controls.Add(this.dgv_ClassList);
            this.Controls.Add(this.lbl_SearchValue);
            this.Controls.Add(this.lbl_Search);
            this.Controls.Add(this.CmdCancel);
            this.Controls.Add(this.CmdOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FrmDocClassTagList";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Class Tag List";
            this.Load += new System.EventHandler(this.FrmDocClassTagList_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmDocClassTagList_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ClassList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CmdCancel;
        private System.Windows.Forms.Button CmdOk;
        private System.Windows.Forms.Label lbl_SearchValue;
        private System.Windows.Forms.Label lbl_Search;
        private System.Windows.Forms.DataGridView dgv_ClassList;
    }
}