﻿using DevExpress.XtraWaitForm;
using System;

namespace MrDAL.Control.SplashScreen;

public partial class PleaseWait : WaitForm
{
    public enum WaitFormCommand
    {
    }

    public PleaseWait()
    {
        InitializeComponent();
        progressPanel1.Caption = @"PLEASE WAIT JUST A MINUTE";
        progressPanel1.AutoHeight = true;
    }

    #region Overrides

    public override void SetCaption(string caption)
    {
        base.SetCaption(caption);
        progressPanel1.Caption = caption;
    }

    public override void SetDescription(string description)
    {
        base.SetDescription(description);
        progressPanel1.Description = description;
    }

    public override void ProcessCommand(Enum cmd, object arg)
    {
        base.ProcessCommand(cmd, arg);
    }

    #endregion Overrides
}