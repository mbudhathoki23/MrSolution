using MrDAL.Control.ControlsEx.NotifyPanel;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Domains.Shared.UserAccessControl;
using MrDAL.Domains.Shared.UserAccessControl.Models;
using MrDAL.Global.Common;
using MrDAL.Global.Dialogs;
using MrDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DatabaseModule.Setup.CompanyMaster;
using UacAction = MrDAL.Domains.Shared.UserAccessControl.Models.UacAction;

namespace MrBLL.Setup.UserSetup;

public partial class FrmUserAccessControl : MrForm
{
    private readonly UacService _uacService;

    public FrmUserAccessControl()
    {
        InitializeComponent();

        _uacService = new UacService();
        clbFeatureActions.DisplayMember = "Item2";
        clbFeatureActions.ValueMember = "Item1";

        tvExistingActions.Sorted = tvFeatures.Sorted = true;
    }

    private async void FrmEnableMenu_Load(object sender, EventArgs e)
    {
        await LoadBranchesAsync();
        await LoadNormalUserRolesAsync();

        if (CmbBranch.Items.Count == 1) CmbBranch.SelectedIndex = 0;
        if (lbRoles.Items.Count == 1) lbRoles.SelectedIndex = 0;
    }

    private void RdoActions_CheckedChanged(object sender, EventArgs e)
    {
        if (rdoAllActions.Checked)
        {
            for (var i = 0; i < clbFeatureActions.Items.Count; i++)
                clbFeatureActions.SetItemCheckState(i, CheckState.Checked);
        }
        else
        {
            for (var i = 0; i < clbFeatureActions.Items.Count; i++)
                clbFeatureActions.SetItemCheckState(i, CheckState.Unchecked);
        }
    }

    private void BtnClose_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void TvFeatures_AfterSelect(object sender, TreeViewEventArgs e)
    {
        clbFeatureActions.Items.Clear();
        if (e.Node?.Tag is not TreeListModel model || !model.AvailActions.Any() || lbRoles.SelectedItem is not UserRoleModel role) return;
        if (CmbBranch.SelectedItem is not Branch) return;

        foreach (var action in model.AvailActions)
        {
            clbFeatureActions.Items.Add(new ValueModel<UacAction, string>(action, action.GetDescription()), role.IsAdmin || model.SelectedActions.Contains(action));
        }
    }

    private async Task<bool> LoadNormalUserRolesAsync()
    {
        lbRoles.SelectedIndexChanged -= lbRoles_SelectedIndexChanged;
        lbRoles.DataSource = null;

        var response = await _uacService.GetRolesAsync(UacRoleType.Normal, null);
        if (!response.Success)
        {
            response.ShowErrorDialog("Unable to fetch user roles.");
            return false;
        }

        lbRoles.DataSource = response.List;
        lbRoles.DisplayMember = "Name";
        lbRoles.ValueMember = "Id";
        lbRoles.SelectedIndex = -1;

        lbRoles.SelectedIndexChanged += lbRoles_SelectedIndexChanged;
        return true;
    }

    private async Task<bool> LoadBranchesAsync()
    {
        CmbBranch.SelectedIndexChanged -= cbxBranches_SelectedIndexChanged;
        CmbBranch.DataSource = null;
        var response = await QueryUtils.GetListAsync<Branch>();
        if (!response.Success)
        {
            response.ShowErrorDialog("Error fetching branches list.");
            return false;
        }

        CmbBranch.DataSource = response.List;
        CmbBranch.DisplayMember = "Branch_Name";
        CmbBranch.ValueMember = "Branch_Id";
        CmbBranch.SelectedIndex = -1;
        CmbBranch.SelectedIndexChanged += cbxBranches_SelectedIndexChanged;
        return true;
    }

