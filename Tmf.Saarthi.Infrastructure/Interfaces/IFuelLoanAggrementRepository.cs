using Tmf.Saarthi.Infrastructure.Models.Request.FuelLoanAggrement;
using Tmf.Saarthi.Infrastructure.Models.Response.FuelLoanAggrement;

namespace Tmf.Saarthi.Infrastructure.Interfaces;

public interface IFuelLoanAggrementRepository
{
    Task<FuelLoanAggrementResponseModel> GenerateFuelLoanAgreement(FuelLoanAggrementRequestModel fuelLoanAggrementRequestModel);
}
