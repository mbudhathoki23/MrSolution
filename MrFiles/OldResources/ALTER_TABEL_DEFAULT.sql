ALTER TABLE [AMS].[Area]
ADD CONSTRAINT [DF_Area_Country]
    DEFAULT (N'Nepal') FOR [Country];
ALTER TABLE [AMS].[Area]
ADD CONSTRAINT [DF_Area_Status]
    DEFAULT ((1)) FOR [Status];
ALTER TABLE [AMS].[Area]
ADD CONSTRAINT [DF_Area_EnterBy]
    DEFAULT (N'MrSolution') FOR [EnterBy];
ALTER TABLE [AMS].[BillOfMaterial_Master]
ADD CONSTRAINT [DF_BillOfMaterial_Master_Alt_Qty]
    DEFAULT ((0)) FOR [AltQty];
ALTER TABLE [AMS].[BillOfMaterial_Master]
ADD CONSTRAINT [DF_BillOfMaterial_Master_Qty]
    DEFAULT ((0)) FOR [Qty];
ALTER TABLE [AMS].[CostCenter]
ADD CONSTRAINT [DF_CostCenter_Status]
    DEFAULT ((1)) FOR [Status];
ALTER TABLE [AMS].[CostCenter]
ADD CONSTRAINT [DF_CostCenter_EnterBy]
    DEFAULT (N'MrSolution') FOR [EnterBy];
ALTER TABLE [AMS].[Counter]
ADD CONSTRAINT [DF_Counter_Status]
    DEFAULT ((1)) FOR [Status];
ALTER TABLE [AMS].[Department]
ADD CONSTRAINT [DF_Department_Dlevel]
    DEFAULT (N'I') FOR [Dlevel];
ALTER TABLE [AMS].[Department]
ADD CONSTRAINT [DF_Department_Status]
    DEFAULT ((1)) FOR [Status];
ALTER TABLE [AMS].[Department]
ADD CONSTRAINT [DF_Department_EnterBy]
    DEFAULT (N'MrSolution') FOR [EnterBy];
ALTER TABLE [AMS].[DocumentNumbering]
ADD CONSTRAINT [DF_DocumentNumbering_DocBlank]
    DEFAULT ((1)) FOR [DocBlank];
ALTER TABLE [AMS].[DocumentNumbering]
ADD CONSTRAINT [DF_DocumentNumbering_DocBlankCh]
    DEFAULT ((0)) FOR [DocBlankCh];
ALTER TABLE [AMS].[DocumentNumbering]
ADD CONSTRAINT [DF_DocumentNumbering_Status]
    DEFAULT ((1)) FOR [Status];
ALTER TABLE [AMS].[DocumentNumbering]
ADD CONSTRAINT [DF_DocumentNumbering_EnterBy]
    DEFAULT (N'MrSolution') FOR [EnterBy];
ALTER TABLE [AMS].[Floor]
ADD CONSTRAINT [DF_Floor_Type]
    DEFAULT ('1St') FOR [Type];
ALTER TABLE [AMS].[Floor]
ADD CONSTRAINT [DF_Floor_EnterBy]
    DEFAULT (N'MrSolution') FOR [EnterBy];
ALTER TABLE [AMS].[Floor]
ADD CONSTRAINT [DF_Floor_Status]
    DEFAULT ((1)) FOR [Status];
ALTER TABLE [AMS].[JuniorAgent]
ADD CONSTRAINT [DF_JuniorAgent_Commission]
    DEFAULT ((0.00)) FOR [Commission];
ALTER TABLE [AMS].[JuniorAgent]
ADD CONSTRAINT [DF_JuniorAgent_CRLimit]
    DEFAULT ((0.00)) FOR [CRLimit];
ALTER TABLE [AMS].[JuniorAgent]
ADD CONSTRAINT [DF_JuniorAgent_CrDays]
    DEFAULT ((0)) FOR [CrDays];
ALTER TABLE [AMS].[JuniorAgent]
ADD CONSTRAINT [DF_JuniorAgent_CrType]
    DEFAULT (N'Ignore') FOR [CrType];
