using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tmf.Saarthi.Core.ResponseModels.DMS
{
    public class ViewDocumentResponse
    {
        [JsonPropertyName("documentHtml")]
        public string DocumentHtml { get; set; }

    }
}
