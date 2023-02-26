using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Response.Ocr;

public class DLExtractResponseModel
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
    public ResultDL Result { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("task_id")]
    public string TaskId { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;
}

public class ResultDL
{
    [JsonPropertyName("extraction_output")]
    public ExtractionOutputDL ExtractionOutput { get; set; }
}

public class ExtractionOutputDL
{
    [JsonPropertyName("address")]
    public string Address { get; set; } = string.Empty;

    [JsonPropertyName("date_of_birth")]
    public string DateOfBirth { get; set; } = string.Empty;

    [JsonPropertyName("date_of_validity")]
    public object DateOfValidity { get; set; } = string.Empty;

    [JsonPropertyName("district")]
    public string District { get; set; } = string.Empty;

    [JsonPropertyName("fathers_name")]
    public string FathersName { get; set; } = string.Empty;

    [JsonPropertyName("id_number")]
    public string IdNumber { get; set; } = string.Empty;

    [JsonPropertyName("is_scanned")]
    public string IsScanned { get; set; } = string.Empty;

    [JsonPropertyName("issue_dates")]
    public IssueDates IssueDates { get; set; }

    [JsonPropertyName("name_on_card")]
    public string NameOnCard { get; set; } = string.Empty;

    [JsonPropertyName("pincode")]
    public string Pincode { get; set; } = string.Empty;

    [JsonPropertyName("state")]
    public string State { get; set; } = string.Empty;

    [JsonPropertyName("street_address")]
    public string StreetAddress { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public List<object> Type { get; set; }

    [JsonPropertyName("validity")]
    public Validity Validity { get; set; }
}

public class IssueDates
{
    [JsonPropertyName("LMV")]
    public string LMV { get; set; } = string.Empty;

    [JsonPropertyName("MCWG")]
    public string MCWG { get; set; } = string.Empty;

    [JsonPropertyName("TRANS")]
    public string TRANS { get; set; } = string.Empty;
}

public class Validity
{
    [JsonPropertyName("NT")]
    public string NT { get; set; } = string.Empty;

    [JsonPropertyName("T")]
    public string T { get; set; } = string.Empty;
}