ALTER TABLE [AMS].[JuniorAgent]
ADD CONSTRAINT [DF_JuniorAgent_Status]
    DEFAULT ((1)) FOR [Status];
ALTER TABLE [AMS].[JuniorAgent]
ADD CONSTRAINT [DF_JuniorAgent_EnterBy]
    DEFAULT (N'MrSolution') FOR [EnterBy];
ALTER TABLE [AMS].[MainArea]
ADD CONSTRAINT [DF_MainArea_MCountry]
    DEFAULT (N'Nepal') FOR [MCountry];
ALTER TABLE [AMS].[MainArea]
ADD CONSTRAINT [DF_MainArea_Status]
    DEFAULT ((1)) FOR [Status];
ALTER TABLE [AMS].[MainArea]
ADD CONSTRAINT [DF_MainArea_EnterBy]
    DEFAULT (N'MrSolution') FOR [EnterBy];
ALTER TABLE [AMS].[MemberType]
ADD CONSTRAINT [DF_MemberType_Discount]
    DEFAULT ((0)) FOR [Discount];
ALTER TABLE [AMS].[MemberType]
ADD CONSTRAINT [DF_MemberType_ActiveStatus]
    DEFAULT ((1)) FOR [ActiveStatus];
ALTER TABLE [AMS].[MemberType]
ADD CONSTRAINT [DF_MemberType_EnterBy]
    DEFAULT (N'MrSolution') FOR [EnterBy];
ALTER TABLE [AMS].[Notes_Master]
ADD CONSTRAINT [DF_Notes_Master_Currency_Rate]
    DEFAULT ((1)) FOR [Currency_Rate];
ALTER TABLE [AMS].[Notes_Master]
ADD CONSTRAINT [DF_Notes_Master_Audit_Lock]
    DEFAULT ((0)) FOR [Audit_Lock];
ALTER TABLE [AMS].[Notes_Master]
ADD CONSTRAINT [DF_Notes_Master_PrintValue]
    DEFAULT ((0)) FOR [PrintValue];
ALTER TABLE [AMS].[Product]
ADD CONSTRAINT [DF_Product_PQtyConv]
    DEFAULT ((0)) FOR [PQtyConv];
ALTER TABLE [AMS].[Product]
ADD CONSTRAINT [DF_Product_PAltConv]
    DEFAULT ((0)) FOR [PAltConv];
ALTER TABLE [AMS].[Product]
ADD CONSTRAINT [DF_Product_PSerialno]
    DEFAULT ((0)) FOR [PSerialno];
ALTER TABLE [AMS].[Product]
ADD CONSTRAINT [DF_Product_PSizewise]
    DEFAULT ((0)) FOR [PSizewise];
ALTER TABLE [AMS].[Product]
ADD CONSTRAINT [DF_Product_PBatchwise]
    DEFAULT ((0)) FOR [PBatchwise];
ALTER TABLE [AMS].[Product]
ADD CONSTRAINT [DF_Product_PBuyRate]
    DEFAULT ((0)) FOR [PBuyRate];
ALTER TABLE [AMS].[Product]
ADD CONSTRAINT [DF_Product_PSalesRate]
    DEFAULT ((0)) FOR [PSalesRate];
ALTER TABLE [AMS].[Product]
ADD CONSTRAINT [DF_Product_PMargin1]
    DEFAULT ((0)) FOR [PMargin1];
ALTER TABLE [AMS].[Product]
ADD CONSTRAINT [DF_Product_TradeRate]
    DEFAULT ((0)) FOR [TradeRate];
ALTER TABLE [AMS].[Product]
ADD CONSTRAINT [DF_Product_PMargin2]
    DEFAULT ((0)) FOR [PMargin2];
ALTER TABLE [AMS].[Product]
ADD CONSTRAINT [DF_Product_PMRP]
    DEFAULT ((0)) FOR [PMRP];
ALTER TABLE [AMS].[Product]
ADD CONSTRAINT [DF_Product_PTax]
    DEFAULT ((0)) FOR [PTax];
