using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Response.Ocr; 

public class DocumentMaskingResponseModel 
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
    public ResultVoterId Result { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("task_id")]
    public string TaskId { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;
}

public class ResultVoterId
{
    [JsonPropertyName("extraction_output")]
    public ExtractionOutputVoterId ExtractionOutput { get; set; }
}

public class ExtractionOutputVoterId
{
    [JsonPropertyName("address")]
    public object Address { get; set; } = string.Empty;

    [JsonPropertyName("age")]
    public object Age { get; set; } = string.Empty;

    [JsonPropertyName("date_of_birth")]
    public object DateOfBirth { get; set; } = string.Empty;

    [JsonPropertyName("district")]
    public object District { get; set; } = string.Empty;

    [JsonPropertyName("fathers_name")]
    public string FathersName { get; set; } = string.Empty;

    [JsonPropertyName("gender")]
    public object Gender { get; set; } = string.Empty;

    [JsonPropertyName("house_number")]
    public object HouseNumber { get; set; } = string.Empty;

    [JsonPropertyName("id_number")]
    public string IdNumber { get; set; } = string.Empty;

    [JsonPropertyName("is_scanned")]
    public bool? IsScanned { get; set; }

    [JsonPropertyName("name_on_card")]
    public string NameOnCard { get; set; } = string.Empty;

    [JsonPropertyName("pincode")]
    public object Pincode { get; set; } = string.Empty;

    [JsonPropertyName("state")]
    public object State { get; set; } = string.Empty;

    [JsonPropertyName("street_address")]
    public object StreetAddress { get; set; } = string.Empty;

    [JsonPropertyName("year_of_birth")]
    public object YearOfBirth { get; set; } = string.Empty;
}
