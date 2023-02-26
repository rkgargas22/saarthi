using Tmf.Saarthi.Core.RequestModels.Payment;

namespace Tmf.Saarthi.Api.Validators.Payment;

public class SavePaymentStatusValidator : AbstractValidator<SavePaymentStatusRequest>
{
    public SavePaymentStatusValidator() 
    {
        //RuleFor(x => x.Source).NotEmpty().WithMessage(ValidationMessages.PaymentSource);
        //RuleFor(x => x.BPNumber).NotEmpty().WithMessage(ValidationMessages.BpNumber);
        RuleFor(x => x.TxnID).NotEmpty().WithMessage(ValidationMessages.PaymentTxnId);
        //RuleFor(x => x.FanNo).NotEmpty().WithMessage(ValidationMessages.FanNo);
        RuleFor(x => x.TxnNo).NotEmpty().WithMessage(ValidationMessages.PaymentTxnNo);
        RuleFor(x => x.Status).NotEmpty().WithMessage(ValidationMessages.PaymentStatus);
        //RuleFor(x => x.TxnDateTime).NotEmpty().WithMessage(ValidationMessages.PaymentTxnDateTime);
    }
}
