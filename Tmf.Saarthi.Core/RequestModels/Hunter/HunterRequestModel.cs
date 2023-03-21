using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tmf.Saarthi.Core.RequestModels.Hunter
{
     
    public class HunterRequestModel
    {
        [JsonPropertyName("header")]
        public Header Header { get; set; }

        [JsonPropertyName("payload")]
        public Payload Payload { get; set; }
    }

    public class Options
    {

    }

    public class Header
    {
        [JsonPropertyName("clientReferenceId")]
        public string ClientReferenceId { get; set; }=String.Empty;

        [JsonPropertyName("expRequestId")]
        public string ExpRequestId { get; set; } = String.Empty;

        [JsonPropertyName("messageTime")]
        public string MessageTime { get; set; }

        [JsonPropertyName("options")]
        public Options Options { get; set; } 

        [JsonPropertyName("requestType")]
        public string RequestType { get; set; } = String.Empty;

        [JsonPropertyName("tenantId")]
        public string TenantId { get; set; } = String.Empty;

        [JsonPropertyName("time")]
        public string Time { get; set; } = String.Empty;

        [JsonPropertyName("txnId")]
        public string TxnId { get; set; } = String.Empty;
    }
    public class Payload
    {
        [JsonPropertyName("application")]
        public Application Application { get; set; }

        [JsonPropertyName("contacts")]
        public List<Contacts> Contacts { get; set; }


    }

    public class Application
    {
        [JsonPropertyName("applicants")]
        public List<Applicant> Applicants { get; set; }

        [JsonPropertyName("notificationRequired")]
        public bool NotificationRequired { get; set; }

        [JsonPropertyName("originalRequestTime")]
        public string OriginalRequestTime { get; set; }

        [JsonPropertyName("productDetails")]
        public ProductDetails ProductDetails { get; set; }
    }

    public class Applicant
    {
        [JsonPropertyName("applicantType")]
        public string ApplicantType { get; set; } = String.Empty;

        [JsonPropertyName("contactId")]
        public string ContactId { get; set; } = String.Empty;

        [JsonPropertyName("id")]
        public string Id { get; set; } = String.Empty;
    }

    public class ProductDetails
    {
        [JsonPropertyName("productCode")]
        public string ProductCode { get; set; } = String.Empty;

        [JsonPropertyName("productType")]
        public string ProductType { get; set; } = String.Empty;
    }

    public class Contacts
    {
        [JsonPropertyName("addresses")]
        public List<Address> Addresses { get; set; }

        [JsonPropertyName("emails")]
        public List<Email> Emails { get; set; }

        [JsonPropertyName("employmentHistory")]
        public List<EmploymentHistory> EmploymentHistory { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; } = String.Empty;

        [JsonPropertyName("identityDocuments")]
        public List<IdentityDocument> IdentityDocuments { get; set; }

        [JsonPropertyName("telephones")]
        public List<Telephone> Telephones { get; set; }   

        [JsonPropertyName("person")]
        public Person Person { get; set; }
    }

    public class Person
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = String.Empty;

        [JsonPropertyName("names")]
        public List<Name> Names { get; set; }

        [JsonPropertyName("personDetails")]
        public PersonDetails PersonDetails { get; set; }
    }

    public class Name
    {
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; } = String.Empty;

        [JsonPropertyName("id")]
        public string Id { get; set; } = String.Empty;

        [JsonPropertyName("surName")]
        public string SurName { get; set; } = String.Empty;

        [JsonPropertyName("type")]
        public string Type { get; set; } = String.Empty;
    }

    public class PersonDetails
    {
        [JsonPropertyName("age")]
        public int Age { get; set; }

        [JsonPropertyName("gender")]
        public string Gender { get; set; } = String.Empty;

        [JsonPropertyName("maritalStatus")]
        public string MaritalStatus { get; set; } = String.Empty;

        [JsonPropertyName("qualificationType")]
        public string QualificationType { get; set; } = String.Empty;
    }
    public class Telephone
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = String.Empty;

        [JsonPropertyName("number")]
        public string Number { get; set; } = String.Empty;

        [JsonPropertyName("type")]
        public string Type { get; set; } = String.Empty;
    }

     public class Address
    {
        [JsonPropertyName("addressType")]
        public string AddressType { get; set; } = String.Empty;

        [JsonPropertyName("buildingName")]
        public string BuildingName { get; set; } = String.Empty;

        [JsonPropertyName("county")]
        public string County { get; set; } = String.Empty;

        [JsonPropertyName("id")]
        public string Id { get; set; } = String.Empty;

        [JsonPropertyName("postal")]
        public string Postal { get; set; } = String.Empty;

        [JsonPropertyName("postTown")]
        public string PostTown { get; set; } = String.Empty;

        [JsonPropertyName("stateProvinceCode")]
        public string StateProvinceCode { get; set; } = String.Empty;

        [JsonPropertyName("street")]
        public string Street { get; set; } = String.Empty;

        [JsonPropertyName("street2")]
        public string Street2 { get; set; } = String.Empty;

        [JsonPropertyName("timeAtAddress")]
        public TimeAtAddress TimeAtAddress { get; set; }
    }

    public class Email
    {
        [JsonPropertyName("email")]
        public string EmailAddress { get; set; } = String.Empty;

        [JsonPropertyName("id")]
        public string Id { get; set; } = String.Empty;

        [JsonPropertyName("type")]
        public string Type { get; set; } = String.Empty;
    }

    public class EmploymentHistory
    {
        [JsonPropertyName("employerAddress")]
        public EmployerAddress EmployerAddress { get; set; }

        [JsonPropertyName("employerName")]
        public string EmployerName { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; } = String.Empty;

        [JsonPropertyName("timeWithEmployer")]
        public TimeWithEmployer TimeWithEmployer { get; set; }
    }

    public class IdentityDocument
    {
        [JsonPropertyName("documentNumber")]
        public string DocumentNumber { get; set; } = String.Empty;

        [JsonPropertyName("documentType")]
        public string DocumentType { get; set; } = String.Empty;

        [JsonPropertyName("id")]
        public string Id { get; set; } = String.Empty;
    }

    public class TimeAtAddress
    {
        [JsonPropertyName("unit")]
        public string Unit { get; set; } = String.Empty;

        [JsonPropertyName("value")]
        public string Value { get; set; } = String.Empty;
    }

    public class EmployerAddress
    {
        [JsonPropertyName("addressType")]
        public string AddressType { get; set; } = String.Empty;

        [JsonPropertyName("buildingName")]
        public string BuildingName { get; set; } = String.Empty;

        [JsonPropertyName("countryCode")]
        public string CountryCode { get; set; } = String.Empty;

        [JsonPropertyName("street")]
        public string Street { get; set; } = String.Empty;

        [JsonPropertyName("street2")]
        public string Street2 { get; set; } = String.Empty;
    }

    public class TimeWithEmployer
    {
        [JsonPropertyName("duration")]
        public int Duration { get; set; }

        [JsonPropertyName("unit")]
        public string Unit { get; set; } = String.Empty;
    }

}
