
namespace MrBLL.Utility.Social
{
    partial class UcWebBrowser
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
            this.mainRibbonControl = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.bbiHome = new DevExpress.XtraBars.BarButtonItem();
            this.bbiBack = new DevExpress.XtraBars.BarButtonItem();
            this.bbiForward = new DevExpress.XtraBars.BarButtonItem();
            this.bbiReload = new DevExpress.XtraBars.BarButtonItem();
            this.bbiStop = new DevExpress.XtraBars.BarButtonItem();
            this.mainRibbonPage = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.mainRibbonPageGroup = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
            ((System.ComponentModel.ISupportInitialize)(this.mainRibbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.webView21)).BeginInit();
            this.SuspendLayout();
            // 
            // mainRibbonControl
            // 
            this.mainRibbonControl.ColorScheme = DevExpress.XtraBars.Ribbon.RibbonControlColorScheme.DarkBlue;
            this.mainRibbonControl.ExpandCollapseItem.Id = 0;
            this.mainRibbonControl.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.mainRibbonControl.ExpandCollapseItem,
            this.mainRibbonControl.SearchEditItem,
            this.bbiHome,
            this.bbiBack,
            this.bbiForward,
            this.bbiReload,
            this.bbiStop});
            this.mainRibbonControl.Location = new System.Drawing.Point(0, 0);
            this.mainRibbonControl.MaxItemId = 10;
            this.mainRibbonControl.Name = "mainRibbonControl";
            this.mainRibbonControl.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.mainRibbonPage});
            this.mainRibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            this.mainRibbonControl.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            this.mainRibbonControl.Size = new System.Drawing.Size(1324, 131);
            this.mainRibbonControl.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            // 
            // bbiHome
            // 
            this.bbiHome.Caption = "Home";
            this.bbiHome.Id = 2;
            this.bbiHome.ImageOptions.ImageUri.Uri = "Save";
            this.bbiHome.Name = "bbiHome";
            this.bbiHome.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BbiHome_ItemClick);
            // 
            // bbiBack
            // 
            this.bbiBack.Caption = "Back";
            this.bbiBack.Id = 3;
            this.bbiBack.ImageOptions.Image = global::MrBLL.Properties.Resources.Previous;
            this.bbiBack.ImageOptions.ImageUri.Uri = "SaveAndClose";
            this.bbiBack.Name = "bbiBack";
            this.bbiBack.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BbiBack_ItemClick);
            // 
            // bbiForward
            // 
            this.bbiForward.Caption = "Forward";
            this.bbiForward.Id = 4;
            this.bbiForward.ImageOptions.Image = global::MrBLL.Properties.Resources.Next;
            this.bbiForward.ImageOptions.ImageUri.Uri = "SaveAndNew";
            this.bbiForward.Name = "bbiForward";
            this.bbiForward.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BbiForward_ItemClick);
            // 
            // bbiReload
            // 
            this.bbiReload.Caption = "Reload";
            this.bbiReload.Id = 5;
            this.bbiReload.ImageOptions.Image = global::MrBLL.Properties.Resources.referesh;
            this.bbiReload.ImageOptions.ImageUri.Uri = "Reset";
            this.bbiReload.Name = "bbiReload";
            this.bbiReload.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BbiReload_ItemClick);
            // 
            // bbiStop
            // 
            this.bbiStop.Caption = "Stop";
            this.bbiStop.Id = 6;
            this.bbiStop.ImageOptions.ImageUri.Uri = "Delete";
            this.bbiStop.Name = "bbiStop";
            this.bbiStop.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BbiStop_ItemClick);
            // 
            // mainRibbonPage
            // 
            this.mainRibbonPage.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainRibbonPage.Appearance.Options.UseFont = true;
            this.mainRibbonPage.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.mainRibbonPageGroup});
            this.mainRibbonPage.MergeOrder = 0;
            this.mainRibbonPage.Name = "mainRibbonPage";
            this.mainRibbonPage.Text = "Home";
            // 
            // mainRibbonPageGroup
            // 
            this.mainRibbonPageGroup.AllowTextClipping = false;
            this.mainRibbonPageGroup.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
            this.mainRibbonPageGroup.ItemLinks.Add(this.bbiHome);
            this.mainRibbonPageGroup.ItemLinks.Add(this.bbiBack);
            this.mainRibbonPageGroup.ItemLinks.Add(this.bbiForward);
            this.mainRibbonPageGroup.ItemLinks.Add(this.bbiStop);
            this.mainRibbonPageGroup.ItemLinks.Add(this.bbiReload);
            this.mainRibbonPageGroup.Name = "mainRibbonPageGroup";
            this.mainRibbonPageGroup.Text = "Tasks";
            // 
            // webView21
            // 
            this.webView21.CreationProperties = null;
            this.webView21.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webView21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webView21.Location = new System.Drawing.Point(0, 131);
            this.webView21.Name = "webView21";
            this.webView21.Size = new System.Drawing.Size(1324, 619);
            this.webView21.TabIndex = 3;
            this.webView21.ZoomFactor = 1D;
            // 
            // UcWebBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.Controls.Add(this.webView21);
            this.Controls.Add(this.mainRibbonControl);
            this.Name = "UcWebBrowser";
            this.Size = new System.Drawing.Size(1324, 750);
            this.Load += new System.EventHandler(this.UcWebBrowser_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mainRibbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.webView21)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraBars.Ribbon.RibbonControl mainRibbonControl;
        private DevExpress.XtraBars.Ribbon.RibbonPage mainRibbonPage;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup mainRibbonPageGroup;
        private DevExpress.XtraBars.BarButtonItem bbiHome;
        private DevExpress.XtraBars.BarButtonItem bbiBack;
        private DevExpress.XtraBars.BarButtonItem bbiForward;
        private DevExpress.XtraBars.BarButtonItem bbiReload;
        private DevExpress.XtraBars.BarButtonItem bbiStop;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
    }
}
