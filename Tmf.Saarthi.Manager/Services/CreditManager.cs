using Tmf.Saarthi.Core.RequestModels.Credit;
using Tmf.Saarthi.Core.ResponseModels.Credit;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request.Credit;
using Tmf.Saarthi.Infrastructure.Models.Response.Credit;
using Tmf.Saarthi.Manager.Interfaces;

namespace Tmf.Saarthi.Manager.Services
{
    public class CreditManager : ICreditManager
    {
        private readonly ICreditRepository _creditRepository;
        public CreditManager(ICreditRepository creditRepository)
        {
            _creditRepository = creditRepository;
        }
        public async Task<List<CreditDashboardResponse>> GetCreditDashboard()
        {
            List<CreditDashboardResponseModel> creditDashboardResponseModelList = await _creditRepository.GetCreditDashboard();
            List<CreditDashboardResponse> creditDashboardResponses = new List<CreditDashboardResponse>();

            foreach (CreditDashboardResponseModel model in creditDashboardResponseModelList)
            {
                CreditDashboardResponse creditDashboardResponse = new CreditDashboardResponse();
                creditDashboardResponse.ApplicationId = model.ApplicationId;
                creditDashboardResponse.CustomerName = model.CustomerName;
                creditDashboardResponse.AssingedDate = model.AssingedDate;
                creditDashboardResponse.ExprDate = model.ExprDate;
                creditDashboardResponse.Status = model.Status;
                creditDashboardResponses.Add(creditDashboardResponse);
            }

            return creditDashboardResponses;
        }
        public async Task<FiDetailResponse> GetFiDetail(long FleetId)
        {
            FiDetailResponseModel fiDetailResponseModelList = await _creditRepository.GetFiDetail(FleetId);

            FiDetailResponse fiDetailResponse = new FiDetailResponse();
            fiDetailResponse.FleetID = fiDetailResponseModelList.FleetID;
            fiDetailResponse.VerificationDate = fiDetailResponseModelList.VerificationDate;
            fiDetailResponse.FiStatus = fiDetailResponseModelList.FiStatus;
            fiDetailResponse.CPCStatus = fiDetailResponseModelList.CPCStatus;
            fiDetailResponse.fiDeviations = fiDetailResponseModelList.fiDeviations;

            return fiDetailResponse;
        }

        public async Task<UpdateFiDetailResponse> UpdateFiDetail(long FleetID, UpdateFiDetailRequest updateFiDetailRequest)
        {
            UpdateFiDetailRequestModel updateFiDetailRequestModel = new UpdateFiDetailRequestModel();
            updateFiDetailRequestModel.FleetID = FleetID;
            updateFiDetailRequestModel.Status = updateFiDetailRequest.Status;
            updateFiDetailRequestModel.Comment = updateFiDetailRequest.Comment;

            FiDetailResponseModel fiDetailResponseModel = await _creditRepository.UpdateFiDetail(updateFiDetailRequestModel);

            UpdateFiDetailResponse updateFiDetailResponse = new UpdateFiDetailResponse();
            if (fiDetailResponseModel.FleetID == 0)
            {
                updateFiDetailResponse.Message = "Update Failed";
            }
            else
            {
                updateFiDetailResponse.Message = "Updated Successfully";
            }
            return updateFiDetailResponse;
        }


        public async Task<FiRetriggerResponse> FIRetrigger(FiRetriggerRequest fiRetriggerRequest)
        {
            FiRetriggerRequestModel fiRetriggerRequestModel = new FiRetriggerRequestModel();
            fiRetriggerRequestModel.fleetId = fiRetriggerRequest.fleetId;
            fiRetriggerRequestModel.UserId = fiRetriggerRequest.UserId;

            FiDetailResponseModel fiDetailResponseModel = await _creditRepository.FIRetrigger(fiRetriggerRequestModel);

            FiRetriggerResponse fiRetriggerResponse = new FiRetriggerResponse();
            if (fiDetailResponseModel.FleetID == 0)
            {
                fiRetriggerResponse.Message = "FI Retrigger Failed, No Fleet Found.";
            }
            else
            {
                fiRetriggerResponse.Message = "FI Retriggered Successfully";
            }
            return fiRetriggerResponse;
        }
    }
}
