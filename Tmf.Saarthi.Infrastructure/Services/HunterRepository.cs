using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using System.Text.Json;
using Tmf.Saarthi.Core.Options;
using Tmf.Saarthi.Core.RequestModels.Hunter;
using Tmf.Saarthi.Core.ResponseModels.Hunter;
using Tmf.Saarthi.Infrastructure.HttpService;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.SqlService;

namespace Tmf.Saarthi.Infrastructure.Services
{
    public class HunterRepository : IHunterRepository
    {

        private readonly IHttpService _httpService;
        private readonly ISqlUtility _sqlUtility;
        private readonly HunterOptions _hunterOptions;
        private readonly ConnectionStringsOptions _connectionStringsOptions;

        public HunterRepository(ISqlUtility sqlUtility, IHttpService httpService, IOptions<HunterOptions> hunterOptions, IOptions<ConnectionStringsOptions> connectionStringsOptions)
        {
            _httpService = httpService;
            _hunterOptions = hunterOptions.Value;
            _connectionStringsOptions = connectionStringsOptions.Value;
            _sqlUtility = sqlUtility;
        }

        public async Task<HunterResponseModel> HunterVerification(HunterRequestModel requestModel)
        {
           
            var jsonSerializerOptions = new JsonSerializerOptions() { WriteIndented = true };
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("BpNo", "1");
            headers.Add("UserType", "User");
            var t = JsonSerializer.Serialize(requestModel);
            JsonDocument result = await _httpService.PostAsync(_hunterOptions.BaseUrl+ _hunterOptions.ValidateCustomer, requestModel, headers);
            HunterResponseModel hunterResponseModel = JsonSerializer.Deserialize<HunterResponseModel>(result, jsonSerializerOptions) ?? throw new ArgumentNullException();

            return hunterResponseModel;
        }


