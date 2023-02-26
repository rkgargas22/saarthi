using Tmf.Saarthi.Core.RequestModels.Nach;
using Tmf.Saarthi.Core.ResponseModels.Fleet;
using Tmf.Saarthi.Core.ResponseModels.Nach;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request.Nach;
using Tmf.Saarthi.Infrastructure.Models.Response.Nach;
using Tmf.Saarthi.Manager.Interfaces;

namespace Tmf.Saarthi.Manager.Services;

public class NachManager : INachManager
{
    private readonly INachRepository _nachRepository;
    private readonly IFleetManager _fleetManager;
    public NachManager(INachRepository nachRepository, IFleetManager fleetManager)
    {
        _nachRepository = nachRepository;
        _fleetManager = fleetManager;
    }

    public async Task<UpdateNachResponse> UpdateNach(NachRequest nachRequest)
    {
        NachRequestModel nachRequestModel = new NachRequestModel();
        nachRequestModel.FleetID = nachRequest.FleetID;
        nachRequestModel.AccountNumber = nachRequest.AccountNumber;
        nachRequestModel.ConfirmAccountNumber = nachRequest.ConfirmAccountNumber;
        nachRequestModel.AccountType = nachRequest.AccountType;
        nachRequestModel.IFSCCode = nachRequest.IFSCCode;
        nachRequestModel.BankName = nachRequest.BankName;
        nachRequestModel.AuthenticationMode = nachRequest.AuthenticationMode;
        nachRequestModel.IsNach = nachRequest.IsNach;
        nachRequestModel.CreatedBy = nachRequest.CreatedBy;
        NachResponseModel nachResponseModel = await _nachRepository.UpdateNach(nachRequestModel);
        UpdateNachResponse nachResponse = new UpdateNachResponse();
        nachResponse.FleetID = nachResponseModel.FleetID;
        return nachResponse;
    }
    public async Task<NachResponseByFleetId> GetMNachByFleetId(long FleetId)
    {
        NachResponseModelByFleetId nachResponseModel = await _nachRepository.GetNachByFleetId(FleetId ,false);

        NachResponseByFleetId nachResponse = new NachResponseByFleetId();
        nachResponse.FleetID = nachResponseModel.FleetID;
        nachResponse.AccountNumber = nachResponseModel.AccountNumber;
        nachResponse.ConfirmAccountNumber = nachResponseModel.ConfirmAccountNumber;
        nachResponse.AccountType = nachResponseModel.AccountType;
        nachResponse.IFSCCode = nachResponseModel.IFSCCode;
        nachResponse.BankName = nachResponseModel.BankName;
        nachResponse.AuthenticationMode = nachResponseModel.AuthenticationMode;
        nachResponse.Amount = nachResponseModel.Amount;
        nachResponse.StartDate = nachResponseModel.StartDate;
        nachResponse.EndDate = nachResponseModel.EndDate;
        nachResponse.Frequency = nachResponseModel.Frequency;
        nachResponse.PurposeOfManadate = nachResponseModel.PurposeOfManadate;
        nachResponse.IsEnach = nachResponseModel.IsEnach;
        nachResponse.IsActive = nachResponseModel.IsActive;
        nachResponse.CreatedBy = nachResponseModel.CreatedBy;

        return nachResponse;
    }
    public async Task<NachResponseByFleetId> GetENachByFleetId(long FleetId)
    {
        NachResponseModelByFleetId nachResponseModel = await _nachRepository.GetNachByFleetId(FleetId , true);

        NachResponseByFleetId nachResponse = new NachResponseByFleetId();
        nachResponse.FleetID = nachResponseModel.FleetID;
        nachResponse.AccountNumber = nachResponseModel.AccountNumber;
        nachResponse.ConfirmAccountNumber = nachResponseModel.ConfirmAccountNumber;
        nachResponse.AccountType = nachResponseModel.AccountType;
        nachResponse.IFSCCode = nachResponseModel.IFSCCode;
        nachResponse.BankName = nachResponseModel.BankName;
        nachResponse.AuthenticationMode = nachResponseModel.AuthenticationMode;
        nachResponse.Amount = nachResponseModel.Amount;
        nachResponse.StartDate = nachResponseModel.StartDate;
        nachResponse.EndDate = nachResponseModel.EndDate;
        nachResponse.Frequency = nachResponseModel.Frequency;
        nachResponse.PurposeOfManadate = nachResponseModel.PurposeOfManadate;
        nachResponse.IsEnach = nachResponseModel.IsEnach;
        nachResponse.IsActive = nachResponseModel.IsActive;
        nachResponse.CreatedBy = nachResponseModel.CreatedBy;

        return nachResponse;
    }