    private async Task PopulateFeaturesTreeAsync(int roleId, int? branchId)
    {
        tvFeatures.Nodes.Clear();
        tvExistingActions.Nodes.Clear();

        var modules = Enum.GetValues(typeof(UacModule)).OfType<UacModule>().ToList();
        var groups = Enum.GetValues(typeof(UacFeatureGroup)).OfType<UacFeatureGroup>().ToList();
        var allFeatures = StaticModels.GetAllFeatures();
        var permResponse = await _uacService.GetPermissionsForRoleAsync(roleId, branchId);
        if (!permResponse.Success)
        {
            permResponse.ShowErrorDialog();
            return;
        }

        var listModels = (from feature in allFeatures
            join perm in permResponse.List on feature.Feature equals perm.Feature into temp
            from nPerm in temp.DefaultIfEmpty()
            select new TreeListModel
            {
                PermissionId = nPerm?.Id,
                Title = feature.FeatureName,
                Feature = feature.Feature,
                FeatureGroup = feature.FeatureGroup,
                FeatureGroupName = feature.FeatureGroupName,
                AvailActions = feature.Actions,
                SelectedActions = (List<UacAction>)(nPerm == null ? new List<UacAction>() : nPerm.Actions)
            }).ToList();

        modules.ForEach(module =>
        {
            var moduleNode = new TreeNode(module.GetDescription()) { Tag = module };

            var featureGroups = allFeatures.Where(x => x.Module == module && x.FeatureGroup.HasValue)
                .Select(g => g.FeatureGroup).Distinct().ToList();
            var nFeatures = allFeatures.Where(x => x.Module == module && x.FeatureGroup == null)
                .Select(x => x.Feature).Distinct().ToList();

            nFeatures.ForEach(feature =>
            {
                var node = new TreeNode(feature.GetDescription()) { Tag = listModels.FirstOrDefault(x => x.Feature == feature) };
                moduleNode.Nodes.Add(node);
            });

            featureGroups.ForEach(grp =>
            {
                var grpNode = new TreeNode(grp.GetDescription()) { Tag = grp };
                moduleNode.Nodes.Add(grpNode);

                var features = listModels.Where(x => x.FeatureGroup == grp).ToList();
                grpNode.Nodes.AddRange(features.Select(x => new TreeNode(x.Title) { Tag = x }).ToArray());
            });

            tvFeatures.Nodes.Add(moduleNode);
        });

        foreach (var fp in permResponse.List)
        {
            var featureNode = new TreeNode(fp.FeatureName) { Tag = fp };

            if (fp.Actions != null && fp.Actions.Any())
                featureNode.Nodes.AddRange(fp.Actions.Select(a => new TreeNode(a.GetDescription())).ToArray());
            tvExistingActions.Nodes.Add(featureNode);
        }

        tvFeatures.ExpandAll();
        tvExistingActions.ExpandAll();
    }

    private async void btnSave_Click(object sender, EventArgs e)
    {
        if (CmbBranch.SelectedItem is not Branch branch)
        {
            this.NotifyError("No branch selected");
            return;
        }

        if (lbRoles.SelectedItem is not UserRoleModel role)
        {
            this.NotifyError("No role selected");
            return;
        }

        var allNodes = tvFeatures.EnumerateAllTreeNodes<TreeNode>(tvFeatures);
        var treeModels = allNodes.Where(x => x.Tag is TreeListModel).Select(s => (TreeListModel)s.Tag).ToList();
        treeModels = treeModels.Where(x => x.SelectedActions.Any()).ToList();

        if (!treeModels.Any())
        {
            this.NotifyError("No feature and action permissions selected to apply.");
            return;
        }

        var response = await _uacService.ApplyPermissionsAsync(role.Id, treeModels.Select(x => new UacEntryModel(branch.Branch_ID, x.SelectedActions, (int)x.Feature)).ToList(), ObjGlobal.LogInUser);

        if (!response.Value)
        {
            response.ShowErrorDialog();
            return;
        }

        this.NotifySuccess("Saved Successfully");
    }

