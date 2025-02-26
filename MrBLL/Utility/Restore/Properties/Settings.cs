using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace MrBLL.Utility.Restore.Properties;

[CompilerGenerated]
[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
internal sealed class Settings : ApplicationSettingsBase
{
    static Settings()
    {
        Default = (Settings)Synchronized(new Settings());
    }

    [DebuggerNonUserCode]
    [DefaultSettingValue("")]
    [UserScopedSetting]
    public string BakFile
    {
        get => (string)this["BakFile"];
        set => this["BakFile"] = value;
    }

    [DebuggerNonUserCode]
    [DefaultSettingValue("")]
    [UserScopedSetting]
    public string Database
    {
        get => (string)this["Database"];
        set => this["Database"] = value;
    }

    public static Settings Default { get; }

    [DebuggerNonUserCode]
    [DefaultSettingValue("False")]
    [UserScopedSetting]
    public bool DontAssoc
    {
        get => (bool)this["DontAssoc"];
        set => this["DontAssoc"] = value;
    }

    [DebuggerNonUserCode]
    [DefaultSettingValue("True")]
    [UserScopedSetting]
    public bool IntergatedSecurity
    {
        get => (bool)this["IntergatedSecurity"];
        set => this["IntergatedSecurity"] = value;
    }

    [DebuggerNonUserCode]
    [DefaultSettingValue("")]
    [UserScopedSetting]
    public string SqlPassword
    {
        get => (string)this["SqlPassword"];
        set => this["SqlPassword"] = value;
    }

    [DebuggerNonUserCode]
    [DefaultSettingValue("")]
    [UserScopedSetting]
    public string SqlServer
    {
        get => (string)this["SqlServer"];
        set => this["SqlServer"] = value;
    }

    [DebuggerNonUserCode]
    [DefaultSettingValue("")]
    [UserScopedSetting]
    public string SqlUser
    {
        get => (string)this["SqlUser"];
        set => this["SqlUser"] = value;
    }
}