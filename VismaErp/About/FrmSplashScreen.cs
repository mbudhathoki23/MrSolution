using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Global.Common;
using MrDAL.Utility.Licensing;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows.Forms;

namespace VismaErp.About
{
    public sealed partial class FrmSplashScreen : Form
    {
        private Timer _tmr;
        public bool IsClose;

        public FrmSplashScreen()
        {
            InitializeComponent();
        }

        private void SplashScreen_Shown(object sender, EventArgs e)
        {
            _tmr = new Timer { Interval = 3000 };
            _tmr.Start();
            _tmr.Tick += Tmr_Tick;
        }

        private void Tmr_Tick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
            _tmr.Stop();
            Close();
        }

        private void SplashScreen_FormClosed(object sender, FormClosedEventArgs e)
        {
            IsClose = true;
        }

        private void FrmSplashScreen_Click(object sender, EventArgs e)
        {
            Tmr_Tick(sender, e);
        }

        private void FrmSplashScreen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar is (char)Keys.Enter)
            {
                Tmr_Tick(sender, e);
            }
        }

        private void FrmSplashScreen_Load(object sender, EventArgs e)
        {
            //await new AnalyticsService().SyncAllAsync();
            ReadLicenseAsync();
        }

        private void ReadLicenseAsync()
        {
            var fileName = Application.StartupPath + @"\MrLicense.lix";
            if (!File.Exists(fileName))
            {
                return;
            }

            var strContent = File.ReadAllText(fileName);
            var json = ObjGlobal.Decrypt(strContent);

            try
            {
                var model = JsonConvert.DeserializeObject<LicDetail>(json);
                var modelLicenseTo = model?.LicenseTo;
                if (model != null)
                {
                    var nudVersion = model.Version;
                }

                var cbxEditions = model.Edition;
                var originGroupId = model.OriginGroupId.ToString();
                var hwIds = model.HwIds;
                foreach (var t in model.Branches)
                {
                    ObjGlobal.TotalUser = t.MaxUsers.GetInt();
                    ObjGlobal.RegisterLicenseNodes = t.MaxPc.GetInt();
                    ObjGlobal.RegistrationId = t.OutletUqId;
                    var expDate = t.ExpDate;
                    var today = DateTime.Now.GetDateTime();
                    var difference = expDate - today;
                    ObjGlobal.IsLicenseExpire = difference.Days.GetInt() <= 0;
                }
            }
            catch (JsonReaderException ex)
            {
                MessageBox.Show(@"Invalid Json" + Environment.NewLine + ex.Message);
            }
            catch (Exception ex)
            {
                ex.ToNonQueryErrorResult(this);
            }
        }
    }
}