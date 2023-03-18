﻿using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Request.FuelLoanAggrement;

public class FuelLoanAggrementRequestModel
{
    [JsonPropertyName("mappingProperties")]
    public Dictionary<string, string> MappingProperties { get; set; } = new Dictionary<string, string>();

    [JsonPropertyName("htmlbase64String")]
    public string Htmlbase64String { get; set; } = string.Empty;
}
