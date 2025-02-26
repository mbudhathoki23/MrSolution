using MrDAL.Domains.Shared.UserAccessControl.Models;
using System.Collections.Generic;
using System.Linq;

namespace MrDAL.Domains.Shared.UserAccessControl;

public static class StaticModels
{
    private static IList<UacFeatureBox> _featureBoxes;

    private static IEnumerable<UacAction> IudActions => new[]
    {
        UacAction.All, UacAction.Create, UacAction.Modify, UacAction.Delete
    };

    public static IList<UacFeatureBox> GetAllFeatures()
    {
        if (_featureBoxes != null && _featureBoxes.Any()) return _featureBoxes;

        _featureBoxes = new List<UacFeatureBox>
        {
            // administrative
            new(UacModule.Administrative, null, UacAccessFeature.DatabaseBackUp, new[] { UacAction.Create }),
            new(UacModule.Administrative, null, UacAccessFeature.DatabaseRestore, new[] { UacAction.Create }),

            // master setup
            new(UacModule.Master, null, UacAccessFeature.Terminal, IudActions),
            new(UacModule.Master, UacFeatureGroup.OpeningMaster, UacAccessFeature.LedgerOpening),
            new(UacModule.Master, UacFeatureGroup.OpeningMaster, UacAccessFeature.ProductOpening, IudActions),
            new(UacModule.Master, UacFeatureGroup.CurrencyMaster, UacAccessFeature.CurrencySetup, IudActions),
            new(UacModule.Master, UacFeatureGroup.CurrencyMaster, UacAccessFeature.CurrencyRate, IudActions),
            new(UacModule.Master, UacFeatureGroup.ProductMaster, UacAccessFeature.ProductGroups, IudActions),
            new(UacModule.Master, UacFeatureGroup.ProductMaster, UacAccessFeature.ProductSubGroups, IudActions),
            new(UacModule.Master, UacFeatureGroup.ProductMaster, UacAccessFeature.Products, IudActions),
            new(UacModule.Master, UacFeatureGroup.ProductMaster, UacAccessFeature.CounterProducts, IudActions),
            new(UacModule.Master, UacFeatureGroup.ProductMaster, UacAccessFeature.ProductUnits, IudActions),
            new(UacModule.Master, UacFeatureGroup.ProductMaster, UacAccessFeature.Godowns, IudActions),
            new(UacModule.Master, UacFeatureGroup.ProductMaster, UacAccessFeature.WareHouses, IudActions),
            new(UacModule.Master, UacFeatureGroup.ProductMaster, UacAccessFeature.Racks, IudActions),
            new(UacModule.Master, UacFeatureGroup.MembershipMaster, UacAccessFeature.PosMembership, IudActions),
            new(UacModule.Master, UacFeatureGroup.MembershipMaster, UacAccessFeature.PosMembershipTypes, IudActions),

            // account

            // inventory
            new(UacModule.Inventory, UacFeatureGroup.InventoryOperations, UacAccessFeature.Purchase, IudActions),
            new(UacModule.Inventory, UacFeatureGroup.InventoryOperations, UacAccessFeature.PurchaseReturn, IudActions),
            new(UacModule.Inventory, UacFeatureGroup.InventoryOperations, UacAccessFeature.StockAdjustment, IudActions),
            new(UacModule.Inventory, UacFeatureGroup.InventoryOperations, UacAccessFeature.ProductBomConfig,
                IudActions),
            new(UacModule.Inventory, UacFeatureGroup.InventoryOperations, UacAccessFeature.PurchaseQuotation,
                IudActions),
            new(UacModule.Inventory, UacFeatureGroup.InventoryOperations, UacAccessFeature.PurchaseChallan, IudActions),
            new(UacModule.Inventory, UacFeatureGroup.InventoryOperations, UacAccessFeature.PurchaseOrder, IudActions),
            new(UacModule.Inventory, UacFeatureGroup.InventoryOperations, UacAccessFeature.PurchaseChallanReturn,
                IudActions),
            new(UacModule.Inventory, UacFeatureGroup.InventoryOperations, UacAccessFeature.StockIndent, IudActions),
            new(UacModule.Inventory, UacFeatureGroup.InventoryOperations, UacAccessFeature.StockTransfer, IudActions),
            new(UacModule.Inventory, UacFeatureGroup.InventoryOperations, UacAccessFeature.StockConsumption,
                IudActions),
            new(UacModule.Inventory, UacFeatureGroup.InventoryOperations, UacAccessFeature.ProductExchange, IudActions),
            new(UacModule.Inventory, UacFeatureGroup.InventoryOperations, UacAccessFeature.ExpiryBreakage, IudActions),
            new(UacModule.Inventory, UacFeatureGroup.InventoryOperations, UacAccessFeature.Production, IudActions),
            new(UacModule.Inventory, UacFeatureGroup.InventoryOperations, UacAccessFeature.StockRequisition,
                IudActions),
            new(UacModule.Inventory, UacFeatureGroup.InventoryOperations, UacAccessFeature.RawMaterialIssue,
                IudActions),
            new(UacModule.Inventory, UacFeatureGroup.InventoryOperations, UacAccessFeature.RawMaterialReturn,
                IudActions),
            new(UacModule.Inventory, UacFeatureGroup.InventoryOperations, UacAccessFeature.FinishedGoodReceive,
                IudActions),
            new(UacModule.Inventory, UacFeatureGroup.InventoryOperations, UacAccessFeature.FinishedGoodsReturn,
                IudActions),

            // point of sale
            new(UacModule.Pos, UacFeatureGroup.Billing, UacAccessFeature.TaxInvoice, IudActionsWithAppend(new[]
            {
                UacAction.ChangeRate, UacAction.ImportExternal, UacAction.Print, UacAction.Reprint, UacAction.Reverse,
                UacAction.ItemWiseDiscount, UacAction.BillDiscount, UacAction.CreateTemp
            })),
            new(UacModule.Pos, UacFeatureGroup.Billing, UacAccessFeature.AbbreviatedTaxInvoice, IudActionsWithAppend(
                new[]
                {
                    UacAction.ImportExternal, UacAction.Print, UacAction.Reprint, UacAction.Reverse,
                    UacAction.ChangeRate, UacAction.ItemWiseDiscount, UacAction.BillDiscount, UacAction.CreateTemp
                })),
            new(UacModule.Pos, UacFeatureGroup.Billing, UacAccessFeature.DayClosing, IudActions),
            new(UacModule.Pos, UacFeatureGroup.Billing, UacAccessFeature.SalesQuotation, IudActionsWithAppend(new[]
            {
                UacAction.ChangeRate, UacAction.ImportExternal, UacAction.Print, UacAction.Reprint, UacAction.Reverse
            })),
            new(UacModule.Pos, UacFeatureGroup.Billing, UacAccessFeature.SalesChallan, IudActionsWithAppend(new[]
            {
                UacAction.ImportExternal, UacAction.Print, UacAction.Reprint, UacAction.Reverse
            })),
            new(UacModule.Pos, UacFeatureGroup.Billing, UacAccessFeature.SalesOrder, IudActionsWithAppend(new[]
                { UacAction.ImportExternal, UacAction.Print, UacAction.Reprint, UacAction.Reverse })),
            new(UacModule.Pos, UacFeatureGroup.Billing, UacAccessFeature.SalesReturn, IudActionsWithAppend(new[]
            {
                UacAction.ChangeRate, UacAction.ImportExternal, UacAction.Print, UacAction.Reprint, UacAction.Reverse,
                UacAction.ItemWiseDiscount, UacAction.BillDiscount, UacAction.CreateTemp
            }))

            //reports
        };
        return _featureBoxes.OrderBy(x => x.ModuleName).ThenBy(x => x.FeatureGroupName).ThenBy(x => x.FeatureName)
            .ToList();
    }

    public static IList<UacFeatureBox> GetAllPermissions(UacModule? module, UacFeatureGroup? group)
    {
        var query = GetAllFeatures().AsQueryable();
        if (module.HasValue) query = query.Where(x => x.Module == module.Value);
        if (group.HasValue) query = query.Where(x => x.FeatureGroup == group.Value);

        return query.ToList();
    }

    private static IList<UacAction> IudActionsWithAppend(UacAction? actionToAppend = null)
    {
        var actions = new List<UacAction>
        {
            UacAction.All, UacAction.Create, UacAction.Modify, UacAction.Delete
        };

        if (actionToAppend.HasValue) actions.Add(actionToAppend.Value);
        return actions;
    }

    private static IList<UacAction> IudActionsWithAppend(IEnumerable<UacAction> actionsToAppend)
    {
        var actions = new List<UacAction>
        {
            UacAction.All, UacAction.Create, UacAction.Modify, UacAction.Delete
        };

        foreach (var uacAction in actionsToAppend)
            actions.Add(uacAction);
        return actions;
    }
}