    public async Task<List<DropResponse>> GetBank()
    {
        List<DropdownResponseModel> bankResponseModel = await _nachRepository.GetBank();
        List<DropResponse> bankResponses = new List<DropResponse>();

        foreach (DropdownResponseModel model in bankResponseModel)
        {
            DropResponse bankResponse = new DropResponse();
            bankResponse.Id = model.Id;
            bankResponse.DisplayName = model.DisplayName;
            bankResponses.Add(bankResponse);
        }

        return bankResponses;
    }
    public async Task<List<DropResponse>> GetState(int bankId)
    {
        List<DropdownResponseModel> stateResponseModel = await _nachRepository.GetState(bankId);
        List<DropResponse> stateResponses = new List<DropResponse>();

        foreach (DropdownResponseModel model in stateResponseModel)
        {
            DropResponse stateResponse = new DropResponse();
            stateResponse.Id = model.Id;
            stateResponse.DisplayName = model.DisplayName;
            stateResponses.Add(stateResponse);
        }

        return stateResponses;
    }
    public async Task<List<DropResponse>> GetCity(int stateId)
    {
        List<DropdownResponseModel> cityResponseModel = await _nachRepository.GetCity(stateId);
        List<DropResponse> cityResponses = new List<DropResponse>();

        foreach (DropdownResponseModel model in cityResponseModel)
        {
            DropResponse stateResponse = new DropResponse();
            stateResponse.Id = model.Id;
            stateResponse.DisplayName = model.DisplayName;
            cityResponses.Add(stateResponse);
        }

        return cityResponses;
    }
    public async Task<List<DropResponse>> GetBranch(int BankId, int StateId, int CityId)
    {
        List<DropdownResponseModel> branchResponseModel = await _nachRepository.GetBranch(BankId, StateId, CityId);
        List<DropResponse> branchResponses = new List<DropResponse>();

        foreach (DropdownResponseModel model in branchResponseModel)
        {
            DropResponse branchResponse = new DropResponse();
            branchResponse.Id = model.Id;
            branchResponse.DisplayName = model.DisplayName;
            branchResponses.Add(branchResponse);
        }

        return branchResponses;
    }

    public async Task<NachResponseIFSC> GetIFSCCode(int BankId, int StateId, int CityId, int BranchId)
    {
        NachResponseModelIFSC nachResponseModel = await _nachRepository.GetIFSCCode(BankId, StateId, CityId, BranchId);

        NachResponseIFSC nachResponse = new NachResponseIFSC();
        nachResponse.IFSCCode = nachResponseModel.IFSCCode;

        return nachResponse;
    }

    public async Task<NachResponse> AddNach(AddNachRequest nachRequest)
    {
        AddNachRequestModel nachRequestModel = new AddNachRequestModel();
        VerifyFleetResponse verifyFleetResponse = await _fleetManager.GetFleetByFleetId(nachRequest.FleetID);
        nachRequestModel.FleetID = nachRequest.FleetID;
        nachRequestModel.Amount = verifyFleetResponse.LoanAmount ?? 0;
        nachRequestModel.StartDate = DateTime.Now;
        nachRequestModel.EndDate = nachRequestModel.StartDate.AddYears(30);
        nachRequestModel.Frequency = "Monthly";
        nachRequestModel.PurposeOfManadate = "L001 - Finance";
        nachRequestModel.IsEnach = nachRequest.IsEnach;
        nachRequestModel.IsActive = true;
        NachResponseModel nachResponseModel = await _nachRepository.AddNach(nachRequestModel);
        NachResponse nachResponse = new NachResponse();
        nachResponse.FleetID = nachResponseModel.FleetID;
        nachResponse.Amount = nachRequestModel.Amount;
        nachResponse.StartDate = nachRequestModel.StartDate;
        nachResponse.EndDate = nachRequestModel.EndDate;
        nachResponse.Frequency = nachRequestModel.Frequency;
        nachResponse.PurposeOfManadate = nachRequestModel.PurposeOfManadate;
        nachResponse.IsEnach = nachRequestModel.IsEnach;
        return nachResponse;
    }
    public async Task<UpdateNachResponse> UpdateNachStatus(UpdateNachStatusRequest updateNachStatusRequest)
    {
        UpdateNachStatusRequestModel nachRequestModel = new UpdateNachStatusRequestModel();
        nachRequestModel.FleetID = updateNachStatusRequest.FleetID;
        nachRequestModel.Status = updateNachStatusRequest.Status;
        nachRequestModel.IsNach = updateNachStatusRequest.IsNach;
        NachResponseModel nachResponseModel = await _nachRepository.UpdateNachStatus(nachRequestModel);
        UpdateNachResponse nachResponse = new UpdateNachResponse();
        nachResponse.FleetID = nachResponseModel.FleetID;
        return nachResponse;
    }
    public async Task<UpdateNachResponse> UpdateTimeSlotStatus(UpdateNachTimeSlotRequest updateNachTimeSlotRequest)
    {
        UpdateNachTimeSlotRequestModel nachRequestModel = new UpdateNachTimeSlotRequestModel();
        nachRequestModel.FleetID = updateNachTimeSlotRequest.FleetID;
        nachRequestModel.IsNach = updateNachTimeSlotRequest.IsNach;
        nachRequestModel.TimeSlot = updateNachTimeSlotRequest.TimeSlot;
        NachResponseModel nachResponseModel = await _nachRepository.UpdateTimeSlotStatus(nachRequestModel);
        UpdateNachResponse nachResponse = new UpdateNachResponse();
        nachResponse.FleetID = nachResponseModel.FleetID;
        return nachResponse;
    }
    public async Task<NachStatusAndTimeslotResponse> GetTimeSlotAndStatusDate(long FleetId, bool IsEnach)
    {
        NachStatusAndTimeslotResponseModel nachStatusAndTimeslotResponse = await _nachRepository.GetTimeSlotAndStatusDate(FleetId, IsEnach);

        NachStatusAndTimeslotResponse nachStatusAndTimeslot = new NachStatusAndTimeslotResponse();
        nachStatusAndTimeslot.FleetID = nachStatusAndTimeslotResponse.FleetID;
        nachStatusAndTimeslot.Status = nachStatusAndTimeslotResponse.Status;
        nachStatusAndTimeslot.TimeSlotDate = nachStatusAndTimeslotResponse.TimeSlotDate;

        return nachStatusAndTimeslot;
    }
}