        public async Task<bool> HunterRequest()
        {    
            DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_getAllHunterPendinginfo");

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    List<SqlParameter> parameters = new List<SqlParameter>()
                    {
                        new SqlParameter("BPnumber", Convert.ToInt32( dt.Rows[i]["BpNumber"])),
                        new SqlParameter("FleetId", Convert.ToInt32( dt.Rows[i]["FleetId"])),
                    };

                    DataTable dt1 = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_getHunterRequestParms", parameters);

                    HunterRequestModel requestModel = HunterRequestMapper(dt1, i);

                    HunterResponseModel hunterResponseModel = await HunterVerification(requestModel);

                    await AddHunterResponse(hunterResponseModel, Convert.ToInt32(dt.Rows[i]["BpNumber"]), Convert.ToInt32(dt.Rows[i]["FleetId"]));
                }
            }
            return true;
            
        }

        private async Task<bool> AddHunterResponse(HunterResponseModel addHunterResponseModel,int BPnumber,int fleetId)
        {
            bool flag = false;
             
            if (addHunterResponseModel.ResponseHeader != null)
            {
                var RecommendedNextActions = addHunterResponseModel.ResponseHeader.OverallResponse.RecommendedNextActions.Count!=0 ? addHunterResponseModel.ResponseHeader.OverallResponse.RecommendedNextActions[0] : "";
                var SpareObjects = addHunterResponseModel.ResponseHeader.OverallResponse.SpareObjects.Count != 0 ? addHunterResponseModel.ResponseHeader.OverallResponse.SpareObjects[0] : "";

                var DecisionReasons = addHunterResponseModel.ResponseHeader.OverallResponse.DecisionReasons[0];

                List<SqlParameter> parameters = new List<SqlParameter>()
                            {
                    
                                new SqlParameter("ClientReferenceId",addHunterResponseModel.ResponseHeader.ClientReferenceId),
                                new SqlParameter("RequestType",addHunterResponseModel.ResponseHeader.RequestType),
                                new SqlParameter("ExpRequestId",addHunterResponseModel.ResponseHeader.ExpRequestId),
                                new SqlParameter("messageTime",addHunterResponseModel.ResponseHeader.MessageTime),
                                new SqlParameter("Decision",addHunterResponseModel.ResponseHeader.OverallResponse.Decision),
                                new SqlParameter("DecisionReasons",DecisionReasons),
                                new SqlParameter("recommendedNextActions",RecommendedNextActions),
                                new SqlParameter("spareObjects",SpareObjects),
                                new SqlParameter("DecisionText",addHunterResponseModel.ResponseHeader.OverallResponse.DecisionText),
                                new SqlParameter("ResponseCode",addHunterResponseModel.ResponseHeader.ResponseCode),
                                new SqlParameter("ResponseType",addHunterResponseModel.ResponseHeader.ResponseType),
                                new SqlParameter("ResponseMessage",addHunterResponseModel.ResponseHeader.ResponseMessage),
                                new SqlParameter("TenantID",addHunterResponseModel.ResponseHeader.TenantID),
                                new SqlParameter("Score",addHunterResponseModel.ResponseHeader.OverallResponse.Score),
                                new SqlParameter("BPnumber",BPnumber),
                                new SqlParameter("fleetId",fleetId),
                            };

                    await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_addHunterResponse", parameters);
                flag = true;
            }
            return flag;
        }


        private static HunterRequestModel HunterRequestMapper(DataTable dt, int i)
        {
            HunterRequestModel requestModel = new HunterRequestModel();

            //************Header Request************//
            Header header = new Header();
            header.MessageTime =  string.Concat(DateTime.UtcNow.ToString("s"), "Z");
            header.ClientReferenceId = dt.Rows[i]["ClientReferenceId"].ToString();
            header.TenantId = dt.Rows[i]["TenantId"].ToString();
            header.RequestType = dt.Rows[i]["RequestType"].ToString();
            header.Options =new Core.RequestModels.Hunter.Options();


            //************Payload***Telephone Number Request************//

            Telephone telephone = new Telephone();
            List<Telephone> telephoneList = new List<Telephone>();
            telephone.Number = dt.Rows[i]["MobileNo"].ToString();
            telephone.Type = dt.Rows[i]["TypeOfphone"].ToString();
            telephone.Id = dt.Rows[i]["TelId"].ToString();
            telephoneList.Add(telephone);

            //****IdentityDocument Request******//
            IdentityDocument identityDocument = new IdentityDocument();
            identityDocument.DocumentNumber = (string)dt.Rows[i]["PanNo"];
            identityDocument.DocumentType = (string)dt.Rows[i]["DocumentTypePan"];
            identityDocument.Id = (string)dt.Rows[i]["DocumentId"];
            List<IdentityDocument> identityDocumentList = new List<IdentityDocument>();
            identityDocumentList.Add(identityDocument);

           


             //*********Application  start*********//
            
            //****ProductDetails Request******//
            ProductDetails productDetails = new ProductDetails();
            productDetails.ProductCode = dt.Rows[i]["ProductCode"].ToString();
            productDetails.ProductType = dt.Rows[i]["ProductType"].ToString();


            //****Applicant*****//
            Applicant applicant = new Applicant();
            applicant.ApplicantType = dt.Rows[i]["ApplicantType"].ToString();
            applicant.Id = dt.Rows[i]["applicantid"].ToString();
            applicant.ContactId = dt.Rows[i]["applicantContctId"].ToString();

            List<Applicant> applicantsLit= new List<Applicant>();
            applicantsLit.Add(applicant);

           
            Application application = new Application();
            application.ProductDetails = productDetails;
            application.Applicants = applicantsLit;
            application.OriginalRequestTime =  string.Concat(DateTime.UtcNow.ToString("s"), "Z"); 
            application.NotificationRequired =Convert.ToBoolean( dt.Rows[i]["NotificationRequired"]);

            ///////////Application End*******//


            //****Contact Request Start******//

            ///**********Address*********//
            Address address = new Address();
            address.AddressType = dt.Rows[i]["AddressType"].ToString();
            address.BuildingName = dt.Rows[i]["buildingName"].ToString();
            address.Id = dt.Rows[i]["AddressId"].ToString();
            address.Postal = dt.Rows[i]["Pincode"].ToString();
            address.PostTown = dt.Rows[i]["City"].ToString();
            address.County = dt.Rows[i]["Country"].ToString()  ;
            address.StateProvinceCode = dt.Rows[i]["stateProvinceCode"].ToString();
            address.Street = dt.Rows[i]["AddressLine1"].ToString();
            address.Street2 = dt.Rows[i]["AddressLine2"].ToString();
            TimeAtAddress timeAtAddress = new TimeAtAddress();
            timeAtAddress.Value = dt.Rows[i]["timeAtAddressValue"].ToString();
            timeAtAddress.Unit = dt.Rows[i]["timeAtAddressUnit"].ToString();
            
            address.TimeAtAddress = timeAtAddress;

            List<Address> addressList = new List<Address>();
            addressList.Add(address);
            //***********Email*********//
            Email email = new Email();
            email.EmailAddress = dt.Rows[i]["EmailID"].ToString();
            email.Id = dt.Rows[i]["emailIdKey"].ToString();
            email.Type = dt.Rows[i]["emailType"].ToString();
            List<Email> emailsList= new List<Email>(); 
            emailsList.Add(email);

            ///////////***********employmentHistory*********//
            ///
            EmploymentHistory employmentHistory = new EmploymentHistory();
            employmentHistory.Id = dt.Rows[i]["employerHistId"].ToString();
            employmentHistory.EmployerName = dt.Rows[i]["EmployerName"].ToString();

            EmployerAddress employerAddress = new EmployerAddress();
            employerAddress.AddressType = dt.Rows[i]["EmployerAddressType"].ToString();
            employerAddress.Street = dt.Rows[i]["EmployerStreet"].ToString();
            employerAddress.Street2 = dt.Rows[i]["EmployerStreet1"].ToString();
            employerAddress.CountryCode = dt.Rows[i]["Country"].ToString();
            employerAddress.BuildingName = dt.Rows[i]["employerBuilingName"].ToString();
             
               

            TimeWithEmployer timeWithEmployer = new TimeWithEmployer();
            timeWithEmployer.Unit = dt.Rows[i]["TimeWithEployerUnit"].ToString();
            timeWithEmployer.Duration =Convert.ToInt32( dt.Rows[i]["TimeWithEployerValue"]);

            employmentHistory.EmployerAddress = employerAddress;
            employmentHistory.TimeWithEmployer =timeWithEmployer;

            List<EmploymentHistory > employmentHistoryList = new List<EmploymentHistory>();
            employmentHistoryList.Add(employmentHistory);

            ///////////Employee Hinstory end********//
            //********Person*******//
            Person person = new Person();
            person.Id = dt.Rows[i]["Personid"].ToString();
            Name name = new Name();
            name.FirstName = dt.Rows[i]["FirstName"].ToString();
            name.SurName = dt.Rows[i]["LastName"].ToString();
            name.Id = dt.Rows[i]["ApplicantIdName"].ToString();
            name.Type = dt.Rows[i]["EmployerAddressType"].ToString();

            List<Name> names = new List<Name>();
            names.Add(name);

            PersonDetails personDetails = new PersonDetails();
            personDetails.MaritalStatus = dt.Rows[i]["maritalStatus"].ToString();
            personDetails.QualificationType = dt.Rows[i]["qualificationType"].ToString();
            personDetails.Gender = dt.Rows[i]["Gender"].ToString();
            personDetails.Age =Convert.ToInt32( dt.Rows[i]["Age"]);
            person.Names= names;
            person.PersonDetails = personDetails;

                        
            List<Contacts> contactsList = new List<Contacts>();

            Contacts contacts = new Contacts();
            contacts.Telephones = telephoneList;
            contacts.EmploymentHistory = employmentHistoryList;
            contacts.IdentityDocuments = identityDocumentList;
            contacts.Person = person;
            contacts.Emails = emailsList;
            contacts.Addresses = addressList;
            contacts.Id = dt.Rows[i]["contactId"].ToString();



            contactsList.Add(contacts);


            Payload payload = new Payload();
            payload.Contacts = contactsList;
            payload.Application = application;

            requestModel.Payload = payload;
            requestModel.Header = header;
            


            return requestModel;
        }
    }
}
