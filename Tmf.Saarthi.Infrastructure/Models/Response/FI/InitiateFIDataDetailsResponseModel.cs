namespace Tmf.Saarthi.Infrastructure.Models.Response.FI
{
    public class InitiateFIDataDetailsResponseModel
    {
        #region DataDetails
        public DateTime ReqTimeStamp { get; set; }

        public string EditFlag { get; set; } = string.Empty;

        public string RossInwardFlag { get; set; } = string.Empty;

        public long UserId { get; set; }

        public string FanNo { get; set; } = string.Empty;

        public int CompanyCode { get; set; }

        public DateTime FanGeneratedDate { get; set; }

        public string BranchName { get; set; } = string.Empty;

        public string BranchCode { get; set; } = string.Empty;

        public double BranchSourceCode { get; set; }

        public string BranchCity { get; set; } = string.Empty;

        public string BranchState { get; set; } = string.Empty;

        public string ChannelCode { get; set; } = string.Empty;

        public string SchemeName { get; set; } = string.Empty;

        public string SchemeCode { get; set; } = string.Empty;

        public string VariantName { get; set; } = string.Empty;

        public long VariantCode { get; set; }

        public int Lob { get; set; }

        public decimal LoanAmount { get; set; }

        public int Tenure { get; set; }

        public int EndVehicleUsage { get; set; }
        #endregion

        #region ApplicantDetails
        public int CustomerType { get; set; }

        public string ApplicantType { get; set; } = string.Empty;

        public int CustProfile { get; set; }

        public int ApplicantTitle { get; set; } 

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public int RelationshipType { get; set; }

        public string FatherSpouseName { get; set; } = string.Empty;

        public DateTime? Dob { get; set; }

        public int Gender { get; set; }

        public string CompanyName { get; set; } = string.Empty;

        public string CompIncrpDate { get; set; } = string.Empty;

        public int MaritalStatus { get; set; }

        public int PropertyType { get; set; }

        public int ReltnWithAppCode { get; set; }

        public int InvestmentInEquip { get; set; }

        public int HouseOwner { get; set; }
        #endregion

        #region ResidentAddress
        public string ResidentAdd1 { get; set; } = string.Empty;

        public string ResidentAdd2 { get; set; } = string.Empty;

        public string ResidentCity { get; set; } = string.Empty;

        public string ResidentDistrict { get; set; } = string.Empty;

        public string ResidentLandmark { get; set; } = string.Empty;

        public int ResidentPincode { get; set; } 

        public string ResidentState { get; set; } = string.Empty;

        public string ResidentMobNo { get; set; } = string.Empty;
        #endregion


        #region OfficeAddress
        public string OfficeAdd1 { get; set; } = string.Empty;

        public string OfficeAdd2 { get; set; } = string.Empty;

        public string OfficeCity { get; set; } = string.Empty;

        public string OfficeDistrict { get; set; } = string.Empty;

        public string OfficeLandmark { get; set; } = string.Empty;

        public int OfficePincode { get; set; } 

        public string OfficeState { get; set; } = string.Empty;

        public string OfficeMobNo { get; set; } = string.Empty;
        #endregion

        #region PermanentAddress
        public string PermanentAdd1 { get; set; } = string.Empty;

        public string PermanentAdd2 { get; set; } = string.Empty;

        public string PermanentCity { get; set; } = string.Empty;

        public string PermanentDistrict { get; set; } = string.Empty;

        public string PermanentLandmark { get; set; } = string.Empty;

        public int PermanentPincode { get; set; }

        public string PermanentState { get; set; } = string.Empty;

        public string PermanentMobNo { get; set; } = string.Empty;
        #endregion

        #region FIAllocationDetails
        public string CompAddAllocCode { get; set; } = string.Empty;

        public string OffAddAllocCode { get; set; } = string.Empty;

        public string PerAddAllocCode { get; set; } = string.Empty;

        public int ResAddAllocCode { get; set; }

        public string ResFiByAgnorEmpCode { get; set; } = string.Empty;

        public string PerFiByAgnorEmpCode { get; set; } = string.Empty;

        public string OffFiByAgnorEmpCode { get; set; } = string.Empty;

        public string CompFiByAgnorEmpCode { get; set; } = string.Empty;
        #endregion
    }
}
