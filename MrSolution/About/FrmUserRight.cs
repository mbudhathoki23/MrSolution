using DevExpress.Utils.Extensions;
using MrDAL.Control.ControlsEx.NotifyPanel;
using MrDAL.Core.Utils;
using MrDAL.Global.Common;
using MrDAL.Master;
using MrDAL.Master.Interface;
using MrDAL.Models.Custom;
using MrDAL.Setup.Interface;
using MrDAL.Setup.UserSetup;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace MrSolution.About;

public partial class FrmUserRight : Form
{
    // USER ACCESS CONTROL
    #region --------------- USER ACCESS CONTROL ---------------
    public FrmUserRight()
    {
        InitializeComponent();
        _forms = new List<UserAccessFormControl>();
        _objMaster = new ClsMasterSetup();
        _control = new UserAccessControlRepository();
        CmbUserRole.SelectedIndexChanged -= CmbUserRole_SelectedIndexChanged;
        ObjGlobal.BindUserRoleAsync(CmbUserRole);
        CmbUserRole.SelectedIndexChanged += CmbUserRole_SelectedIndexChanged;
        CmbUser.SelectedIndexChanged -= CmbUser_SelectedIndexChanged;
        BindUsers();
        CmbUser.SelectedIndexChanged += CmbUser_SelectedIndexChanged;
        mrGrid1.ReadOnly = false;
        mrGrid1.Columns[0].ReadOnly = true;
    }
    [Obsolete]
    private void FrmUserRight_Load(object sender, EventArgs e)
    {
        CloneMyMenu();
        var menuTag = "";
        MrMenu.Items.OfType<ToolStripMenuItem>().Where(i => i.DropDownItems.Count > 0).ForEach(x =>
        {
            menuTag = x.Tag.ToString();
            x.MouseHover -= X_MouseHover;
            x.MouseHover += X_MouseHover;
            x.Click -= X_Click;
            x.Click += X_Click;

            x.DropDownItems.OfType<ToolStripMenuItem>().ForEach(di =>
            {
                di.MouseHover -= Di_MouseHover;
                di.MouseHover += Di_MouseHover;
                di.Click -= Di_Click;
                di.Click += Di_Click;
                if (di.DropDownItems.Count > 0)
                {
                    di.DropDownItems.OfType<ToolStripItem>().ForEach(child =>
                    {
                        if (menuTag != "Report")
                            GetForms(child);
                        RemoveEvent(child, "EventClick");
                        child.Click -= Child_ItemClicked;
                        child.Click += Child_ItemClicked;
                    });
                }
            });
        });
        SelectMenu();
    }
    private void X_MouseHover(object sender, EventArgs e)
    {
        if (!(sender is ToolStripMenuItem pItem)) return;
        if (_previousParentItem != null && _previousParentItem != pItem)
        {
            _previousParentItem.DropDown.Close();
            _previousParentItem.DropDownItems.OfType<ToolStripMenuItem>().ForEach(x =>
            {
                x.DropDown.Close();
            });
        }
        _previousParentItem = pItem;
    }
    private void X_Click(object sender, EventArgs e)
    {
        if (!(sender is ToolStripMenuItem pItem)) return;
        if (_previousParentItem != null && _previousParentItem != pItem)
        {
            _previousParentItem.DropDown.Close();
            _previousParentItem.DropDownItems.OfType<ToolStripMenuItem>().ForEach(x =>
            {
                x.DropDown.Close();
            });
        }
        else
        {
            pItem.DropDown.AutoClose = false;
            _previousParentItem = pItem;
        }
    }
    private void Di_MouseHover(object sender, EventArgs e)
    {
        if (!(sender is ToolStripMenuItem dItem)) return;
        if (_previousItem != null)
        {
            _previousItem.DropDown.Close();
            _previousItem = null;
        }
        _previousItem = dItem;
        dItem.DropDown.AutoClose = false;
        dItem.DropDown.Show();
    }
    private void Di_Click(object sender, EventArgs e)
    {
        try
        {
            if (sender is not ToolStripMenuItem dItem)
            {
                return;
            }
            dItem.Checked = !dItem.Checked;
            dItem.DropDown.AutoClose = false;
            _previousItem = dItem;
            dItem.DropDownItems.OfType<ToolStripMenuItem>().ForEach(child =>
            {
                string mainMenu = dItem.Name;
                string ChildMenu = child.Name;
                child.Checked = dItem.Checked;
                CheckedGridViewItems(mainMenu, ChildMenu);
            });
        }
        catch
        {
            // ignored
        }
    }
    public void CheckedGridViewItems(string mainMenu, string ChildMenu)
    {
        //var filteredmainMenus = mrGrid1.Rows.Cast<DataGridViewRow>()
        //    .Where(row => row.Cells[0].Value.ToString().Contains(mainMenu))
        //    .ToList();

        var filteredmainMenus = mrGrid1.Rows.Cast<DataGridViewRow>()
            .Where(row => row.Cells[0].Value != null &&
                          row.Cells[0].Value.ToString() == mainMenu)
            .ToList();

        if (filteredmainMenus.Count > 0)
        {
            int rowIndex = mrGrid1.Rows.IndexOf(filteredmainMenus.First());
            if (mrGrid1.Rows[rowIndex].Cells[0].Value.Equals(mainMenu))
            {
                bool isChecked = (bool)mrGrid1.Rows[rowIndex].Cells[2].Value;
                if (isChecked == false)
                {
                    mrGrid1.Rows[rowIndex].Cells[2].Value = true;
                    mrGrid1.Rows[rowIndex].Cells[3].Value = true;
                    mrGrid1.Rows[rowIndex].Cells[4].Value = true;
                    mrGrid1.Rows[rowIndex].Cells[5].Value = true;
                    mrGrid1.Rows[rowIndex].Cells[6].Value = true;
                    mrGrid1.Rows[rowIndex].Cells[7].Value = true;
                    //if (mainMenu != "HMnuCompanyManage" || mainMenu != "HMnuMaster" || mainMenu != "HMnuEntery")
                    //{
                    //    mrGrid1.Rows[rowIndex].Cells[8].Value = true;
                    //    mrGrid1.Rows[rowIndex].Cells[9].Value = true;
                    //    mrGrid1.Rows[rowIndex].Cells[10].Value = true;
                    //}
                    if (ChkCompanyInfo.Checked == true || ChkMaster.Checked == true || ChkDataEntry.Checked == true)
                    {
                        mrGrid1.Rows[rowIndex].Cells[8].Value = false;
                        mrGrid1.Rows[rowIndex].Cells[9].Value = false;
                        mrGrid1.Rows[rowIndex].Cells[10].Value = false;
                    }
                    if (ChkFinanceReport.Checked == true || ChkRegisterReport.Checked == true || ChkStockReports.Checked == true || ChkUtility.Checked == true || ChkAboutUs.Checked == true)
                    {
                        mrGrid1.Rows[rowIndex].Cells[2].Value = false;
                        mrGrid1.Rows[rowIndex].Cells[3].Value = false;
                        mrGrid1.Rows[rowIndex].Cells[4].Value = false;
                        mrGrid1.Rows[rowIndex].Cells[5].Value = false;
                        mrGrid1.Rows[rowIndex].Cells[6].Value = false;
                        mrGrid1.Rows[rowIndex].Cells[7].Value = false;
                        mrGrid1.Rows[rowIndex].Cells[8].Value = true;
                        mrGrid1.Rows[rowIndex].Cells[9].Value = true;
                        mrGrid1.Rows[rowIndex].Cells[10].Value = true;
                    }
                }
                else
                {
                    mrGrid1.Rows[rowIndex].Cells[2].Value = false;
                    mrGrid1.Rows[rowIndex].Cells[3].Value = false;
                    mrGrid1.Rows[rowIndex].Cells[4].Value = false;
                    mrGrid1.Rows[rowIndex].Cells[5].Value = false;
                    mrGrid1.Rows[rowIndex].Cells[6].Value = false;
                    mrGrid1.Rows[rowIndex].Cells[7].Value = false;
                    mrGrid1.Rows[rowIndex].Cells[8].Value = false;
                    mrGrid1.Rows[rowIndex].Cells[9].Value = false;
                    mrGrid1.Rows[rowIndex].Cells[10].Value = false;
                }
            }
        }

        //var filteredmainChildMenu = mrGrid1.Rows.Cast<DataGridViewRow>()
        //    .Where(row => row.Cells[0].Value.ToString().Contains(ChildMenu))
        //    .ToList();

        var filteredmainChildMenu = mrGrid1.Rows.Cast<DataGridViewRow>()
            .Where(row => row.Cells[0].Value != null &&
                          row.Cells[0].Value.ToString() == ChildMenu)
            .ToList();

        if (filteredmainChildMenu.Count > 0)
        {
            int rowIndex = mrGrid1.Rows.IndexOf(filteredmainChildMenu.First());

            if (mrGrid1.Rows[rowIndex].Cells[0].Value.Equals(ChildMenu))
            {
                bool isChecked = (bool)mrGrid1.Rows[rowIndex].Cells[2].Value;
                if (isChecked == false)
                {
                    mrGrid1.Rows[rowIndex].Cells[2].Value = true;
                    mrGrid1.Rows[rowIndex].Cells[3].Value = true;
                    mrGrid1.Rows[rowIndex].Cells[4].Value = true;
                    mrGrid1.Rows[rowIndex].Cells[5].Value = true;
                    mrGrid1.Rows[rowIndex].Cells[6].Value = true;
                    mrGrid1.Rows[rowIndex].Cells[7].Value = true;
                    if (ChkCompanyInfo.Checked == true || ChkMaster.Checked == true || ChkDataEntry.Checked == true)
                    {
                        mrGrid1.Rows[rowIndex].Cells[8].Value = false;
                        mrGrid1.Rows[rowIndex].Cells[9].Value = false;
                        mrGrid1.Rows[rowIndex].Cells[10].Value = false;
                    }
                    if (ChkFinanceReport.Checked == true || ChkRegisterReport.Checked == true || ChkStockReports.Checked == true || ChkUtility.Checked == true || ChkAboutUs.Checked == true)
                    {
                        mrGrid1.Rows[rowIndex].Cells[2].Value = false;
                        mrGrid1.Rows[rowIndex].Cells[3].Value = false;
                        mrGrid1.Rows[rowIndex].Cells[4].Value = false;
                        mrGrid1.Rows[rowIndex].Cells[5].Value = false;
                        mrGrid1.Rows[rowIndex].Cells[6].Value = false;
                        mrGrid1.Rows[rowIndex].Cells[7].Value = false;
                        mrGrid1.Rows[rowIndex].Cells[8].Value = true;
                        mrGrid1.Rows[rowIndex].Cells[9].Value = true;
                        mrGrid1.Rows[rowIndex].Cells[10].Value = true;
                    }
                }
                else
                {
                    mrGrid1.Rows[rowIndex].Cells[2].Value = false;
                    mrGrid1.Rows[rowIndex].Cells[3].Value = false;
                    mrGrid1.Rows[rowIndex].Cells[4].Value = false;
                    mrGrid1.Rows[rowIndex].Cells[5].Value = false;
                    mrGrid1.Rows[rowIndex].Cells[6].Value = false;
                    mrGrid1.Rows[rowIndex].Cells[7].Value = false;
                    mrGrid1.Rows[rowIndex].Cells[8].Value = false;
                    mrGrid1.Rows[rowIndex].Cells[9].Value = false;
                    mrGrid1.Rows[rowIndex].Cells[10].Value = false;
                }
            }
        }
    }
    private void Child_ItemClicked(object sender, EventArgs e)
    {
        try
        {
            var child = (ToolStripMenuItem)sender;
            var parent = (ToolStripMenuItem)child.OwnerItem;
            parent.Checked = true;
            parent.DropDown.AutoClose = false;
            _previousItem = parent;
            child.Checked = !child.Checked;
            if (child.Tag != null)
            {
                string ChildMenu = child.Tag.ToString();
                CheckedGridViewItems(ChildMenu);
            }
        }
        catch
        {
            // ignored
        }
    }
    public void CheckedGridViewItems(string ChildMenu)
    {
        //var filteredmainChildMenu = mrGrid1.Rows.Cast<DataGridViewRow>()
        //    .Where(row => row.Cells[0].Value.ToString().Contains(ChildMenu))
        //    .ToList();

        var filteredmainChildMenu = mrGrid1.Rows.Cast<DataGridViewRow>()
            .Where(row => row.Cells[0].Value != null &&
                          row.Cells[0].Value.ToString() == ChildMenu)
            .ToList();

        if (filteredmainChildMenu.Count > 0)
        {
            int rowIndex = mrGrid1.Rows.IndexOf(filteredmainChildMenu.First());
            if (mrGrid1.Rows[rowIndex].Cells[0].Value.Equals(ChildMenu))
            {
                bool isChecked = (bool)mrGrid1.Rows[rowIndex].Cells[2].Value;
                if (isChecked == false)
                {
                    mrGrid1.Rows[rowIndex].Cells[2].Value = true;
                }
                else
                {
                    mrGrid1.Rows[rowIndex].Cells[2].Value = false;
                }
            }

        }
    }
    private void FrmUserRight_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Return)
        {
            SendKeys.Send("{TAB}");
        }
        else if (e.KeyCode == Keys.Escape)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
    private void RemoveEvent(ToolStripItem ctl, string event_name)
    {
        try
        {
            FieldInfo field_info = typeof(ToolStripItem).GetField(event_name,
                BindingFlags.Static | BindingFlags.NonPublic);
            PropertyInfo property_info = ctl.GetType().GetProperty("Events",
                BindingFlags.NonPublic | BindingFlags.Instance);
            object obj = field_info.GetValue(ctl);
            EventHandlerList event_handlers =
                (EventHandlerList)property_info.GetValue(ctl, null);
            event_handlers.RemoveHandler(obj, event_handlers[obj]);
        }
        catch
        {
        }
    }
    private void ChkCompanyInfo_CheckedChanged(object sender, EventArgs e)
    {
        SelectMenu("HMnuCompanyManage", ChkCompanyInfo.Checked);
    }
    private void ChkMaster_CheckedChanged(object sender, EventArgs e)
    {
        SelectMenu("HMnuMaster", ChkMaster.Checked);
    }
    private void ChkDataEntry_CheckedChanged(object sender, EventArgs e)
    {
        SelectMenu("HMnuEntery", ChkDataEntry.Checked);
    }
    private void ChkFinanceReport_CheckedChanged(object sender, EventArgs e)
    {
        SelectMenu("HMnuFinanceReport", ChkFinanceReport.Checked);
    }
    private void ChkRegisterReport_CheckedChanged(object sender, EventArgs e)
    {
        SelectMenu("HMnuRegisterReport", ChkRegisterReport.Checked);
    }
    private void ChkStockReports_CheckedChanged(object sender, EventArgs e)
    {
        SelectMenu("HmnuStockReport", ChkStockReports.Checked);
    }
    private void ChkUtility_CheckedChanged(object sender, EventArgs e)
    {
        SelectMenu("MnuDataManage", ChkUtility.Checked);
    }
    private void ChkAboutUs_CheckedChanged(object sender, EventArgs e)
    {
        SelectMenu("MnuAboutUs", ChkAboutUs.Checked);
    }
    private void BtnSave_Click(object sender, EventArgs e)
    {
        #region Menu Item Update 
        ClosePreviousOpenedMenu();
        var xmlString = new StringBuilder();
        xmlString.Append("<accessMenuList>");
        MrMenu.Items.OfType<ToolStripMenuItem>().Where(i => i.DropDownItems.Count > 0).ForEach(x =>
        {
            xmlString.Append($@"<{x.Name}>");
            xmlString.Append($@"{x.Checked.ToString().ToLower()}");
            xmlString.Append($@"</{x.Name}>");
            x.DropDownItems.OfType<ToolStripMenuItem>().ForEach(di =>
            {
                xmlString.Append($@"<{di.Name}>");
                xmlString.Append($@"{di.Checked.ToString().ToLower()}");
                xmlString.Append($@"</{di.Name}>");
                if (di.DropDownItems.Count > 0)
                    di.DropDownItems.OfType<ToolStripMenuItem>().ForEach(child =>
                    {
                        xmlString.Append($@"<{child.Name}>");
                        xmlString.Append($@"{child.Checked.ToString().ToLower()}");
                        xmlString.Append($@"</{child.Name}>");
                    });
            });
        });
        xmlString.Append("</accessMenuList>");
        _xmlConfig = xmlString.ToString();
        #endregion

        #region Gridview event Item Update
        var formXmlString = new StringBuilder();
        formXmlString.Append("<UserAccessFormControls>");
        var formControlList = new List<UserAccessFormControl>();
        foreach (UserAccessFormControl f in bsForms)
        {
            if (f.FormName.Contains("&"))
                f.FormName = f.FormName.Replace("&&", "&amp;");
            formControlList.Add(f);
            formXmlString.Append("<UserAccessFormControl>");

            formXmlString.Append($@"<FormName>");
            formXmlString.Append($@"{f.FormId}");
            formXmlString.Append($@"</FormName>");

            formXmlString.Append($@"<NewButtonCheck>");
            formXmlString.Append($@"{f.NewButtonCheck}");
            formXmlString.Append($@"</NewButtonCheck>");

            formXmlString.Append($@"<SaveButtonCheck>");
            formXmlString.Append($@"{f.SaveButtonCheck}");
            formXmlString.Append($@"</SaveButtonCheck>");

            formXmlString.Append($@"<EditButtonCheck>");
            formXmlString.Append($@"{f.EditButtonCheck}");
            formXmlString.Append($@"</EditButtonCheck>");

            formXmlString.Append($@"<UpdateButtonCheck>");
            formXmlString.Append($@"{f.UpdateButtonCheck}");
            formXmlString.Append($@"</UpdateButtonCheck>");

            formXmlString.Append($@"<DeleteButtonCheck>");
            formXmlString.Append($@"{f.DeleteButtonCheck}");
            formXmlString.Append($@"</DeleteButtonCheck>");

            formXmlString.Append($@"<ViewButtonCheck>");
            formXmlString.Append($@"{f.ViewButtonCheck}");
            formXmlString.Append($@"</ViewButtonCheck>");

            formXmlString.Append($@"<SearchButtonCheck>");
            formXmlString.Append($@"{f.SearchButtonCheck}");
            formXmlString.Append($@"</SearchButtonCheck>");

            formXmlString.Append($@"<PrintButtonCheck>");
            formXmlString.Append($@"{f.PrintButtonCheck}");
            formXmlString.Append($@"</PrintButtonCheck>");

            formXmlString.Append($@"<ExportButtonCheck>");
            formXmlString.Append($@"{f.ExportButtonCheck}");
            formXmlString.Append($@"</ExportButtonCheck>");
            formXmlString.Append("</UserAccessFormControl>");
        }
        formXmlString.Append("</UserAccessFormControls>");
        //_formsXmlConfig = XmlUtils.SerializeToXml(bsForms.Cast<UserAccessFormControl>().ToList());
        _formsXmlConfig = formXmlString.ToString();
        #endregion

        var result = SaveSecurityRight();
        if (result > 0)
        {
            this.NotifySuccess("Configuration Saved Successfully");
        }
        else
        {
            this.NotifyError("Failed to save configuration");
        }
        ObjGlobal.BindUserRoleAsync(CmbUserRole);
        BindUsers();
        EmptyGridControls();
    }
    private void mrGrid1_Click(object sender, EventArgs e)
    {
        ClosePreviousOpenedMenu();
    }
    private void CmbUserRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        CmbUser.SelectedIndexChanged -= CmbUser_SelectedIndexChanged;
        BindUsers();
        CmbUser.SelectedIndexChanged += CmbUser_SelectedIndexChanged;
        SelectMenu();
    }
    private void FrmUserRight_FormClosing(object sender, FormClosingEventArgs e)
    {
        ClosePreviousOpenedMenu();
    }
    private void panel1_Click(object sender, EventArgs e)
    {
        ClosePreviousOpenedMenu();
    }
    private void CmbUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        SelectMenus();
    }
    private void mrGrid1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
    {
        if (e.ColumnIndex == 0) return;
        switch (e.ColumnIndex)
        {
            case 2:
                foreach (UserAccessFormControl f in bsForms)
                {
                    f.NewButtonCheck = !f.NewButtonCheck;
                }
                break;
            case 3:
                foreach (UserAccessFormControl f in bsForms)
                {
                    f.SaveButtonCheck = !f.SaveButtonCheck;
                }
                break;
            case 4:
                foreach (UserAccessFormControl f in bsForms)
                {
                    f.EditButtonCheck = !f.EditButtonCheck;
                }
                break;
            case 5:
                foreach (UserAccessFormControl f in bsForms)
                {
                    f.UpdateButtonCheck = !f.UpdateButtonCheck;
                }
                break;
            case 6:
                foreach (UserAccessFormControl f in bsForms)
                {
                    f.DeleteButtonCheck = !f.DeleteButtonCheck;
                }
                break;

            case 7:
                foreach (UserAccessFormControl f in bsForms)
                {
                    f.ViewButtonCheck = !f.ViewButtonCheck;
                }
                break;
            case 8:
                foreach (UserAccessFormControl f in bsForms)
                {
                    f.SearchButtonCheck = !f.SearchButtonCheck;
                }
                break;
            case 9:
                foreach (UserAccessFormControl f in bsForms)
                {
                    f.PrintButtonCheck = !f.PrintButtonCheck;
                }
                break;
            case 10:
                foreach (UserAccessFormControl f in bsForms)
                {
                    f.ExportButtonCheck = !f.ExportButtonCheck;
                }
                break;
        }

        mrGrid1.Refresh();
    }
    #endregion
    // METHOD FOR THIS FORM
    #region --------------- METHOD ---------------
    [Obsolete]
    public void CloneMyMenu()
    {
        MrMenu.Items.Clear();
        var menu = new MdiMrSolution().HearderMenuList.Items.OfType<ToolStripMenuItem>();
        MrMenu.Items.AddRange(menu.ToArray<ToolStripItem>());
    }
    public void BindUsers()
    {
        var roleId = (int)CmbUserRole.SelectedValue;
        string cmdString = $@"
            SELECT 0 User_Id, '' User_Name FROM AMS.UserInfo
            UNION
            SELECT User_Id, User_Name FROM AMS.UserInfo 
            WHERE Role_Id={roleId} AND USER_NAME NOT IN ('ADMIN', 'AMSADMIN','MRDEMO','MRSOLUTION')";
        var dt1 = SqlExtensions.ExecuteDataSetOnMaster(cmdString).Tables[0];
        if (dt1.Rows.Count <= 0) return;
        CmbUser.DataSource = dt1;
        CmbUser.DisplayMember = "User_Name";
        CmbUser.ValueMember = "User_Id";
    }
    private int SaveSecurityRight()
    {
        _control.AccessControl.UserRoleId = (int)CmbUserRole.SelectedValue;
        _control.AccessControl.UserId = CmbUser.SelectedValue == null ? 0 : (int)CmbUser.SelectedValue;
        _control.AccessControl.FeatureAlias = 1;
        _control.AccessControl.BranchId = ObjGlobal.SysBranchId;
        _control.AccessControl.IsValid = true;
        _control.AccessControl.CreatedOn = DateTime.Now;
        _control.AccessControl.ConfigXml = _xmlConfig;
        _control.AccessControl.ConfigFormsXml = _formsXmlConfig;
        return _control.SaveSecurityRights(@"SAVE");
        return 0;
    }
    private System.Data.DataTable LoadSecurityRights()
    {
        var userId = CmbUser.SelectedValue == null ? 0 : (int)CmbUser.SelectedValue;
        return _objMaster.GetUserAccessControl((int)CmbUserRole.SelectedValue, userId, true);
    }
    private System.Data.DataTable LoadSecurityRight()
    {
        var userId = CmbUser.SelectedValue == null ? 0 : (int)CmbUser.SelectedValue;
        return _objMaster.GetUserAccessControls((int)CmbUserRole.SelectedValue, userId, true);
    }
    private void ClosePreviousOpenedMenu()
    {
        if (_previousParentItem != null)
        {
            _previousParentItem.DropDown.Close();
            _previousParentItem.DropDownItems.OfType<ToolStripMenuItem>().ForEach(x =>
            {
                x.DropDown.Close();
            });
        }
    }
    private void SelectMenu()
    {
        var alreadySavedData = LoadSecurityRights();
        if (alreadySavedData.Rows.Count > 0)
        {
            //menu control xml
            var xmlData = alreadySavedData.Rows[0].ItemArray[8].ToString();
            if (!string.IsNullOrEmpty(xmlData))
            {
                var doc = new XmlDocument();
                doc.LoadXml(xmlData);
                MemoryStream xmlStream = new MemoryStream();
                doc.Save(xmlStream);
                xmlStream.Flush();//Adjust this if you want read your data
                xmlStream.Position = 0;
                XmlReader rd = XmlReader.Create(xmlStream);
                while (rd.Read())
                {
                    string menuName = rd.Name;
                    if (rd.NodeType == XmlNodeType.Element && menuName != "accessMenuList")
                    {
                        //Console.WriteLine(rd.ReadElementContentAsString());
                        var value = rd.ReadElementContentAsBoolean();
                        var menu = MrMenu.Items.OfType<ToolStripMenuItem>().FirstOrDefault(x => x.Name == menuName);
                        if (menu == null)
                        {
                            var items = MrMenu.Items.OfType<ToolStripMenuItem>();
                            var isExist = true;
                            foreach (var it in items)
                            {
                                var subMenu = it.DropDownItems.OfType<ToolStripMenuItem>().FirstOrDefault(x => x.Name == menuName);
                                if (subMenu != null)
                                {
                                    subMenu.Checked = value;
                                    isExist = true;
                                    break;
                                }
                                isExist = false;
                            }
                            if (!isExist)
                            {
                                foreach (var ch in items)
                                {
                                    var child = ch.DropDownItems.OfType<ToolStripMenuItem>();
                                    foreach (var chi in child)
                                    {
                                        var subMenu = chi.DropDownItems.OfType<ToolStripMenuItem>().FirstOrDefault(x => x.Name == menuName);
                                        if (subMenu != null)
                                        {
                                            subMenu.Checked = value;
                                            isExist = true;
                                            break;
                                        }
                                        isExist = false;
                                    }
                                }
                            }
                        }
                        else
                        {
                            //menu.Enabled = value;
                        }
                    }
                    else
                    {
                        rd.Read();
                    }
                }
            }
            else
            {
                BindEmptyMenu();
            }
            //form control xml
            var formXmlData = alreadySavedData.Rows[0].ItemArray[9].ToString();
            if (!string.IsNullOrEmpty(formXmlData))
            {
                var formXml = XmlUtils.XmlDeserialize<List<UserAccessFormControl>>(formXmlData);
                bsForms.DataSource = formXml;
            }
            else
            {
                BindEmptyFormControls();
            }
        }
        else
        {
            BindEmptyUserAccessControlData();
        }
        mrGrid1.DataSource = bsForms;
    }
    private void SelectMenus()
    {
        var alreadySavedData = LoadSecurityRight();
        if (alreadySavedData.Rows.Count > 0)
        {
            var xmlData = alreadySavedData.Rows[0].ItemArray[8].ToString();
            var xmlDataEvent = alreadySavedData.Rows[0].ItemArray[10].ToString();
            if (!string.IsNullOrEmpty(xmlData))
            {
                var doc = new XmlDocument();
                doc.LoadXml(xmlData);
                XmlNodeList accessMenuList = doc.GetElementsByTagName("accessMenuList");
                if (!string.IsNullOrEmpty(xmlDataEvent))
                {
                    var docEvent = new XmlDocument();
                    docEvent.LoadXml(xmlDataEvent);
                    XmlNodeList accessGridEventList = docEvent.GetElementsByTagName("UserAccessFormControls");
                    foreach (XmlNode accessGridEventNode in accessGridEventList)
                    {
                        XmlNodeList formControlNodes = accessGridEventNode.ChildNodes;

                        foreach (XmlNode formControlNode in formControlNodes)
                        {
                            string formName = "";
                            bool newButtonCheck = false;
                            bool saveButtonCheck = false;
                            bool editButtonCheck = false;
                            bool updateButtonCheck = false;
                            bool deleteButtonCheck = false;
                            bool viewButtonCheck = false;
                            bool searchButtonCheck = false;
                            bool printButtonCheck = false;
                            bool exportButtonCheck = false;
                            foreach (XmlNode formControlNodelst in formControlNode)
                            {
                                string nodeName = formControlNodelst.Name;
                                string nodeValue = formControlNodelst.InnerText;
                                switch (nodeName)
                                {
                                    case "FormName":
                                        formName = nodeValue;
                                        break;

                                    case "NewButtonCheck":
                                        newButtonCheck = Convert.ToBoolean(nodeValue);
                                        break;

                                    case "SaveButtonCheck":
                                        saveButtonCheck = Convert.ToBoolean(nodeValue);
                                        break;

                                    case "EditButtonCheck":
                                        editButtonCheck = Convert.ToBoolean(nodeValue);
                                        break;

                                    case "UpdateButtonCheck":
                                        updateButtonCheck = Convert.ToBoolean(nodeValue);
                                        break;

                                    case "DeleteButtonCheck":
                                        deleteButtonCheck = Convert.ToBoolean(nodeValue);
                                        break;

                                    case "ViewButtonCheck":
                                        viewButtonCheck = Convert.ToBoolean(nodeValue);
                                        break;

                                    case "SearchButtonCheck":
                                        searchButtonCheck = Convert.ToBoolean(nodeValue);
                                        break;

                                    case "PrintButtonCheck":
                                        printButtonCheck = Convert.ToBoolean(nodeValue);
                                        break;

                                    case "ExportButtonCheck":
                                        exportButtonCheck = Convert.ToBoolean(nodeValue);
                                        break;
                                }
                            }
                            CheckedGridViewItemsEvent(formName, newButtonCheck, saveButtonCheck, editButtonCheck, updateButtonCheck, deleteButtonCheck,
                                viewButtonCheck, searchButtonCheck, printButtonCheck, exportButtonCheck);
                        }
                    }
                }
                foreach (XmlNode node in accessMenuList)
                {
                    if (node.NodeType == XmlNodeType.Element)
                    {
                        foreach (XmlNode childNode in node.ChildNodes)
                        {
                            if (childNode.NodeType == XmlNodeType.Element)
                            {
                                string mainMenu = childNode.Name;
                                string ChildMenu = childNode.Name;
                                bool Checked = bool.Parse(childNode.InnerText);
                                SelectMenus(ChildMenu, Checked);
                            }
                        }
                    }
                }
            }
        }
        else
        {
            EmptyGridControls();
            //this.NotifySuccess("User does not have any permissions.");
        }
    }
    public void CheckedGridViewItemsEvent(string formName, bool newButtonCheck, bool saveButtonCheck, bool editButtonCheck, bool updateButtonCheck, bool deleteButtonCheck,
        bool viewButtonCheck, bool searchButtonCheck, bool printButtonCheck, bool exportButtonCheck)
    {
        var filteredmainChildMenu = mrGrid1.Rows.Cast<DataGridViewRow>()
            .Where(row => row.Cells[0].Value != null &&
                          row.Cells[0].Value.ToString() == formName)
            .ToList();

        if (filteredmainChildMenu.Count > 0)
        {
            int rowIndex = mrGrid1.Rows.IndexOf(filteredmainChildMenu.First());
            if (mrGrid1.Rows[rowIndex].Cells[0].Value.Equals(formName))
            {
                mrGrid1.Rows[rowIndex].Cells[2].Value = newButtonCheck;
                mrGrid1.Rows[rowIndex].Cells[3].Value = saveButtonCheck;
                mrGrid1.Rows[rowIndex].Cells[4].Value = editButtonCheck;
                mrGrid1.Rows[rowIndex].Cells[5].Value = updateButtonCheck;
                mrGrid1.Rows[rowIndex].Cells[6].Value = deleteButtonCheck;
                mrGrid1.Rows[rowIndex].Cells[7].Value = viewButtonCheck;
                mrGrid1.Rows[rowIndex].Cells[8].Value = searchButtonCheck;
                mrGrid1.Rows[rowIndex].Cells[9].Value = printButtonCheck;
                mrGrid1.Rows[rowIndex].Cells[10].Value = exportButtonCheck;
            }
        }
    }
    public void CheckedGridViewItems(string mainMenu, string ChildMenu, bool Checked)
    {
        //var filteredmainMenus = mrGrid1.Rows.Cast<DataGridViewRow>()
        //    .Where(row => row.Cells[0].Value.ToString().Contains(mainMenu))
        //    .ToList();

        var filteredmainMenus = mrGrid1.Rows.Cast<DataGridViewRow>()
            .Where(row => row.Cells[0].Value != null &&
                          row.Cells[0].Value.ToString() == mainMenu)
            .ToList();

        if (filteredmainMenus.Count > 0)
        {
            int rowIndex = mrGrid1.Rows.IndexOf(filteredmainMenus.First());
            if (mrGrid1.Rows[rowIndex].Cells[0].Value.Equals(mainMenu))
            {
                if (Checked == true)
                {
                    mrGrid1.Rows[rowIndex].Cells[2].Value = true;
                    mrGrid1.Rows[rowIndex].Cells[3].Value = true;
                    mrGrid1.Rows[rowIndex].Cells[4].Value = true;
                    mrGrid1.Rows[rowIndex].Cells[5].Value = true;
                    mrGrid1.Rows[rowIndex].Cells[6].Value = true;
                    mrGrid1.Rows[rowIndex].Cells[7].Value = true;
                    if (ChkCompanyInfo.Checked == true || ChkMaster.Checked == true || ChkDataEntry.Checked == true)
                    {
                        mrGrid1.Rows[rowIndex].Cells[8].Value = false;
                        mrGrid1.Rows[rowIndex].Cells[9].Value = false;
                        mrGrid1.Rows[rowIndex].Cells[10].Value = false;
                    }
                    if (ChkFinanceReport.Checked == true || ChkRegisterReport.Checked == true || ChkStockReports.Checked == true || ChkUtility.Checked == true || ChkAboutUs.Checked == true)
                    {
                        mrGrid1.Rows[rowIndex].Cells[2].Value = false;
                        mrGrid1.Rows[rowIndex].Cells[3].Value = false;
                        mrGrid1.Rows[rowIndex].Cells[4].Value = false;
                        mrGrid1.Rows[rowIndex].Cells[5].Value = false;
                        mrGrid1.Rows[rowIndex].Cells[6].Value = false;
                        mrGrid1.Rows[rowIndex].Cells[7].Value = false;
                        mrGrid1.Rows[rowIndex].Cells[8].Value = true;
                        mrGrid1.Rows[rowIndex].Cells[9].Value = true;
                        mrGrid1.Rows[rowIndex].Cells[10].Value = true;
                    }
                }
                else
                {
                    mrGrid1.Rows[rowIndex].Cells[2].Value = false;
                    mrGrid1.Rows[rowIndex].Cells[3].Value = false;
                    mrGrid1.Rows[rowIndex].Cells[4].Value = false;
                    mrGrid1.Rows[rowIndex].Cells[5].Value = false;
                    mrGrid1.Rows[rowIndex].Cells[6].Value = false;
                    mrGrid1.Rows[rowIndex].Cells[7].Value = false;
                    mrGrid1.Rows[rowIndex].Cells[8].Value = false;
                    mrGrid1.Rows[rowIndex].Cells[9].Value = false;
                    mrGrid1.Rows[rowIndex].Cells[10].Value = false;
                }
            }
        }

        //var filteredmainChildMenu = mrGrid1.Rows.Cast<DataGridViewRow>()
        //    .Where(row => row.Cells[0].Value.ToString().Contains(ChildMenu))
        //    .ToList();

        var filteredmainChildMenu = mrGrid1.Rows.Cast<DataGridViewRow>()
            .Where(row => row.Cells[0].Value != null &&
                          row.Cells[0].Value.ToString() == ChildMenu)
            .ToList();

        if (filteredmainChildMenu.Count > 0)
        {
            int rowIndex = mrGrid1.Rows.IndexOf(filteredmainChildMenu.First());

            if (mrGrid1.Rows[rowIndex].Cells[0].Value.Equals(ChildMenu))
            {
                if (Checked == true)
                {
                    mrGrid1.Rows[rowIndex].Cells[2].Value = true;
                    mrGrid1.Rows[rowIndex].Cells[3].Value = true;
                    mrGrid1.Rows[rowIndex].Cells[4].Value = true;
                    mrGrid1.Rows[rowIndex].Cells[5].Value = true;
                    mrGrid1.Rows[rowIndex].Cells[6].Value = true;
                    mrGrid1.Rows[rowIndex].Cells[7].Value = true;
                    if (ChkCompanyInfo.Checked == true || ChkMaster.Checked == true || ChkDataEntry.Checked == true)
                    {
                        mrGrid1.Rows[rowIndex].Cells[8].Value = false;
                        mrGrid1.Rows[rowIndex].Cells[9].Value = false;
                        mrGrid1.Rows[rowIndex].Cells[10].Value = false;
                    }
                    if (ChkFinanceReport.Checked == true || ChkRegisterReport.Checked == true || ChkStockReports.Checked == true || ChkUtility.Checked == true || ChkAboutUs.Checked == true)
                    {
                        mrGrid1.Rows[rowIndex].Cells[2].Value = false;
                        mrGrid1.Rows[rowIndex].Cells[3].Value = false;
                        mrGrid1.Rows[rowIndex].Cells[4].Value = false;
                        mrGrid1.Rows[rowIndex].Cells[5].Value = false;
                        mrGrid1.Rows[rowIndex].Cells[6].Value = false;
                        mrGrid1.Rows[rowIndex].Cells[7].Value = false;
                        mrGrid1.Rows[rowIndex].Cells[8].Value = true;
                        mrGrid1.Rows[rowIndex].Cells[9].Value = true;
                        mrGrid1.Rows[rowIndex].Cells[10].Value = true;
                    }
                }
                else
                {
                    mrGrid1.Rows[rowIndex].Cells[2].Value = false;
                    mrGrid1.Rows[rowIndex].Cells[3].Value = false;
                    mrGrid1.Rows[rowIndex].Cells[4].Value = false;
                    mrGrid1.Rows[rowIndex].Cells[5].Value = false;
                    mrGrid1.Rows[rowIndex].Cells[6].Value = false;
                    mrGrid1.Rows[rowIndex].Cells[7].Value = false;
                    mrGrid1.Rows[rowIndex].Cells[8].Value = false;
                    mrGrid1.Rows[rowIndex].Cells[9].Value = false;
                    mrGrid1.Rows[rowIndex].Cells[10].Value = false;
                }
            }
        }
    }
    private void SelectMenus(string menuName, bool checkStatus)
    {
        if (menuName == "HMnuCompanyManage" || menuName == "HMnuMaster" ||
            menuName == "HMnuEntery" || menuName == "HMnuFinanceReport")
        {
            MainItems = MrMenu.Items.OfType<ToolStripMenuItem>().FirstOrDefault(x => x.Name == menuName);
        }

        if (MainItems != null)
        {
            SubMenuItems = MainItems.DropDownItems.OfType<ToolStripMenuItem>()
                .FirstOrDefault(y => y.Name == menuName);
            if (SubMenuItems != null)
            {
                var filteredmainMenus = mrGrid1.Rows.Cast<DataGridViewRow>()
                    .Where(row => row.Cells[0].Value != null &&
                                  row.Cells[0].Value.ToString() == SubMenuItems.Name)
                    .ToList();
                if (filteredmainMenus.Count > 0)
                {
                    SubMenuItems.Checked = checkStatus;
                }
                SubMenuPriviewItems = SubMenuItems;
            }

            if (SubMenuPriviewItems != null)
            {
                ChildItems = SubMenuPriviewItems.DropDownItems.OfType<ToolStripMenuItem>()
                    .FirstOrDefault(z => z.Name == menuName);
                if (ChildItems != null)
                {
                    var filteredmainMenus = mrGrid1.Rows.Cast<DataGridViewRow>()
                        .Where(row => row.Cells[0].Value != null &&
                                      row.Cells[0].Value.ToString() == ChildItems.Name)
                        .ToList();

                    if (filteredmainMenus.Count > 0)
                    {
                        ChildItems.Checked = checkStatus;
                    }
                }
            }
        }
    }
    private void BindEmptyUserAccessControlData()
    {
        BindEmptyMenu();
        BindEmptyFormControls();
    }
    private void BindEmptyMenu()
    {
        MrMenu.Items.OfType<ToolStripMenuItem>().ForEach(menu =>
        {
            menu.DropDownItems.OfType<ToolStripMenuItem>().ForEach(x =>
            {
                x.Checked = false;
                x.DropDownItems.OfType<ToolStripMenuItem>().ForEach(di =>
                {
                    di.Checked = false;
                });
            });
        });
    }
    private void BindEmptyFormControls()
    {
        bsForms.DataSource = _forms.OrderBy(x => x.FormName);
    }
    private void SelectMenu(string menuName, bool checkStatus)
    {
        var menu = MrMenu.Items.OfType<ToolStripMenuItem>().FirstOrDefault(x => x.Name == menuName);
        if (menu == null) return;
        {
            menu.DropDownItems.OfType<ToolStripMenuItem>().ForEach(x =>
            {
                var filteredmainMenus = mrGrid1.Rows.Cast<DataGridViewRow>()
                    .Where(row => row.Cells[0].Value != null &&
                                  row.Cells[0].Value.ToString() == x.Name)
                    .ToList();

                if (filteredmainMenus.Count > 0)
                {
                    x.Checked = checkStatus;
                }
                x.DropDownItems.OfType<ToolStripMenuItem>().ForEach(di =>
                {
                    var filteredmainChildMenu = mrGrid1.Rows.Cast<DataGridViewRow>()
                        .Where(row => row.Cells[0].Value != null &&
                                      row.Cells[0].Value.ToString() == di.Name)
                        .ToList();
                    if (filteredmainChildMenu.Count > 0)
                    {
                        di.Checked = checkStatus;
                    }
                    string mainMenu = x.Name;
                    string ChildMenu = di.Name;
                    CheckedGridViewItems(mainMenu, ChildMenu);
                });
            });
        }
    }
    private void GetForms(ToolStripItem ctl)
    {
        if (ctl?.Tag == null || string.IsNullOrEmpty(ctl.Tag.ToString())) return;

        var form = new UserAccessFormControl
        {
            FormId = ctl.Name.ToString(),
            FormName = ctl.Text.ToString(),
            NewButtonCheck = false,
            SaveButtonCheck = false,
            EditButtonCheck = false,
            UpdateButtonCheck = false,
            DeleteButtonCheck = false,
            ViewButtonCheck = false,
            SearchButtonCheck = false,
            PrintButtonCheck = false,
            ExportButtonCheck = false,
        };
        _forms.Add(form);
        //mrGrid1.Rows.Add(form);
    }
    #endregion
    //OBJECT FOR THIS FORM
    #region --------------- OBJECT FOR THIS ---------------
    private ToolStripMenuItem _previousParentItem = null;
    private string _xmlConfig = string.Empty, _formsXmlConfig = string.Empty;
    private readonly IMasterSetup _objMaster;
    private readonly IUserAccessControl _control;
    private List<UserAccessFormControl> _forms;
    private void mrGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
        int row = e.RowIndex;
        int col = e.ColumnIndex;
        if (row != -1)
        {
            bool isCheck1 = (bool)mrGrid1.Rows[e.RowIndex].Cells[2].Value;
            bool isCheck2 = (bool)mrGrid1.Rows[e.RowIndex].Cells[3].Value;
            bool isCheck3 = (bool)mrGrid1.Rows[e.RowIndex].Cells[4].Value;
            bool isCheck4 = (bool)mrGrid1.Rows[e.RowIndex].Cells[5].Value;
            bool isCheck5 = (bool)mrGrid1.Rows[e.RowIndex].Cells[6].Value;
            bool isCheck6 = (bool)mrGrid1.Rows[e.RowIndex].Cells[7].Value;
            bool isCheck7 = (bool)mrGrid1.Rows[e.RowIndex].Cells[8].Value;
            bool isCheck8 = (bool)mrGrid1.Rows[e.RowIndex].Cells[9].Value;
            bool isCheck9 = (bool)mrGrid1.Rows[e.RowIndex].Cells[10].Value;
            if (isCheck1 || isCheck2 || isCheck3 || isCheck4 || isCheck5 || isCheck6 || isCheck7 ||
                isCheck8 || isCheck9)
            {
                isCheck1 = true;
            }
            else
            {
                isCheck1 = false;
            }

            string particularData = mrGrid1.Rows[e.RowIndex].Cells[0].Value.ToString();

            MrMenu.Items.OfType<ToolStripMenuItem>().Where(i => i.DropDownItems.Count > 0).ForEach(x =>
            {
                if (x.Text == particularData)
                {
                    x.Checked = true;
                }
                x.DropDownItems.OfType<ToolStripMenuItem>().ForEach(di =>
                {
                    if (di.Text == particularData)
                    {
                        di.Checked = true;
                    }

                    if (di.DropDownItems.Count > 0)
                    {
                        di.DropDownItems.OfType<ToolStripMenuItem>().ForEach(child =>
                        {
                            if (child.Text == particularData)
                            {
                                child.Checked = true;
                            }
                        });
                    }
                });
            });
        }
    }
    private void EmptyGridControls()
    {
        for (int i = 0; i < mrGrid1.Rows.Count; i++)
        {
            mrGrid1.Rows[i].Cells[2].Value = false;
            mrGrid1.Rows[i].Cells[3].Value = false;
            mrGrid1.Rows[i].Cells[4].Value = false;
            mrGrid1.Rows[i].Cells[5].Value = false;
            mrGrid1.Rows[i].Cells[6].Value = false;
            mrGrid1.Rows[i].Cells[7].Value = false;
            mrGrid1.Rows[i].Cells[8].Value = false;
            mrGrid1.Rows[i].Cells[9].Value = false;
            mrGrid1.Rows[i].Cells[10].Value = false;
        }
        ChkCompanyInfo.Checked = false;
        ChkMaster.Checked = false;
        ChkDataEntry.Checked = false;
        ChkFinanceReport.Checked = false;
        ChkRegisterReport.Checked = false;
        ChkStockReports.Checked = false;
        ChkUtility.Checked = false;
        ChkAboutUs.Checked = false;
    }
    private ToolStripMenuItem _previousItem = null;
    private ToolStripMenuItem MainItems;
    private ToolStripMenuItem SubMenuItems;
    private ToolStripMenuItem SubMenuPriviewItems;
    private ToolStripMenuItem ChildItems;
    #endregion
}