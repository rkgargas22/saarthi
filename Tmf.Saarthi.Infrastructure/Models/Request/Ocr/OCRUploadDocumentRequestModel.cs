using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmf.Saarthi.Infrastructure.Models.Request.Ocr
{
    public class OCRUploadDocumentRequestModel
    {
        public long FleetID { get; set; }
        public long StagId { get; set; }
        public int DocumentType { get; set; } = 0;
        public string DocumentExtension { get; set; } = string.Empty;
        public string DocumentURL_1 { get; set; } = string.Empty;
        public string DocumentURL_2 { get; set; } = string.Empty;
    }
}
