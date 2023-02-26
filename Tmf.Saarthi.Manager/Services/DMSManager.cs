using Tmf.Saarthi.Core.RequestModels.DMS;
using Tmf.Saarthi.Core.ResponseModels.DMS;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request.DMS;
using Tmf.Saarthi.Infrastructure.Models.Response.DMS;
using Tmf.Saarthi.Manager.Interfaces;

namespace Tmf.Saarthi.Manager.Services;

public class DMSManager : IDMSManager
{
    private readonly IDMSRepository _dMSRepository;

    public DMSManager(IDMSRepository dMSRepository)
    {
        _dMSRepository = dMSRepository;
    }

    public async Task<GenerateFanNoResponse> GenerateFanNo(GenerateFanNoRequest generateFanNoRequest)
    {
        GenerateFanNoRequestModel generateFanNoRequestModel = new GenerateFanNoRequestModel();
        generateFanNoRequestModel.BranchCode = generateFanNoRequest.BranchCode;
        generateFanNoRequestModel.ProcessType = generateFanNoRequest.ProcessType;
        generateFanNoRequestModel.SchemeName = generateFanNoRequest.SchemeName;
        generateFanNoRequestModel.LoanType = generateFanNoRequest.LoanType;
        generateFanNoRequestModel.ApplicantName = generateFanNoRequest.ApplicantName;
        generateFanNoRequestModel.BdmName = generateFanNoRequest.BdmName;
        generateFanNoRequestModel.DsaName = generateFanNoRequest.DsaName;
        generateFanNoRequestModel.DealerName = generateFanNoRequest.DealerName;
        generateFanNoRequestModel.DealerCode = generateFanNoRequest.DealerCode;

        GenerateFanNoResponseModel generateFanNoResponseModel = await _dMSRepository.GenerateFanNo(generateFanNoRequestModel);

        GenerateFanNoResponse generateFanNoResponse = new GenerateFanNoResponse();
        generateFanNoResponse.FanNo = generateFanNoResponseModel.Fanno;

        return generateFanNoResponse;
    }
}