ALTER TABLE [AMS].[Product]
ADD CONSTRAINT [DF_Product_PMin]
    DEFAULT ((0.00)) FOR [PMin];
ALTER TABLE [AMS].[Product]
ADD CONSTRAINT [DF_Product_PMax]
    DEFAULT ((0)) FOR [PMax];
ALTER TABLE [AMS].[Product]
ADD CONSTRAINT [DF_Product_EnterBy]
    DEFAULT (N'MrSolution') FOR [EnterBy];
ALTER TABLE [AMS].[Product]
ADD CONSTRAINT [DF_Product_Status]
    DEFAULT ((1)) FOR [Status];
ALTER TABLE [AMS].[ProductGroup]
ADD CONSTRAINT [DF_ProductGroup_GMargin]
    DEFAULT ((0.00)) FOR [GMargin];
ALTER TABLE [AMS].[ProductGroup]
ADD CONSTRAINT [DF_ProductGroup_Status]
    DEFAULT ((1)) FOR [Status];
ALTER TABLE [AMS].[ProductGroup]
ADD CONSTRAINT [DF_ProductGroup_EnterBy]
    DEFAULT (N'MrSolution') FOR [EnterBy];
ALTER TABLE [AMS].[ProductSubGroup]
ADD CONSTRAINT [DF_ProductSubGroup_EnterBy]
    DEFAULT (N'MrSolution') FOR [EnterBy];
ALTER TABLE [AMS].[ProductSubGroup]
ADD CONSTRAINT [DF_ProductSubGroup_Status]
    DEFAULT ((1)) FOR [Status];
ALTER TABLE [AMS].[ProductUnit]
ADD CONSTRAINT [DF_ProductUnit_EnterBy]
    DEFAULT (N'MrSolution') FOR [EnterBy];
ALTER TABLE [AMS].[ProductUnit]
ADD CONSTRAINT [DF_ProductUnit_Status]
    DEFAULT ((1)) FOR [Status];
ALTER TABLE [AMS].[PT_Term]
ADD CONSTRAINT [DF_PT_Term_PT_Rate]
    DEFAULT ((0.00)) FOR [PT_Rate];
ALTER TABLE [AMS].[PT_Term]
ADD CONSTRAINT [DF_PT_Term_PT_Profitability]
    DEFAULT ((1)) FOR [PT_Profitability];
ALTER TABLE [AMS].[PT_Term]
ADD CONSTRAINT [DF_PT_Term_PT_Supess]
    DEFAULT ((1)) FOR [PT_Supess];
ALTER TABLE [AMS].[PT_Term]
ADD CONSTRAINT [DF_PT_Term_PT_Status]
    DEFAULT ((1)) FOR [PT_Status];
ALTER TABLE [AMS].[SeniorAgent]
ADD CONSTRAINT [DF_SeniorAgent_Comm]
    DEFAULT ((0.00)) FOR [Comm];
ALTER TABLE [AMS].[SeniorAgent]
ADD CONSTRAINT [DF_SeniorAgent_Company_ID]
    DEFAULT ((0.00)) FOR [Company_ID];
ALTER TABLE [AMS].[SeniorAgent]
ADD CONSTRAINT [DF_SeniorAgent_Status]
    DEFAULT ((1)) FOR [Status];
ALTER TABLE [AMS].[SR_Master]
ADD CONSTRAINT [DF_SR_Master_Cur_Rate]
    DEFAULT ((1)) FOR [Cur_Rate];
ALTER TABLE [AMS].[ST_Term]
ADD CONSTRAINT [DF_ST_Term_ST_Rate]
    DEFAULT ((0.00)) FOR [ST_Rate];
ALTER TABLE [AMS].[ST_Term]
ADD CONSTRAINT [DF_ST_Term_ST_Status]
    DEFAULT ((1)) FOR [ST_Status];
ALTER TABLE [AMS].[ST_Term]
ADD CONSTRAINT [DF_ST_Term_EnterBy]
    DEFAULT (N'MrSolution') FOR [EnterBy];
