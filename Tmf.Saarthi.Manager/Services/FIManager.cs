using Microsoft.Extensions.Options;
using Tmf.Saarthi.Core.Options;
using Tmf.Saarthi.Core.ResponseModels.FI;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request.FI;
using Tmf.Saarthi.Infrastructure.Models.Response.FI;
using Tmf.Saarthi.Manager.Interfaces;

namespace Tmf.Saarthi.Manager.Services;

public class FIManager : IFIManager
{
    private readonly IFIRepository _fiRepository;
    private readonly FIRossOptions _fiRossOptions;

    public FIManager(IFIRepository fiRepository, IOptions<FIRossOptions> fiRossOptions)
    {
        _fiRepository = fiRepository;
        _fiRossOptions = fiRossOptions.Value;
    }

    public async Task<InitiateFIResponse> InitiateFI(long fleetId, long queueId, long createdBy)
    {
        InitiateFIDataDetailsResponseModel initiateFIDataDetails = await _fiRepository.GetFIRossInitiateData(fleetId);

        FIAllocationDetail fIAllocationDetail = DeepCopy<InitiateFIDataDetailsResponseModel, FIAllocationDetail>(initiateFIDataDetails);

        ApplicantDetail applicantDetail = DeepCopy<InitiateFIDataDetailsResponseModel, ApplicantDetail>(initiateFIDataDetails);

        AddressDataDetails addressDataDetails = new()
        {
            OfficeAddress = DeepCopy<InitiateFIDataDetailsResponseModel, Address>(initiateFIDataDetails, "Office"),
            ResidentAddress = DeepCopy<InitiateFIDataDetailsResponseModel, Address>(initiateFIDataDetails, "Permanent"),
            PermanentAddress = DeepCopy<InitiateFIDataDetailsResponseModel, Address>(initiateFIDataDetails, "Resident")
        };

        applicantDetail.AddressDataDetails = addressDataDetails;

        DataDetails dataDetails = DeepCopy<InitiateFIDataDetailsResponseModel, DataDetails>(initiateFIDataDetails);

        dataDetails.ApplicantDetails = new List<ApplicantDetail> { applicantDetail };

        FIRossRequest fIRossRequest = new()
        {
            DataDetails = dataDetails,
            FIAllocationDetails = new List<FIAllocationDetail> { fIAllocationDetail }
        };

        UserContext userContext = new()
        {
            UserID = _fiRossOptions.UserId,
            Password = _fiRossOptions.Password
        };

        InitiateFIRequestModel initiateFIRequestModel = new()
        {
            FIRossRequest = fIRossRequest,
            Source = "SAP",
            RequestID = new Random().Next(),
            UserContext = userContext
        };

        InitiateFIResponseModel initiateFIResponseModel = await _fiRepository.InitiateFI(initiateFIRequestModel);

        UpdateInitiateFIRequestModel requestModel = DeepCopy<ResponseData, UpdateInitiateFIRequestModel>(initiateFIResponseModel.ResponseData);
        requestModel.FleetId = fleetId;
        requestModel.QueueId = queueId;
        requestModel.CreatedBy = createdBy;

        await _fiRepository.UpdateInitiateFIResponse(requestModel);

        InitiateFIResponse response = DeepCopy<ResponseData, InitiateFIResponse>(initiateFIResponseModel.ResponseData);

        return response;
    }

    private static TOut DeepCopy<TIn, TOut>(TIn sourceDataObject, string? prefix = null)
    {
        var properties = typeof(TOut).GetProperties();
        var typeOfFI = typeof(TIn);

        var objT = Activator.CreateInstance<TOut>();
        foreach (var pro in properties)
        {
            try
            {
                var propertyOfFI = prefix == null ? typeOfFI.GetProperty(pro.Name)! : typeOfFI.GetProperty($"{prefix}{pro.Name}")!;
                if (propertyOfFI != null)
                {
                    pro.SetValue(objT, propertyOfFI.GetValue(sourceDataObject));
                }
            }
            catch
            {
                throw;
            }
        }
        return objT;
    }
}
