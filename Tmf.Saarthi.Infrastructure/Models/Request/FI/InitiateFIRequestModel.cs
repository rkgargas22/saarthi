using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Request.FI;

public class InitiateFIRequestModel
{
    [JsonPropertyName("fIROSSRequest")]
    public FIRossRequest? FIRossRequest { get; set; }

    [JsonPropertyName("requestID")]
    public int RequestID { get; set; } 

    [JsonPropertyName("source")]
    public string Source { get; set; } = string.Empty;

    [JsonPropertyName("userContext")]
    public UserContext? UserContext { get; set; }
}

public class FIRossRequest
{
    [JsonPropertyName("dataDetails")]
    public DataDetails? DataDetails { get; set; }

    [JsonPropertyName("fIAllocationDetails")]
    public List<FIAllocationDetail> FIAllocationDetails { get; set; } = new List<FIAllocationDetail>();
}

public class UserContext
{
    [JsonPropertyName("userID")]
    public string UserID { get; set; } = string.Empty;

    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;
}

public class DataDetails
{
    [JsonPropertyName("reqTimestamp")]
    public DateTime ReqTimeStamp { get; set; }

    [JsonPropertyName("editFlag")]
    public string EditFlag { get; set; } = string.Empty;

    [JsonPropertyName("rossInwardFlag")]
    public string RossInwardFlag { get; set; } = string.Empty;

    [JsonPropertyName("userID")]
    public long UserId { get; set; } 

    [JsonPropertyName("fanNo")]
    public string FanNo { get; set; } = string.Empty;

    [JsonPropertyName("companyCode")]
    public int CompanyCode { get; set; } 

    [JsonPropertyName("fanGeneratedDate")]
    public DateTime FanGeneratedDate { get; set; } 

    [JsonPropertyName("branchName")]
    public string BranchName { get; set; } = string.Empty;

    [JsonPropertyName("branchCode")]
    public string BranchCode { get; set; } = string.Empty;

    [JsonPropertyName("branchSourceCode")]
    public double BranchSourceCode { get; set; } 

    [JsonPropertyName("branchCity")]
    public string BranchCity { get; set; } = string.Empty;

    [JsonPropertyName("branchState")]
    public string BranchState { get; set; } = string.Empty;

    [JsonPropertyName("channelCode")]
    public string ChannelCode { get; set; } = string.Empty;    

    [JsonPropertyName("schemeName")]
    public string SchemeName { get; set; } = string.Empty;

    [JsonPropertyName("shemeCode")]
    public string SchemeCode { get; set; } = string.Empty;

    [JsonPropertyName("variantName")]
    public string VariantName { get; set; } = string.Empty;

    [JsonPropertyName("variantCode")]
    public long VariantCode { get; set; } 

    [JsonPropertyName("lob")]
    public int Lob { get; set; }

    [JsonPropertyName("loanAmount")]
    public decimal LoanAmount { get; set; }

    [JsonPropertyName("tenure")]
    public int Tenure { get; set; }

    [JsonPropertyName("endVehicleUsage")]
    public int EndVehicleUsage { get; set; }

    [JsonPropertyName("applicantDetails")]
    public List<ApplicantDetail>? ApplicantDetails { get; set; }
}

public class FIAllocationDetail
{
    [JsonPropertyName("applicantType")]
    public string ApplicantType { get; set; } = string.Empty;

    [JsonPropertyName("compAddAllocCode")]
    public string CompAddAllocCode { get; set; } = string.Empty;

    [JsonPropertyName("offAddAllocCode")]
    public string OffAddAllocCode { get; set; } = string.Empty;

    [JsonPropertyName("perAddAllocCode")]
    public string PerAddAllocCode { get; set; } = string.Empty;

    [JsonPropertyName("resAddAllocCode")]
    public int ResAddAllocCode { get; set; }

    [JsonPropertyName("resFiByAgnorEmpCode")]
    public string ResFiByAgnorEmpCode { get; set; } = string.Empty;

    [JsonPropertyName("perFiByAgnorEmpCode")]
    public string PerFiByAgnorEmpCode { get; set; } = string.Empty;

    [JsonPropertyName("offFiByAgnorEmpCode")]
    public string OffFiByAgnorEmpCode { get; set; } = string.Empty;

    [JsonPropertyName("compFiByAgnorEmpCode")]
    public string CompFiByAgnorEmpCode { get; set; } = string.Empty;
}

public class AddressDataDetails
{
    [JsonPropertyName("officeAddress")]
    public Address? OfficeAddress { get; set; }

    [JsonPropertyName("permanentAddress")]
    public Address? PermanentAddress { get; set; }

    [JsonPropertyName("residentAddress")]
    public Address? ResidentAddress { get; set; }
}

public class ApplicantDetail
{
    [JsonPropertyName("customerType")]
    public int CustomerType { get; set; } 

    [JsonPropertyName("applicantType")]
    public string ApplicantType { get; set; } = string.Empty;

    [JsonPropertyName("custProfile")]
    public int CustProfile { get; set; }

    [JsonPropertyName("applicantTitle")]
    public int ApplicantTitle { get; set; }

    [JsonPropertyName("firstName")]
    public string FirstName { get; set; } = string.Empty;

    [JsonPropertyName("lastName")]
    public string LastName { get; set; } = string.Empty;

    [JsonPropertyName("relationshipType")]
    public int RelationshipType { get; set; } 

    [JsonPropertyName("fatherSpouseName")]
    public string FatherSpouseName { get; set; } = string.Empty;

    [JsonPropertyName("dob")]
    public DateTime? Dob { get; set; }

    [JsonPropertyName("gender")]
    public int Gender { get; set; }

    [JsonPropertyName("companyName")]
    public string CompanyName { get; set; } = string.Empty;

    [JsonPropertyName("compIncrpDate")]
    public string CompIncrpDate { get; set; } = string.Empty;

    [JsonPropertyName("maritalStatus")]
    public int MaritalStatus { get; set; } 

    [JsonPropertyName("propertyType")]
    public int PropertyType { get; set; }

    [JsonPropertyName("reltnWithAppCode")]
    public int ReltnWithAppCode { get; set; }

    [JsonPropertyName("investmentInEquip")]
    public int InvestmentInEquip { get; set; }

    [JsonPropertyName("houseOwner")]
    public int HouseOwner { get; set; }

    [JsonPropertyName("addressDataDetails")]
    public AddressDataDetails? AddressDataDetails { get; set; }
}

public class Address
{
    [JsonPropertyName("add1")]
    public string Add1 { get; set; } = string.Empty;

    [JsonPropertyName("add2")]
    public string Add2 { get; set; } = string.Empty;

    [JsonPropertyName("city")]
    public string City { get; set; } = string.Empty;

    [JsonPropertyName("district")]
    public string District { get; set; } = string.Empty;

    [JsonPropertyName("landmark")]
    public string Landmark { get; set; } = string.Empty;

    [JsonPropertyName("pincode")]
    public int Pincode { get; set; } 

    [JsonPropertyName("state")]
    public string State { get; set; } = string.Empty;

    [JsonPropertyName("mobNo")]
    public string MobNo { get; set; } = string.Empty;
}
