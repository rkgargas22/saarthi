using Microsoft.AspNetCore.Mvc;
using System.IO;
using Tmf.Saarthi.Core.RequestModels.Admin;
using Tmf.Saarthi.Core.RequestModels.Fleet;
using Tmf.Saarthi.Core.ResponseModels.Admin;
using Tmf.Saarthi.Core.ResponseModels.Agent;
using Tmf.Saarthi.Core.ResponseModels.Fleet;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request.Admin;
using Tmf.Saarthi.Infrastructure.Models.Request.Fleet;
using Tmf.Saarthi.Infrastructure.Models.Request.Ocr;
using Tmf.Saarthi.Infrastructure.Models.Response.Admin;
using Tmf.Saarthi.Infrastructure.Models.Response.Agent;
using Tmf.Saarthi.Infrastructure.Models.Response.Fleet;
using Tmf.Saarthi.Infrastructure.Services;
using Tmf.Saarthi.Manager.Interfaces;

namespace Tmf.Saarthi.Manager.Services
{
    public class AdminManager : IAdminManager
    {
        private readonly IAdminRepository _adminRepository;
        public AdminManager(IAdminRepository aminRepository)
        {
            _adminRepository = aminRepository;
        }
        public async Task<List<AdminDashbaordResponse>> GetAdminDashbaord()
        {
            List<AdminDashbaordResponseModel> adminDashbaordResponseModelList = await _adminRepository.GetAdminDashboard();
            List<AdminDashbaordResponse> adminDashboardResponses = new List<AdminDashbaordResponse>();

            foreach (AdminDashbaordResponseModel model in adminDashbaordResponseModelList)
            {
                AdminDashbaordResponse adminDashbaordResponse = new AdminDashbaordResponse();
                adminDashbaordResponse.ApplicationId = model.ApplicationId;
                adminDashbaordResponse.CustomerName = model.CustomerName;
                adminDashbaordResponse.AssignDateTime = model.AssignDateTime;
                adminDashbaordResponse.ExprDate = model.ExprDate;
                adminDashbaordResponse.Status = model.Status;
                adminDashboardResponses.Add(adminDashbaordResponse);
            }

            return adminDashboardResponses;
        }

        public async Task<List<AdminFleetResponse>> GetAdminFleet(long FleetId)
        {
            List<AdminFleetResponseModel> adminFleetResponseModelList = await _adminRepository.GetAdminFleet(FleetId);
            List<AdminFleetResponse> adminFleetResponses = new List<AdminFleetResponse>();

            foreach (AdminFleetResponseModel model in adminFleetResponseModelList)
            {
                AdminFleetResponse adminFleetResponse = new AdminFleetResponse();
                adminFleetResponse.VehicleId = model.VehicleId;
                adminFleetResponse.RegistrationNo = model.RegistrationNo;
                adminFleetResponse.OwnerName = model.OwnerName;
                adminFleetResponse.Year = model.Year;
                adminFleetResponse.Category = model.Category;
                adminFleetResponse.ModelType = model.ModelType;
                adminFleetResponse.Manufacturer = model.Manufacturer;
                adminFleetResponse.Status = model.Status;
                adminFleetResponses.Add(adminFleetResponse);
            }

            return adminFleetResponses;
        }

        public async Task<AdminFleetDeviationResponse> GetAdminFleetDeviation(long FleetId)
        {
            AdminFleetDeviationResponseModel adminFleetDeviationResponseModel = await _adminRepository.GetAdminFleetDeviation(FleetId);
            AdminFleetDeviationResponse adminFleetDeviationResponse = new AdminFleetDeviationResponse();

            adminFleetDeviationResponse.FleetId = adminFleetDeviationResponseModel.FleetId;
            adminFleetDeviationResponse.OgIRR = adminFleetDeviationResponseModel.OgIRR;
            adminFleetDeviationResponse.OgAIR = adminFleetDeviationResponseModel.OgAIR;
            adminFleetDeviationResponse.OgProcessingFee = adminFleetDeviationResponseModel.OgProcessingFee;
            adminFleetDeviationResponse.StampDuty = adminFleetDeviationResponseModel.StampDuty;
            adminFleetDeviationResponse.RequestedIRR = adminFleetDeviationResponseModel.RequestedIRR;
            adminFleetDeviationResponse.RequestedARR = adminFleetDeviationResponseModel.RequestedARR;
            adminFleetDeviationResponse.RequestedProcessingFees = adminFleetDeviationResponseModel.RequestedProcessingFees;
            adminFleetDeviationResponse.NewIRR = Convert.ToString(adminFleetDeviationResponseModel.NewIRR);
            adminFleetDeviationResponse.NewAIR = Convert.ToString(adminFleetDeviationResponseModel.NewAIR);
            adminFleetDeviationResponse.NewProcessing = Convert.ToString(adminFleetDeviationResponseModel.NewProcessing);

            return adminFleetDeviationResponse;
        }