    private async void lbRoles_SelectedIndexChanged(object sender, EventArgs e)
    {
        tvFeatures.Nodes.Clear();
        if (lbRoles.SelectedIndex == -1 || lbRoles.SelectedItem is not UserRoleModel model) return;

        if (CmbBranch.SelectedItem is not Branch branch)
        {
            this.NotifyValidationError(CmbBranch, "No branch selected");
            return;
        }

        await PopulateFeaturesTreeAsync(model.Id, branch.Branch_ID);
    }

    private void cbxBranches_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    private void btnApplyAction_Click(object sender, EventArgs e)
    {
        if (tvFeatures.SelectedNode.Tag is not TreeListModel model) return;

        if (clbFeatureActions.CheckedItems.Count == 0)
        {
            this.NotifyError("No action marked");
            return;
        }

        var checkedItems = clbFeatureActions.CheckedItems.OfType<ValueModel<UacAction, string>>().ToList();
        model.SelectedActions = checkedItems.Select(x => x.Item1).ToList();
        this.NotifyHint("Applied Successfully");
    }

    private async void btnResetActions_Click_1(object sender, EventArgs e)
    {
        if (tvFeatures.SelectedNode.Tag is not TreeListModel model) return;
        if (lbRoles.SelectedItem is not UserRoleModel role) return;
        if (CmbBranch.SelectedItem is not Branch branch) return;

        var response = await _uacService.GetPermissionsForRoleAsync(role.Id, branch.Branch_ID);
        if (!response.Success)
        {
            response.ShowErrorDialog();
            return;
        }

        var perm = response.List.FirstOrDefault(x => x.Feature == model.Feature);
        if (perm == null) return;

        for (var i = 0; i < clbFeatureActions.Items.Count; i++)
        {
            var item = (ValueModel<UacAction, string>)clbFeatureActions.Items[i];
            clbFeatureActions.SetItemChecked(i, perm.Actions.Contains(item.Item1));
        }
    }

    //private void btnAddAction_Click(object sender, EventArgs e)
    //{
    //    //if (tvFeatures.SelectedNode?.Tag is not UacFeatureBox box)
    //    //{
    //    //    this.NotifyError("No feature selected from the tree.");
    //    //    return;
    //    //}

    //    //var permissions = bsApplyPermissions.Count == 0
    //    //    ? new List<ApplyPermissionModel>() : bsApplyPermissions.List.OfType<ApplyPermissionModel>().ToList();

    //    //var checkedActions = clbFeatureActions.CheckedItems.Count == 0 ?
    //    //    new List<ValueModel<UacAction, string>>() :
    //    //    clbFeatureActions.CheckedItems.OfType<ValueModel<UacAction, string>>().ToList();

    //    //checkedActions.ForEach(x =>
    //    //{
    //    //    var ePerm = permissions.FirstOrDefault(p => p.Feature == box.Feature && p.Actions.Contains(x.Item1));

    //    //});
    //}

    private void BtnExpandCollapseTree_Click(object sender, EventArgs e)
    {
        var expanded = tvFeatures.Nodes.Cast<TreeNode>().Any(node => node.IsExpanded);

        if (expanded)
        {
            tvFeatures.CollapseAll();
        }
        else
        {
            tvFeatures.ExpandAll();
        }
    }

    private void BtnTreeCheckNone_Click(object sender, EventArgs e)
    {
        foreach (TreeNode node in tvFeatures.Nodes)
        {
            tvFeatures.CheckAllChildNodes(node, false);
        }
    }

    private void BtnTreeCheckAll_Click(object sender, EventArgs e)
    {
        foreach (TreeNode node in tvFeatures.Nodes)
        {
            tvFeatures.CheckAllChildNodes(node, true);
        }
    }

    private class TreeListModel
    {
        public int? PermissionId { get; set; }
        public string Title { get; set; }
        public UacAccessFeature Feature { get; set; }
        public UacFeatureGroup? FeatureGroup { get; set; }
        public string FeatureGroupName { get; set; }
        public IList<UacAction> AvailActions { get; set; }
        public List<UacAction> SelectedActions { get; set; }
    }
}