﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tmf.Saarthi.Core.ResponseModels.Credit
{
    public class FiRetriggerResponse
    {
        [JsonPropertyName("Message")]
        public string Message { get; set; }
    }
}
