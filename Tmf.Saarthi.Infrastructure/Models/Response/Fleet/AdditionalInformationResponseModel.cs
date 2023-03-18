﻿using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Response.Fleet;

public class AdditionalInformationResponseModel
{
    [JsonPropertyName("additionalInfoId")]
    public long AdditionalInfoId { get; set; }

    [JsonPropertyName("errorMessage")]
    public string ErrorMessage { get; set; } = string.Empty;

    [JsonPropertyName("stageId")]
    public long StageId { get; set; }
}
