namespace MrDAL.Domains.MrSchoolTime.ViewModule;

public class ObjSchMaster
{
    public long MasterId { get; set; }
    public long TxtLedgerId { get; set; }
    public long TxtPBLedgerId { get; set; }
    public long TxtPRLedgerId { get; set; }
    public long TxtSBLedgerId { get; set; }
    public long TxtSRLedgerId { get; set; }
    public long TxtPL_OPLedgerId { get; set; }
    public long TxtPL_ClLedgerId { get; set; }
    public long TxtBS_ClLedgerId { get; set; }
    public int TxtGroupId { get; set; }
    public int TxtSubGroupId { get; set; }
    public int TxtUnitId { get; set; }
    public int TxtAltUnitId { get; set; }
    public int TxtSchedule { get; set; }
    public int TxtPrimaryGroupId { get; set; }
    public int TxtSecondaryGroupId { get; set; }
    public int TxtBranchId { get; set; }
    public int TxtCompanyUnitId { get; set; }
    public int TxtMemberTypeId { get; set; }
    public int TxtMemberId { get; set; }
    public int TxtCompanyId { get; set; }
    public int TxtModelId { get; set; }
    public int TxtColorId { get; set; }
    public int TxtVehicleNoId { get; set; }

    public bool TxtStatus { get; set; }
    public bool TxtSerialNo { get; set; }
    public bool TxtSizeWise { get; set; }
    public bool TxtBatchWise { get; set; }
    public bool TxtIsDefault { get; set; } = false;

    public string _ActionTag { get; set; }
    public string TxtDescription { get; set; }
    public string TxtAliasDescription { get; set; }
    public string TxtNepaliName { get; set; }
    public string TxtShortName { get; set; }
    public string TxtPhoneNo { get; set; }
    public string TxtEmailAdd { get; set; }
    public string TxtResPhoneNo { get; set; }
    public string TxtLandLineNo { get; set; }
    public string TxtPrimaryGrp { get; set; }
    public string TxtSecondaryGroup { get; set; }
    public string TxtEnterBy { get; set; }
    public string TxtType { get; set; }
    public string TxtCategory { get; set; }
    public string TxtGrpType { get; set; }
    public string TxtStartDate { get; set; }
    public string TxtStartMiti { get; set; }
    public string TxtExpireDate { get; set; }
    public string TxtExpireMiti { get; set; }
    public string TxtPrinter { get; set; }
    public string TxtChassisNo { get; set; }
    public string TxtEngineNo { get; set; }
    public string TxtValMethod { get; set; }

    public double TxtTaxRate { get; set; }
    public double TxtConQty { get; set; }
    public double TxtConAltQty { get; set; }
    public double TxtQty { get; set; }
    public double TxtReOrderQty { get; set; }
    public double TxtReOrderLvlQty { get; set; }
    public double TxtMinQty { get; set; }
    public double TxtMaxQty { get; set; }
    public double TxtRate { get; set; }
    public double TxtAmount { get; set; }
    public double TxtBuyRate { get; set; }
    public double TxtSalesRate { get; set; }
    public double TxtMargin { get; set; }
    public double TxtMargin1 { get; set; }
    public double TxtMRP { get; set; }
    public double TxtTradeRate { get; set; }
    public double TxtBeforeVatSales { get; set; }
    public double TxtBeforeVatPurchase { get; set; }
    public string ProductImage { get; set; }

    public int TxtStudentId { get; set; }
    public string CmbTitle { get; set; }
    public string TxtName { get; set; }
    public string TxtLedger { get; set; }
    public long TxtContactNp { get; set; }
    public int TxtAge { get; set; }
    public string CmbAgeType { get; set; }
    public string CmbGender { get; set; }
    public string CmbMartial { get; set; }
    public string CmbRegionType { get; set; }
    public string CmbReligion { get; set; }
    public string CmbNationality { get; set; }
    public string CmbBloodGroup { get; set; }
    public string TxtTState { get; set; }
    public string TxtTStreet { get; set; }
    public string TxtTCity { get; set; }
    public string TxtTAddress { get; set; }
    public string TxtPState { get; set; }
    public string TxtPStreet { get; set; }
    public string TxtPCity { get; set; }
    public string TxtPAddress { get; set; }
    public string TxtClass { get; set; }
    public string TxtSection { get; set; }
    public string TxtFaculty { get; set; }
    public string TxtGName { get; set; }
    public long TxtTelephoneNumber { get; set; }
    public long TxtGMobileNumber { get; set; }
    public string TxtGOccupation { get; set; }
}