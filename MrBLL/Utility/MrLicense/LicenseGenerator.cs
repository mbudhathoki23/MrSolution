using MrDAL.Control.ControlsEx.NotifyPanel;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Models.Common;
using MrDAL.Utility.Licensing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MrBLL.Utility.MrLicense;

public partial class LicenseGenerator : MrForm
{
    public LicenseGenerator()
    {
        InitializeComponent();
        bsOutlets.DataSource = new List<LicBranchModel>();
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void LicenseGenerator_Load(object sender, EventArgs e)
    {
        bsOutletTypes.DataSource = Enum.GetValues(typeof(OutletNature)).OfType<OutletNature>()
            .Select(x => new ValueModel<OutletNature, string>(x, x.GetDescription())).ToList();
        cbxEditions.DataSource = Enum.GetValues(typeof(LicEdition)).OfType<LicEdition>().ToList();

        colOutletType.ValueMember = "Item1";
        colOutletType.DisplayMember = "Item2";
    }

    private bool InputFieldsValid()
    {
        if (txtLicenseTo.Text.IsBlankOrEmpty())
        {
            this.NotifyValidationError(txtLicenseTo, "Enter licensed to value.");
            return false;
        }

        if (cbxEditions.SelectedItem == null)
        {
            this.NotifyValidationError(cbxEditions, "License edition not selected.");
            return false;
        }

        if (txtOriginGroupId.Text.IsBlankOrEmpty())
        {
            this.NotifyValidationError(txtOriginGroupId, "Invalid client group Id value.");
            return false;
        }

        if (gridHardwareIds.Rows.Cast<DataGridViewRow>().All(x => x.IsNewRow))
        {
            this.NotifyValidationError(gridHardwareIds, "No hardware Id entered for this license.");
            return false;
        }

        if (gridOutlets.Rows.Cast<DataGridViewRow>().All(x => x.IsNewRow))
        {
            this.NotifyValidationError(gridOutlets, "No outlet or branch entered for this license.");
            return false;
        }

        return true;
    }

    private void btnGenerateLicense_Click(object sender, EventArgs e)
    {
        if (!InputFieldsValid()) return;

        var model = new LicDetail
        {
            OriginGroupId = Guid.Parse(txtOriginGroupId.Text),
            ClientType = LicClient.Consumer,
            DateGenerated = DateTime.Now,
            Edition = (LicEdition)cbxEditions.SelectedItem,
            HwIds = gridHardwareIds.Rows.OfType<DataGridViewRow>().Where(x => !x.IsNewRow)
                .Select(x => x.Cells[0].Value.ToString()).ToList(),
            LicenseTo = txtLicenseTo.Text.Trim(),
            MultiBranch = rdoMultiBranch.Checked,
            Version = (uint)nudVersion.Value,
            SubscriptionId = Guid.NewGuid(),
            Branches = new List<LicBranch>()
        };

        foreach (DataGridViewRow row in gridOutlets.Rows)
        {
            if (row.IsNewRow) continue;
            var licBranch = (LicBranchModel)row.DataBoundItem;
            model.Branches.Add(new LicBranch(licBranch.OriginId, (OutletNature)row.Cells[0].Value,
                licBranch.MaxUsers, licBranch.MaxPc, licBranch.ServerName, licBranch.ExpDate));
        }

        var json = model.ToJson();
        File.WriteAllText(@"app.lix", EncryptOrDecrypt.EncryptString(json, LicenseHandler.LixKix));
        this.NotifySuccess("License generated successfully.");
    }

    private void llReadLicense_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        gridOutlets.Rows.Clear();

        txtLicenseTo.Clear();
        txtOriginGroupId.Clear();

        var dialog = new OpenFileDialog
        {
            Title = @"Open license file",
            CheckFileExists = true,
            CheckPathExists = true,
            Multiselect = false,
            Filter = @"All License Files|*.lix"
        };

        if (dialog.ShowDialog() != DialogResult.OK) return;

        // get the contents of the file
        var strContent = File.ReadAllText(dialog.FileName);
        var json = EncryptOrDecrypt.DecryptString(strContent, LicenseHandler.LixKix);

        try
        {
            var model = JsonConvert.DeserializeObject<LicDetail>(json);

            txtLicenseTo.Text = model.LicenseTo;
            nudVersion.Value = model.Version;
            cbxEditions.SelectedItem = model.Edition;
            nudVersion.Value = model.Version;
            txtOriginGroupId.Text = model.OriginGroupId.ToString();

            if (model.MultiBranch) rdoMultiBranch.Checked = true;
            else rdoSingleBranch.Checked = true;

            bsHardwareIds.DataSource = model.HwIds;
            bsOutlets.DataSource = model.Branches;
        }
        catch (JsonReaderException ex)
        {
            MessageBox.Show(@"Invalid Json" + Environment.NewLine + ex.Message);
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
        }
    }

    private void btnGenClientId_Click(object sender, EventArgs e)
    {
        txtOriginGroupId.Text = Guid.NewGuid().ToString();
    }

    private void gridHardwareIds_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.ColumnIndex != colRemoveHwId.Index || e.RowIndex == -1) return;
        if (gridHardwareIds.Rows[e.RowIndex].IsNewRow) return;
        gridHardwareIds.Rows.Remove(gridHardwareIds.Rows[e.RowIndex]);
    }

    private void gridOutlets_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.ColumnIndex != colRemoveOutlet.Index || e.RowIndex == -1) return;
        if (gridOutlets.Rows[e.RowIndex].IsNewRow) return;
        gridOutlets.Rows.Remove(gridOutlets.Rows[e.RowIndex]);
    }

    public class LicBranchModel
    {
        public LicBranchModel()
        {
            OriginId = Guid.NewGuid();
            MaxPc = 1;
            MaxUsers = 1;
            ExpDate = DateTime.Today.AddDays(7);
            Nature = OutletNature.AIMS;
        }

        public Guid OriginId { get; set; }
        public OutletNature Nature { get; set; }
        public DateTime ExpDate { get; set; }
        public uint MaxPc { get; set; }
        public uint MaxUsers { get; set; }
        public string ServerName { get; set; }
    }
}