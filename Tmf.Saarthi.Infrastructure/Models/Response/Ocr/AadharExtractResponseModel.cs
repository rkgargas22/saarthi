using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Response.Ocr;

public class AadharExtractResponseModel
{
    [JsonPropertyName("action")]
    public string Action { get; set; } = string.Empty;

    [JsonPropertyName("completed_at")]
    public DateTime CompletedAt { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("group_id")]
    public string GroupId { get; set; } = string.Empty;

    [JsonPropertyName("request_id")]
    public string RequestId { get; set; } = string.Empty;

    [JsonPropertyName("result")]
    public ResultAadhar Result { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("task_id")]
    public string TaskId { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;
}

public class ResultAadhar
{
    [JsonPropertyName("extraction_output")]
    public ExtractionOutputAadhar ExtractionOutput { get; set; }

    [JsonPropertyName("qr_output")]
    public QrOutput QrOutput { get; set; }
}

public class ExtractionOutputAadhar
{
    [JsonPropertyName("address")]
    public string Address { get; set; } = string.Empty;

    [JsonPropertyName("date_of_birth")]
    public string DateOfBirth { get; set; } = string.Empty;

    [JsonPropertyName("district")]
    public string District { get; set; } = string.Empty;

    [JsonPropertyName("fathers_name")]
    public string FathersName { get; set; } = string.Empty;

    [JsonPropertyName("gender")]
    public string Gender { get; set; } = string.Empty;

    [JsonPropertyName("house_number")]
    public string HouseNumber { get; set; } = string.Empty;

    [JsonPropertyName("id_number")]
    public string IdNumber { get; set; } = string.Empty;

    [JsonPropertyName("is_scanned")]
    public bool? IsScanned { get; set; }

    [JsonPropertyName("name_on_card")]
    public string NameOnCard { get; set; } = string.Empty;

    [JsonPropertyName("pincode")]
    public string Pincode { get; set; } = string.Empty;

    [JsonPropertyName("state")]
    public string State { get; set; } = string.Empty;

    [JsonPropertyName("street_address")]
    public string StreetAddress { get; set; } = string.Empty;

    [JsonPropertyName("year_of_birth")]
    public string YearOfBirth { get; set; } = string.Empty;
}

public class QrOutput
{
    [JsonPropertyName("address")]
    public string Address { get; set; } = string.Empty;

    [JsonPropertyName("date_of_birth")]
    public string DateOfBirth { get; set; } = string.Empty;

    [JsonPropertyName("district")]
    public string District { get; set; } = string.Empty;

    [JsonPropertyName("gender")]
    public string Gender { get; set; } = string.Empty;

    [JsonPropertyName("house_number")]
    public string HouseNumber { get; set; } = string.Empty;

    [JsonPropertyName("id_number")]
    public string IdNumber { get; set; } = string.Empty;

    [JsonPropertyName("name_on_card")]
    public string NameOnCard { get; set; } = string.Empty;

    [JsonPropertyName("pincode")]
    public string Pincode { get; set; } = string.Empty;

    [JsonPropertyName("state")]
    public string State { get; set; } = string.Empty;

    [JsonPropertyName("street_address")]
    public string StreetAddress { get; set; } = string.Empty;

    [JsonPropertyName("year_of_birth")]
    public string YearOfBirth { get; set; } = string.Empty;
}
