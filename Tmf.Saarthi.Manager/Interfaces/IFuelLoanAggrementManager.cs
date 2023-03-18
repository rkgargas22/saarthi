using Tmf.Saarthi.Core.ResponseModels.FuelLoanAgreement;

namespace Tmf.Saarthi.Manager.Interfaces;

public interface IFuelLoanAggrementManager
{
    Task<FuelLoanAgreementResponse> GenerateFuelLoanAgreement(long FleetId, long CreatedBy);
}