        public async Task<AdminFleetDeviationUpdateResponse> UpdateAdminFleetDeviation(long fleetID, AdminFleetDeviationRequest adminFleetDeviationRequest)
        {
            AdminFleetDeviationRequestModel adminFleetDeviationRequestModel = new AdminFleetDeviationRequestModel();
            adminFleetDeviationRequestModel.FleetID = fleetID;
            adminFleetDeviationRequestModel.IsIRR = adminFleetDeviationRequest.IsIRR;
            adminFleetDeviationRequestModel.NewIRR = adminFleetDeviationRequest.NewIRR;
            adminFleetDeviationRequestModel.IsAIR = adminFleetDeviationRequest.IsAIR;
            adminFleetDeviationRequestModel.NewAIR = adminFleetDeviationRequest.NewAIR;
            adminFleetDeviationRequestModel.IsProcessing = adminFleetDeviationRequest.IsProcessing;
            adminFleetDeviationRequestModel.NewProcessing = adminFleetDeviationRequest.NewProcessing;

            AdminFleetDeviationResponseModel adminFleetDeviationResponseModel = await _adminRepository.UpdateAdminFleetDeviation(adminFleetDeviationRequestModel);

            AdminFleetDeviationUpdateResponse adminFleetDeviationUpdateResponse = new AdminFleetDeviationUpdateResponse();
            if (adminFleetDeviationResponseModel.FleetId == 0)
            {
                adminFleetDeviationUpdateResponse.Message = "Update Failed";
            }
            else
            {
                adminFleetDeviationUpdateResponse.Message = "Updated Successfully";
            }
            return adminFleetDeviationUpdateResponse;
        }

        public async Task<List<AdminCaseOverViewResponse>> GetAdminCaseOverViewData(long fleetId)
        {
            List<AdminCaseOverViewResponseModel> adminCaseOverViewResponseModels = await _adminRepository.GetAdminCaseOverViewData(fleetId);
            List<AdminCaseOverViewResponse> agentCaseOverViewResponses = new List<AdminCaseOverViewResponse>();
            if (adminCaseOverViewResponseModels.Count > 0)
            {
                foreach (var adminCaseOverViewResponsesList in adminCaseOverViewResponseModels)
                {
                    AdminCaseOverViewResponse adminCaseOverViewResponse = new AdminCaseOverViewResponse();
                    adminCaseOverViewResponse.IsAgreementLetterApproved = adminCaseOverViewResponsesList.IsAgreementLetterApproved;
                    adminCaseOverViewResponse.IsProvisionLetterApproved = adminCaseOverViewResponsesList.IsProvisionLetterApproved;
                    adminCaseOverViewResponse.IsSanctionLetterApproved = adminCaseOverViewResponsesList.IsSanctionLetterApproved;
                    adminCaseOverViewResponse.Comment = adminCaseOverViewResponsesList.Comment;
                    adminCaseOverViewResponse.CreatedDate = adminCaseOverViewResponsesList.CreatedDate;
                    agentCaseOverViewResponses.Add(adminCaseOverViewResponse);
                }
            }
            return agentCaseOverViewResponses;
        }

        public async Task<ApproveAdminFleetDeviationResponse> ApproveAdminFleetDeviation(ApproveAdminFleetDeviationRequest approveAdminFleetDeviationRequest)
        {
            ApproveAdminFleetDeviationRequestModel approveAdminFleetDeviationRequestModel = new ApproveAdminFleetDeviationRequestModel();
            approveAdminFleetDeviationRequestModel.VehicleId = approveAdminFleetDeviationRequest.VehicleId;
            approveAdminFleetDeviationRequestModel.Comment = approveAdminFleetDeviationRequest.Comment;

            AdminFleetResponseModel adminFleetResponseModel = await _adminRepository.ApproveAdminFleetDeviation(approveAdminFleetDeviationRequestModel);

            ApproveAdminFleetDeviationResponse approveAdminFleetDeviationResponse = new ApproveAdminFleetDeviationResponse();
            if (adminFleetResponseModel.VehicleId == 0)
            {
                approveAdminFleetDeviationResponse.Message = "Update Failed";
            }
            else
            {
                approveAdminFleetDeviationResponse.Message = "Updated Successfully";
            }
            return approveAdminFleetDeviationResponse;
        }


        public async Task<List<CustomerDataResponse>> GetCustomerData(long fleetId)
        {
            List<CustomerDataResponseModel> adminFleetResponseModelList = await _adminRepository.GetCustomerData(fleetId);
            List<CustomerDataResponse> customerDataResponses = new List<CustomerDataResponse>();

            foreach (CustomerDataResponseModel model in adminFleetResponseModelList)
            {
                CustomerDataResponse customerDataResponse = new CustomerDataResponse();
                customerDataResponse.BPNumber = model.BPNumber;
                customerDataResponse.FleetID = model.FleetID;
                customerDataResponse.FirstName = model.FirstName;
                customerDataResponse.MiddleName = model.MiddleName;
                customerDataResponse.LastName = model.LastName;
                customerDataResponse.Dob = model.Dob;
                customerDataResponse.Gender = model.Gender;
                customerDataResponse.PanNo = model.PanNo;
                customerDataResponse.FanNo = model.FanNo;
                customerDataResponse.MobileNo = model.MobileNo;
                customerDataResponses.Add(customerDataResponse);
            }

            return customerDataResponses;
        }
    }
}