ALTER TABLE [AMS].[Subledger]
ADD CONSTRAINT [DF_Subledger_Status]
    DEFAULT ((1)) FOR [Status];
ALTER TABLE [AMS].[Subledger]
ADD CONSTRAINT [DF_Subledger_EnterBy]
    DEFAULT (N'MrSolution') FOR [EnterBy];
ALTER TABLE [AMS].[SystemConfiguration]
ADD CONSTRAINT [DF_SystemConfiguration_SC_Id]
    DEFAULT ((1)) FOR [SC_Id];
ALTER TABLE [AMS].[SystemConfiguration]
ADD CONSTRAINT [DF_SystemConfiguration_Date_Type]
    DEFAULT ('M') FOR [Date_Type];
ALTER TABLE [AMS].[SystemConfiguration]
ADD CONSTRAINT [DF_SystemConfiguration_Audit_Trial]
    DEFAULT ((0)) FOR [Audit_Trial];
ALTER TABLE [AMS].[SystemConfiguration]
ADD CONSTRAINT [DF_SystemConfiguration_Udf]
    DEFAULT ((0)) FOR [Udf];
ALTER TABLE [AMS].[SystemConfiguration]
ADD CONSTRAINT [DF_SystemConfiguration_AutoPupup]
    DEFAULT ((1)) FOR [AutoPupup];
ALTER TABLE [AMS].[SystemConfiguration]
ADD CONSTRAINT [DF_SystemConfiguration_CurrentDate]
    DEFAULT ((1)) FOR [CurrentDate];
ALTER TABLE [AMS].[SystemConfiguration]
ADD CONSTRAINT [DF_SystemConfiguration_ConfirmSave]
    DEFAULT ((0)) FOR [ConfirmSave];
ALTER TABLE [AMS].[SystemConfiguration]
ADD CONSTRAINT [DF_SystemConfiguration_ConfirmCancel]
    DEFAULT ((0)) FOR [ConfirmCancel];
ALTER TABLE [AMS].[SystemConfiguration]
ADD CONSTRAINT [DF_SystemConfiguration_Transby_Code]
    DEFAULT ((0)) FOR [Transby_Code];
ALTER TABLE [AMS].[SystemConfiguration]
ADD CONSTRAINT [DF_SystemConfiguration_Amount_Format]
    DEFAULT ((0.00)) FOR [Amount_Format];
ALTER TABLE [AMS].[SystemConfiguration]
ADD CONSTRAINT [DF_SystemConfiguration_Rate_Format]
    DEFAULT ((0.00)) FOR [Rate_Format];
ALTER TABLE [AMS].[SystemConfiguration]
ADD CONSTRAINT [DF_SystemConfiguration_Qty_Format]
    DEFAULT ((0.00)) FOR [Qty_Format];
ALTER TABLE [AMS].[SystemConfiguration]
ADD CONSTRAINT [DF_SystemConfiguration_AltQty_Format]
    DEFAULT ((0.00)) FOR [AltQty_Format];
ALTER TABLE [AMS].[SystemConfiguration]
ADD CONSTRAINT [DF_SystemConfiguration_Cur_Format]
    DEFAULT ((0.00)) FOR [Cur_Format];
ALTER TABLE [AMS].[SystemControlOptions]
ADD CONSTRAINT [DF_SystemControlOptions_Enable]
    DEFAULT ((0)) FOR [Enable];
ALTER TABLE [AMS].[SystemControlOptions]
ADD CONSTRAINT [DF_SystemControlOptions_Mandatory]
    DEFAULT ((0)) FOR [Mandatory];
ALTER TABLE [AMS].[TableMaster]
ADD CONSTRAINT [DF_TableMaster_Status]
    DEFAULT ((1)) FOR [Status];
ALTER TABLE [AMS].[TableMaster]
ADD CONSTRAINT [DF_TableMaster_EnterBy]
    DEFAULT ('MrSolution') FOR [EnterBy];
ALTER TABLE [INV].[Production_Master]
ADD CONSTRAINT [DF_Production_Master_Machine]
    DEFAULT (N'Main') FOR [Machine];
