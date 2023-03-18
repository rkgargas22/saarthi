using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using Tmf.Saarthi.Core.Options;
using Tmf.Saarthi.Infrastructure.HttpService;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request.Admin;
using Tmf.Saarthi.Infrastructure.Models.Response.Admin;
using Tmf.Saarthi.Infrastructure.Models.Response.Customer;
using Tmf.Saarthi.Infrastructure.SqlService;

namespace Tmf.Saarthi.Infrastructure.Services
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ISqlUtility _sqlUtility;
        private readonly IHttpService _httpService;
        private readonly ConnectionStringsOptions _connectionStringsOptions;
        

        public AdminRepository(ISqlUtility sqlUtility, IOptions<ConnectionStringsOptions> connectionStringsOptions, IHttpService httpService, IOptions<InstaVeritaOptions> instaVeritaOptions)
        {
            _sqlUtility = sqlUtility;
            _httpService = httpService;
            _connectionStringsOptions = connectionStringsOptions.Value;            
        }

        public async Task<List<AdminDashbaordResponseModel>> GetAdminDashboard()
        {
            DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_getAdminDashboard");

            List<AdminDashbaordResponseModel> adminDashbaordResponseModelList = new List<AdminDashbaordResponseModel>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    AdminDashbaordResponseModel adminDashbaordResponseModel = new AdminDashbaordResponseModel();
                    adminDashbaordResponseModel.ApplicationId = (Int64)dt.Rows[i]["APPLICATIONID"];
                    adminDashbaordResponseModel.CustomerName = dt.Rows[i]["CUSTOMERNAME"] == DBNull.Value ? "" : (string)dt.Rows[i]["CUSTOMERNAME"];
                    adminDashbaordResponseModel.AssignDateTime = dt.Rows[i]["AssingedDate"] == DBNull.Value ? "" : (string)dt.Rows[i]["AssingedDate"];
                    adminDashbaordResponseModel.ExprDate = dt.Rows[i]["ExprDate"] == DBNull.Value ? "" : (string)dt.Rows[i]["ExprDate"];
                    adminDashbaordResponseModel.Status = dt.Rows[i]["Status"] == DBNull.Value ? "" : (string)dt.Rows[i]["Status"];
                    adminDashbaordResponseModelList.Add(adminDashbaordResponseModel);
                }
            }

            return adminDashbaordResponseModelList;
        }

        public async Task<List<AdminFleetResponseModel>> GetAdminFleet(long FleetId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>()
            {
            new SqlParameter("FleetId", FleetId)
            };
            DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_getAdminFleet", parameters);

            List<AdminFleetResponseModel> adminFleetResponseModelList = new List<AdminFleetResponseModel>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    AdminFleetResponseModel adminFleetResponseModel = new AdminFleetResponseModel();
                    adminFleetResponseModel.VehicleId = (Int64)dt.Rows[i]["VehicleId"];
                    adminFleetResponseModel.FleetID = (Int64)dt.Rows[i]["FleetID"];
                    adminFleetResponseModel.RegistrationNo = dt.Rows[i]["REGISTRATIONNO"] == DBNull.Value ? "" : (string)dt.Rows[i]["REGISTRATIONNO"];
                    adminFleetResponseModel.OwnerName = dt.Rows[i]["OWNERNAME"] == DBNull.Value ? "" : (string)dt.Rows[i]["OWNERNAME"];
                    adminFleetResponseModel.Year = dt.Rows[i]["YEAR"] == DBNull.Value ? 0 : (int)dt.Rows[i]["YEAR"];
                    adminFleetResponseModel.Category = dt.Rows[i]["CATEGORY"] == DBNull.Value ? "" : (string)dt.Rows[i]["CATEGORY"];
                    adminFleetResponseModel.ModelType = dt.Rows[i]["MODELTYPE"] == DBNull.Value ? "" : (string)dt.Rows[i]["MODELTYPE"];
                    adminFleetResponseModel.Manufacturer = dt.Rows[i]["MANUFACTURER"] == DBNull.Value ? "" : (string)dt.Rows[i]["MANUFACTURER"];
                    adminFleetResponseModel.Status = (bool)dt.Rows[i]["STATUS"];
                    adminFleetResponseModelList.Add(adminFleetResponseModel);
                }
            }

            return adminFleetResponseModelList;
        }

        public async Task<AdminFleetDeviationResponseModel> GetAdminFleetDeviation(long FleetId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>()
            {
            new SqlParameter("FleetId", FleetId)
            };
            DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_getAdminFleetDeviation", parameters);
            AdminFleetDeviationResponseModel adminFleetDeviationResponseModel = new AdminFleetDeviationResponseModel();

            if (dt.Rows.Count > 0)
            {
                adminFleetDeviationResponseModel.FleetId = (Int64)dt.Rows[0]["FleetId"];
                adminFleetDeviationResponseModel.OgIRR = dt.Rows[0]["OgIRR"] == DBNull.Value ? 0 : (decimal)dt.Rows[0]["OgIRR"];
                adminFleetDeviationResponseModel.OgAIR = dt.Rows[0]["OgAIR"] == DBNull.Value ? 0 : (decimal)dt.Rows[0]["OgAIR"];
                adminFleetDeviationResponseModel.OgProcessingFee = dt.Rows[0]["OgProcessingFee"] == DBNull.Value ? 0 : (decimal)dt.Rows[0]["OgProcessingFee"];
                adminFleetDeviationResponseModel.StampDuty = dt.Rows[0]["StampDuty"] == DBNull.Value ? 0 : (decimal)dt.Rows[0]["StampDuty"];
                adminFleetDeviationResponseModel.RequestedIRR = dt.Rows[0]["RequestedIRR"] == DBNull.Value ? 0 : (decimal)dt.Rows[0]["RequestedIRR"];
                adminFleetDeviationResponseModel.RequestedARR = dt.Rows[0]["RequestedAIR"] == DBNull.Value ? 0 : (decimal)dt.Rows[0]["RequestedAIR"];
                adminFleetDeviationResponseModel.RequestedProcessingFees = dt.Rows[0]["RequestedProcessingFees"] == DBNull.Value ? 0 : (decimal)dt.Rows[0]["RequestedProcessingFees"];
                adminFleetDeviationResponseModel.NewIRR = Convert.ToString(dt.Rows[0]["NewIRR"]);
                adminFleetDeviationResponseModel.NewAIR = Convert.ToString(dt.Rows[0]["NewAIR"]);
                adminFleetDeviationResponseModel.NewProcessing = Convert.ToString(dt.Rows[0]["NewProcessing"]);
            }

            return adminFleetDeviationResponseModel;
        }


        public async Task<List<AdminCaseOverViewResponseModel>> GetAdminCaseOverViewData(long fleetId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("FleetID", fleetId),
        };
            DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_getAdminCaseOverview", parameters);
            List<AdminCaseOverViewResponseModel> adminCaseOverViewResponseModels = new List<AdminCaseOverViewResponseModel>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    AdminCaseOverViewResponseModel adminCaseOverViewResponseModel = new AdminCaseOverViewResponseModel();
                    adminCaseOverViewResponseModel.IsAgreementLetterApproved = dt.Rows[i]["IsAgreementLetterApproved"] == DBNull.Value ? null : (bool)dt.Rows[i]["IsAgreementLetterApproved"];
                    adminCaseOverViewResponseModel.IsProvisionLetterApproved = dt.Rows[i]["IsProvisionLetterApproved"] == DBNull.Value ? null : (bool)dt.Rows[i]["IsProvisionLetterApproved"];
                    adminCaseOverViewResponseModel.IsSanctionLetterApproved = dt.Rows[i]["IsSanctionLetterApproved"] == DBNull.Value ? null : (bool)dt.Rows[i]["IsSanctionLetterApproved"];
                    adminCaseOverViewResponseModel.Comment = dt.Rows[i]["Comment"] == DBNull.Value ? "" : (string)dt.Rows[i]["Comment"];
                    adminCaseOverViewResponseModel.CreatedDate = (DateTime)dt.Rows[i]["CreatedDate"];
                    adminCaseOverViewResponseModels.Add(adminCaseOverViewResponseModel);
                }
            }
            return adminCaseOverViewResponseModels;
        }

        public async Task<AdminFleetResponseModel> ApproveAdminFleetDeviation(ApproveAdminFleetDeviationRequestModel approveAdminFleetDeviationRequestModel)
        {
            AdminFleetResponseModel adminFleetResponseModel = new AdminFleetResponseModel();
            if (approveAdminFleetDeviationRequestModel.VehicleId != null)
            {
                int loop = approveAdminFleetDeviationRequestModel.VehicleId.Length;
                for (int i = 0; i < approveAdminFleetDeviationRequestModel.VehicleId.Length; i++)
                {
                    List<SqlParameter> parameters = new List<SqlParameter>()
                {
                new SqlParameter("VehicleId", approveAdminFleetDeviationRequestModel.VehicleId[i]),
                new SqlParameter("Comment", approveAdminFleetDeviationRequestModel.Comment),
                new SqlParameter("AdminId", 41),
                };
                    DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_ApproveFleetByAdmin", parameters);
                    if (dt.Rows.Count > 0)
                    {
                        adminFleetResponseModel.VehicleId = Convert.ToInt64(dt.Rows[0]["VehicleID"]);
                    }

                }
            }


            return adminFleetResponseModel;
        }

        public async Task<List<CustomerDataResponseModel>> GetCustomerData(long fleetId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>()
            {
            new SqlParameter("FleetId", fleetId)
            };
            DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_getCustomerDetails", parameters);
            List<CustomerDataResponseModel> customerDataResponseModelList = new List<CustomerDataResponseModel>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CustomerDataResponseModel customerDataResponseModel = new CustomerDataResponseModel();
                    customerDataResponseModel.BPNumber = (Int64)dt.Rows[i]["BPNumber"];
                    customerDataResponseModel.FleetID = (Int64)dt.Rows[i]["FleetID"];
                    customerDataResponseModel.FirstName = dt.Rows[i]["FirstName"] == DBNull.Value ? "" : (string)dt.Rows[i]["FirstName"];
                    customerDataResponseModel.MiddleName = dt.Rows[i]["MiddleName"] == DBNull.Value ? "" : (string)dt.Rows[i]["MiddleName"];
                    customerDataResponseModel.LastName = dt.Rows[i]["LastName"] == DBNull.Value ? "" : (string)dt.Rows[i]["LastName"];
                    customerDataResponseModel.Gender = dt.Rows[i]["Gender"] == DBNull.Value ? "" : (string)dt.Rows[i]["Gender"];
                    customerDataResponseModel.Dob = dt.Rows[i]["Dob"] == DBNull.Value ? "" : (string)dt.Rows[i]["Dob"];
                    customerDataResponseModel.PanNo = dt.Rows[i]["PanNo"] == DBNull.Value ? "" : (string)dt.Rows[i]["PanNo"];
                    customerDataResponseModel.FanNo = dt.Rows[i]["FanNo"] == DBNull.Value ? "" : (string)dt.Rows[i]["FanNo"];
                    customerDataResponseModel.MobileNo = dt.Rows[i]["MobileNo"] == DBNull.Value ? "" : (string)dt.Rows[i]["MobileNo"];
                    customerDataResponseModelList.Add(customerDataResponseModel);
                }
            }

            return customerDataResponseModelList;
        }


        public async Task<AdminFleetDeviationResponseModel> UpdateAdminFleetDeviation(AdminFleetDeviationRequestModel adminFleetDeviationRequestModel)
        {
            List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("FleetId", adminFleetDeviationRequestModel.FleetID),
            new SqlParameter("IsIRR", adminFleetDeviationRequestModel.IsIRR),
            new SqlParameter("NewIRR", adminFleetDeviationRequestModel.NewIRR),
            new SqlParameter("IsAIR", adminFleetDeviationRequestModel.IsAIR),
            new SqlParameter("NewAIR", adminFleetDeviationRequestModel.NewAIR),
            new SqlParameter("IsProcessing", adminFleetDeviationRequestModel.IsProcessing),
            new SqlParameter("NewProcessing", adminFleetDeviationRequestModel.NewProcessing)
        };

            DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_updateAdminFleetDeviation", parameters);

            AdminFleetDeviationResponseModel adminFleetDeviationResponseModel = new AdminFleetDeviationResponseModel();
            if (dt.Rows.Count > 0)
            {
                adminFleetDeviationResponseModel.FleetId = Convert.ToInt64(dt.Rows[0]["FleetID"]);
            }

            return adminFleetDeviationResponseModel;
        }
    }
}
