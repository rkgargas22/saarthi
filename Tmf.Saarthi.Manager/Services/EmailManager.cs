using Tmf.Saarthi.Core.RequestModels.Email;
using Tmf.Saarthi.Core.ResponseModels.Customer;
using Tmf.Saarthi.Core.ResponseModels.Email;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request.Email;
using Tmf.Saarthi.Infrastructure.Models.Response.Email;
using Tmf.Saarthi.Manager.Interfaces;

namespace Tmf.Saarthi.Manager.Services;

public class EmailManager : IEmailManager
{
    private readonly IEmailRepository _emailRepository;
    private readonly ICustomerManager _customerManager;

    public EmailManager(IEmailRepository emailRepository, ICustomerManager customerManager)
    {
        _emailRepository = emailRepository;
        _customerManager = customerManager;
    }

    public async Task<SendEmailResponse> SendEmail(SendEmailRequest sendEmailRequest)
    {
        SendEmailResponse sendEmailResponse = new SendEmailResponse();

        CustomerResponse customerResponse = await _customerManager.GetCustomerByBPNumber(sendEmailRequest.BPNumber);
        if(customerResponse != null && !string.IsNullOrEmpty(customerResponse.EmailID) && !string.IsNullOrEmpty(customerResponse.MobileNo)) 
        {
            TemplateDataRequest templateDataRequest = new TemplateDataRequest();
            templateDataRequest.Module = sendEmailRequest.Module;
            templateDataRequest.SubModule = sendEmailRequest.SubModule;
            templateDataRequest.TemplateType = sendEmailRequest.TemplateType;

            TemplateDataResponse templateDataResponse = await _emailRepository.GetTemplateData(templateDataRequest);

            if (templateDataResponse.TemplateType == "EMAIL")
            {
                if (templateDataResponse != null && templateDataResponse.TemplateId != 0)
                {
                    SendEmailRequestModel sendEmailRequestModel = new SendEmailRequestModel();
                    sendEmailRequestModel.Subject = templateDataResponse.Subject;
                    sendEmailRequestModel.ToEmail = customerResponse.EmailID;
                    sendEmailRequestModel.Body = templateDataResponse.Body;

                    //if (templateDataResponse.Module == "Payment")
                    //{
                    //    sendEmailRequestModel.Body = templateDataResponse.Body.Replace("{{url}}", templateDataResponse.Url);
                    //}
                    sendEmailRequestModel.Body = templateDataResponse.Body.Replace("{{url}}", templateDataResponse.Url);

                    SendEmailResponseModel sendEmailResponseModel = await _emailRepository.SendEmail(sendEmailRequestModel);

                    if (sendEmailResponseModel != null && !string.IsNullOrEmpty(sendEmailResponseModel.Message))
                    {
                        sendEmailResponse.Message = sendEmailResponseModel.Message;
                    }
                }
            }
        }

        

        return sendEmailResponse;
    }
}
