using System.ComponentModel;
using System.Windows.Forms;
using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Utility.Common
{
    partial class FrmRptSearch
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRptSearch));
            this.BtnSearch = new System.Windows.Forms.Button();
            this.gb_Options = new System.Windows.Forms.GroupBox();
            this.chk_MatchCase = new System.Windows.Forms.CheckBox();
            this.chk_WholeWordOnly = new System.Windows.Forms.CheckBox();
            this.gb_Direction = new System.Windows.Forms.GroupBox();
            this.rb_Up = new System.Windows.Forms.RadioButton();
            this.rb_Down = new System.Windows.Forms.RadioButton();
            this.TxtValue = new MrTextBox();
            this.mrGroup1 = new MrGroup();
            this.gb_Options.SuspendLayout();
            this.gb_Direction.SuspendLayout();
            this.mrGroup1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnSearch
            // 
            this.BtnSearch.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.BtnSearch.Location = new System.Drawing.Point(198, 65);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(123, 35);
            this.BtnSearch.TabIndex = 2;
            this.BtnSearch.Text = "&Search";
            this.BtnSearch.UseVisualStyleBackColor = false;
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // gb_Options
            // 
            this.gb_Options.Controls.Add(this.chk_MatchCase);
            this.gb_Options.Controls.Add(this.chk_WholeWordOnly);
            this.gb_Options.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.gb_Options.Location = new System.Drawing.Point(15, 103);
            this.gb_Options.Name = "gb_Options";
            this.gb_Options.Size = new System.Drawing.Size(302, 51);
            this.gb_Options.TabIndex = 3;
            this.gb_Options.TabStop = false;
            this.gb_Options.Text = "Options";
            // 
            // chk_MatchCase
            // 
            this.chk_MatchCase.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.chk_MatchCase.Location = new System.Drawing.Point(174, 18);
            this.chk_MatchCase.Name = "chk_MatchCase";
            this.chk_MatchCase.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_MatchCase.Size = new System.Drawing.Size(110, 23);
            this.chk_MatchCase.TabIndex = 1;
            this.chk_MatchCase.Text = "Match &Case";
            this.chk_MatchCase.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_MatchCase.UseVisualStyleBackColor = true;
            // 
            // chk_WholeWordOnly
            // 
            this.chk_WholeWordOnly.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.chk_WholeWordOnly.Location = new System.Drawing.Point(8, 18);
            this.chk_WholeWordOnly.Name = "chk_WholeWordOnly";
            this.chk_WholeWordOnly.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_WholeWordOnly.Size = new System.Drawing.Size(149, 23);
            this.chk_WholeWordOnly.TabIndex = 0;
            this.chk_WholeWordOnly.Text = "Whole &Word Only";
            this.chk_WholeWordOnly.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_WholeWordOnly.UseVisualStyleBackColor = true;
            // 
            // gb_Direction
            // 
            this.gb_Direction.Controls.Add(this.rb_Up);
            this.gb_Direction.Controls.Add(this.rb_Down);
            this.gb_Direction.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.gb_Direction.Location = new System.Drawing.Point(15, 58);
            this.gb_Direction.Name = "gb_Direction";
            this.gb_Direction.Size = new System.Drawing.Size(181, 43);
            this.gb_Direction.TabIndex = 1;
            this.gb_Direction.TabStop = false;
            this.gb_Direction.Text = "Direction";
            // 
            // rb_Up
            // 
            this.rb_Up.AutoSize = true;
            this.rb_Up.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.rb_Up.Location = new System.Drawing.Point(105, 14);
            this.rb_Up.Name = "rb_Up";
            this.rb_Up.Size = new System.Drawing.Size(47, 23);
            this.rb_Up.TabIndex = 0;
            this.rb_Up.TabStop = true;
            this.rb_Up.Text = "Up";
            this.rb_Up.UseVisualStyleBackColor = true;
            // 
            // rb_Down
            // 
            this.rb_Down.AutoSize = true;
            this.rb_Down.Checked = true;
            this.rb_Down.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.rb_Down.Location = new System.Drawing.Point(8, 15);
            this.rb_Down.Name = "rb_Down";
            this.rb_Down.Size = new System.Drawing.Size(69, 23);
            this.rb_Down.TabIndex = 4;
            this.rb_Down.TabStop = true;
            this.rb_Down.Text = "Down";
            this.rb_Down.UseVisualStyleBackColor = true;
            // 
            // TxtValue
            // 
            this.TxtValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtValue.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.TxtValue.Location = new System.Drawing.Point(15, 28);
            this.TxtValue.Name = "TxtValue";
            this.TxtValue.Size = new System.Drawing.Size(302, 25);
            this.TxtValue.TabIndex = 0;
            this.TxtValue.TextChanged += new System.EventHandler(this.TxtSearchValue_TextChanged);
            // 
            // mrGroup1
            // 
            this.mrGroup1.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup1.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup1.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup1.BorderColor = System.Drawing.Color.White;
            this.mrGroup1.BorderThickness = 1F;
            this.mrGroup1.Controls.Add(this.BtnSearch);
            this.mrGroup1.Controls.Add(this.TxtValue);
            this.mrGroup1.Controls.Add(this.gb_Options);
            this.mrGroup1.Controls.Add(this.gb_Direction);
            this.mrGroup1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mrGroup1.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.mrGroup1.GroupImage = null;
            this.mrGroup1.GroupTitle = "SEARCH IN REPORTS";
            this.mrGroup1.Location = new System.Drawing.Point(0, 0);
            this.mrGroup1.Name = "mrGroup1";
            this.mrGroup1.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup1.PaintGroupBox = false;
            this.mrGroup1.RoundCorners = 10;
            this.mrGroup1.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup1.ShadowControl = false;
            this.mrGroup1.ShadowThickness = 3;
            this.mrGroup1.Size = new System.Drawing.Size(333, 161);
            this.mrGroup1.TabIndex = 0;
            // 
            // FrmRptSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(333, 161);
            this.Controls.Add(this.mrGroup1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmRptSearch";
            this.ShowIcon = false;
            this.Text = "Search";
            this.Load += new System.EventHandler(this.FrmRptSearch_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmRptSearch_KeyPress);
            this.gb_Options.ResumeLayout(false);
            this.gb_Direction.ResumeLayout(false);
            this.gb_Direction.PerformLayout();
            this.mrGroup1.ResumeLayout(false);
            this.mrGroup1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private Button BtnSearch;
        private GroupBox gb_Options;
        private CheckBox chk_MatchCase;
        private CheckBox chk_WholeWordOnly;
        private GroupBox gb_Direction;
        private RadioButton rb_Up;
        private RadioButton rb_Down;
        private MrGroup mrGroup1;
        private MrTextBox TxtValue;
    }
}