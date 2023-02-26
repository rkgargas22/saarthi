using Tmf.Saarthi.Core.RequestModels.Credit;
using Tmf.Saarthi.Core.ResponseModels.Credit;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request.Credit;
using Tmf.Saarthi.Infrastructure.Models.Response.Credit;
using Tmf.Saarthi.Manager.Interfaces;

namespace Tmf.Saarthi.Manager.Services
{
    public class CreditManager: ICreditManager
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
        public async Task<List<FiDetailResponse>> GetFiDetail(long FleetId)
        {
            List<FiDetailResponseModel> fiDetailResponseModelList = await _creditRepository.GetFiDetail(FleetId);
            List<FiDetailResponse> creditDashboardResponses = new List<FiDetailResponse>();

            foreach (FiDetailResponseModel model in fiDetailResponseModelList)
            {
                FiDetailResponse creditDashboardResponse = new FiDetailResponse();
                creditDashboardResponse.FleetID = model.FleetID;
                creditDashboardResponse.VerificationDate = model.VerificationDate;
                creditDashboardResponse.FiStatus = model.FiStatus;
                creditDashboardResponse.CPCStatus = model.CPCStatus;
                creditDashboardResponse.FiDeviation = model.FiDeviation;
                creditDashboardResponses.Add(creditDashboardResponse);
            }

            return creditDashboardResponses;
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
    }
}
