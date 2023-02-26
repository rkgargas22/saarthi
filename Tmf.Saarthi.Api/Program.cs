using Microsoft.OpenApi.Models;
using Tmf.Logs;
using Tmf.Saarthi.Api.Validators.Agent;
using Tmf.Saarthi.Api.Validators.Customer;
using Tmf.Saarthi.Api.Validators.Document;
using Tmf.Saarthi.Api.Validators.Email;
using Tmf.Saarthi.Api.Validators.Fleet;
using Tmf.Saarthi.Api.Validators.Nach;
using Tmf.Saarthi.Api.Validators.Ocr;
using Tmf.Saarthi.Api.Validators.Payment;
using Tmf.Saarthi.Core.RequestModels.Agent;
using Tmf.Saarthi.Core.RequestModels.Customer;
using Tmf.Saarthi.Core.RequestModels.Document;
using Tmf.Saarthi.Core.RequestModels.Email;
using Tmf.Saarthi.Core.RequestModels.Fleet;
using Tmf.Saarthi.Core.RequestModels.Login;
using Tmf.Saarthi.Core.RequestModels.Nach;
using Tmf.Saarthi.Core.RequestModels.Ocr;
using Tmf.Saarthi.Core.RequestModels.Payment;
namespace Tmf.Saarthi.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();

        builder.Services.AddHttpClient();
        builder.Services.AddCors();
        #region Options        
        builder.Services.Configure<OtpServiceOptions>(builder.Configuration.GetSection(OtpServiceOptions.OtpService));
        builder.Services.Configure<LoginOptions>(builder.Configuration.GetSection(LoginOptions.Login));
        builder.Services.Configure<ConnectionStringsOptions>(builder.Configuration.GetSection(ConnectionStringsOptions.ConnectionStrings));
        builder.Services.Configure<InstaVeritaOptions>(builder.Configuration.GetSection(InstaVeritaOptions.InstaVerita));
        builder.Services.Configure<LetterOptions>(builder.Configuration.GetSection(LetterOptions.Letter));
        builder.Services.Configure<FleetConfigurationOptions>(builder.Configuration.GetSection(FleetConfigurationOptions.FleetConfiguration));
        builder.Services.Configure<PaymentOptions>(builder.Configuration.GetSection(PaymentOptions.Payment));
        builder.Services.Configure<DMSOptions>(builder.Configuration.GetSection(DMSOptions.DMS));
        builder.Services.Configure<OcrOptions>(builder.Configuration.GetSection(OcrOptions.OCR));
        builder.Services.Configure<EmailOptions>(builder.Configuration.GetSection(EmailOptions.Email));
        #endregion
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
       
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Saarthi API",
                Description = "Saarthi web api"
                //TermsOfService = new Uri("https://example.com/terms"),
                //Contact = new OpenApiContact
                //{
                //    Name = "Example Contact",
                //    Url = new Uri("https://example.com/contact")
                //},
                //License = new OpenApiLicense
                //{
                //    Name = "Example License",
                //    Url = new Uri("https://example.com/license")
                //}
            });
        });

        #region Http Service
        builder.Services.AddScoped<IHttpService, HttpService>();
        #endregion

        #region Sql Service
        builder.Services.AddScoped<ISqlUtility, SqlUtility>();
        #endregion

        #region Manager
        builder.Services.AddScoped<ILoginManager, LoginManager>();
        builder.Services.AddScoped<IOtpManager, OtpManager>();
        builder.Services.AddScoped<IFleetManager, FleetManager>();
        builder.Services.AddScoped<IFleetVehicleManager, FleetVehicleManager>();
        builder.Services.AddScoped<ICustomerPreApprovedManager, CustomerPreApprovedManager>();
        builder.Services.AddScoped<ICustomerManager, CustomerManager>();
        builder.Services.AddScoped<ICustomerConsentManager, CustomerConsentManager>();
        builder.Services.AddScoped<ISanctionLetterManager, SantionLetterManager>();
        builder.Services.AddScoped<IFuelLoanAggrementManager, FuelLoanAggrementManager>();
        builder.Services.AddScoped<IPaymentManager, PaymentManager>();
        builder.Services.AddScoped<IUploadManager, UploadManager>();
        builder.Services.AddScoped<IProvisionalLetterManager, ProvisionalLetterManager>();
        builder.Services.AddScoped<IDMSManager, DMSManager>();
        builder.Services.AddScoped<INachManager, NachManager>();
        builder.Services.AddScoped<IAgentManager, AgentManager>();
        builder.Services.AddScoped<IOcrManager, OcrManager>();
        builder.Services.AddScoped<IAdminManager, AdminManager>();
        builder.Services.AddScoped<IEmailManager, EmailManager>();
        builder.Services.AddScoped<ICreditManager, CreditManager>();
        #endregion

        #region Repository
        builder.Services.AddScoped<ILoginRepository, LoginRepository>();
        builder.Services.AddScoped<IOtpRepository, OtpRepository>();
        builder.Services.AddScoped<IFleetRepository, FleetRepository>();
        builder.Services.AddScoped<IFleetVehicleRepository, FleetVehicleRepository>();
        builder.Services.AddScoped<ICustomerPreApprovedRepository, CustomerPreApprovedRepository>();
        builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
        builder.Services.AddScoped<ICustomerAddressRepository, CustomerAddressRepository>();
        builder.Services.AddScoped<ICustomerConsentRepository, CustomerConsentRepository>();
        builder.Services.AddScoped<ISantionLetterRepository, SantionLetterRepository>();
        builder.Services.AddScoped<IFuelLoanAggrementRepository, FuelLoanAggrementRepository>();
        builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
        builder.Services.AddScoped<IUploadDocumentRepository, DocumentUploadRepository>();
        builder.Services.AddScoped<IProvisionalLetterRepository, ProvisionalLetterRepository>();
        builder.Services.AddScoped<IDMSRepository, DMSRepository>();
        builder.Services.AddScoped<INachRepository, NachRepository>();
        builder.Services.AddScoped<IAgentRepository, AgentRepository>();
        builder.Services.AddScoped<IOcrRepository, OcrRepository>();
        builder.Services.AddScoped<IAdminRepository, AdminRepository>();
        builder.Services.AddScoped<IEmailRepository, EmailRepository>();
        builder.Services.AddScoped<ICreditRepository, CreditRepository>();
        #endregion

        #region Validators
        builder.Services.AddScoped<IValidator<OtpRequest>, OtpRequestValidator>();
        builder.Services.AddScoped<IValidator<VerifyOtpRequest>, VerifyOtpRequestValidator>();
        builder.Services.AddScoped<IValidator<AddFleetVehicleRequest>, AddFleetVehicleValidator>();
        builder.Services.AddScoped<IValidator<BulkAddFleetVehicleRequest>, BulkAddFleetVehicleRequestValidator>();
        builder.Services.AddScoped<IValidator<LoginRequest>, LoginRequestValidator>();
        builder.Services.AddScoped<IValidator<UpdateFleetVehicleRCRequest>, UpdateFleetVehicleRCValidator>();
        builder.Services.AddScoped<IValidator<SavePaymentStatusRequest>, SavePaymentStatusValidator>();
        builder.Services.AddScoped<IValidator<CustomerAddressRequest>, CustomerAddressRequestValidator>();
        builder.Services.AddScoped<IValidator<DocumentRequest>, DocumentUploadValidator>();
        builder.Services.AddScoped<IValidator<ProvisionApprovalRequest>, ProvisionApprovalValidator>();
        builder.Services.AddScoped<IValidator<SanctionApprovalRequest>, SanctionApprovalValidator>();
        builder.Services.AddScoped<IValidator<EAgreementApprovalRequest>, EAgreementApprovalValidator>();
        builder.Services.AddScoped<IValidator<NachRequest>, AddNachValidator>();
        builder.Services.AddScoped<IValidator<AddressDetailsRequest>, GetAddressDetailsValidator>();
        builder.Services.AddScoped<IValidator<AgentSalesDeviationRequest>, UpdateSalesDeviationValidator>();
        builder.Services.AddScoped<IValidator<SendEmailRequest>, SendEmailValidator>();
        builder.Services.AddScoped<IValidator<UpdateNachStatusRequest>, UpdateNachStatusValidator>();
        builder.Services.AddScoped<IValidator<UpdateNachTimeSlotRequest>, UpdateTimeSlotStatusValidator>();
        builder.Services.AddScoped<IValidator<CommentRequest>, CommentValidator>();
        builder.Services.AddScoped<IValidator<AdditionalInformationRequest>, AdditionalInformationValidator>();
        builder.Services.AddScoped<IValidator<AddressChangeRequest>, AddressChangeValidator>();
        builder.Services.AddScoped<IValidator<AssignFleetRequest>, AssignFleetValidator>();
        #endregion

        builder.Services.AddSingleton<ILog, Log>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
        {
            app.UseSwagger();
            app.UseSwaggerUI();           
        }

        app.UseCors(builder =>
        {
            builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        });
        app.UseHttpsRedirection(); 
        app.UseMiddleware<GlobalErrorHandlingMiddleware>();
        app.UseMiddleware<RequestResponseLoggingMiddleware>();

        app.UseAuthorization();

        app.MapControllers();

        //app.MapWhen(context => context.Request.Path == "/api/Payment/SavePaymentStatus", m => {
        //    m.UseMiddleware<RequestResponseLoggingMiddleware>();
        //});

        app.Run();
    }
}