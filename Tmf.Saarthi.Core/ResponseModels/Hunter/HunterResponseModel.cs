using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Tmf.Saarthi.Core.RequestModels.Hunter;

namespace Tmf.Saarthi.Core.ResponseModels.Hunter
{
    
    public class HunterResponseModel
    {

        [JsonPropertyName("responseHeader")]
        public ResponseHeader ResponseHeader { get; set; }

        [JsonPropertyName("clientResponsePayload")]
        public ClientResponsePayload ClientResponsePayload { get; set; }

        [JsonPropertyName("originalRequestData")]
        public OriginalRequestData OriginalRequestData { get; set; }

       
    }
    public class ResponseHeader
    {
        [JsonPropertyName("clientReferenceId")]
        public string ClientReferenceId { get; set; }

        [JsonPropertyName("expRequestId")]
        public string ExpRequestId { get; set; }

        [JsonPropertyName("messageTime")]
        public DateTime MessageTime { get; set; }

        [JsonPropertyName("overallResponse")]
        public OverallResponse OverallResponse { get; set; }

        [JsonPropertyName("requestType")]
        public string RequestType { get; set; }

        [JsonPropertyName("responseCode")]
        public string ResponseCode { get; set; }

        [JsonPropertyName("responseMessage")]
        public string ResponseMessage { get; set; }

        [JsonPropertyName("responseType")]
        public string ResponseType { get; set; }

        [JsonPropertyName("tenantID")]
        public string TenantID { get; set; }
    }
    public class ClientResponsePayload
    {
        [JsonPropertyName("decisionElements")]
        public List<DecisionElement> DecisionElements { get; set; }

        [JsonPropertyName("orchestrationDecisions")]
        public List<OrchestrationDecision> OrchestrationDecisions { get; set; }
    }
    public class OriginalRequestData
    {
        [JsonPropertyName("application")]
        public Application Application { get; set; }

        [JsonPropertyName("contacts")]
        public List<Contacts> Contacts { get; set; }
    }
    public class OverallResponse
    {
        [JsonPropertyName("decision")]
        public string Decision { get; set; }

        [JsonPropertyName("decisionReasons")]
        public List<string> DecisionReasons { get; set; }

        [JsonPropertyName("decisionText")]
        public string DecisionText { get; set; }

        [JsonPropertyName("recommendedNextActions")]
        public List<object> RecommendedNextActions { get; set; }

        [JsonPropertyName("score")]
        public int Score { get; set; }

        [JsonPropertyName("spareObjects")]
        public List<object> SpareObjects { get; set; }
    }


  

    public class OrchestrationDecision
    {
        [JsonPropertyName("appReference")]
        public string AppReference { get; set; }

        [JsonPropertyName("decision")]
        public string Decision { get; set; }

        [JsonPropertyName("decisionReasons")]
        public List<object> DecisionReasons { get; set; }

        [JsonPropertyName("decisionSource")]
        public string DecisionSource { get; set; }

        [JsonPropertyName("decisionText")]
        public string DecisionText { get; set; }

        [JsonPropertyName("nextAction")]
        public string NextAction { get; set; }

        [JsonPropertyName("score")]
        public int Score { get; set; }

        [JsonPropertyName("sequenceId")]
        public string SequenceId { get; set; }
    }

   

    public class DecisionElement
    {
        [JsonPropertyName("appReference")]
        public string AppReference { get; set; }

        [JsonPropertyName("normalizedScore")]
        public int NormalizedScore { get; set; }

        [JsonPropertyName("otherData")]
        public OtherData OtherData { get; set; }

        [JsonPropertyName("score")]
        public int Score { get; set; }

        [JsonPropertyName("serviceName")]
        public string ServiceName { get; set; }

        [JsonPropertyName("warningsErrors")]
        public List<WarningsError> WarningsErrors { get; set; }
    }

    public class OtherData
    {
        [JsonPropertyName("response")]
        public Response Response { get; set; }
    }

    public class Response
    {
        [JsonPropertyName("errorWarnings")]
        public ErrorWarnings ErrorWarnings { get; set; }

        [JsonPropertyName("matchSummary")]
        public MatchSummary MatchSummary { get; set; }
    }

    public class ErrorWarnings
    {
        [JsonPropertyName("errors")]
        public Errors Errors { get; set; }

        [JsonPropertyName("warnings")]
        public Warnings Warnings { get; set; }
    }

    public class Errors
    {
        [JsonPropertyName("error")]
        public List<object> Error { get; set; }

        [JsonPropertyName("errorCount")]
        public int ErrorCount { get; set; }
    }

    public class Warnings
    {
        [JsonPropertyName("warning")]
        public List<Warning> Warning { get; set; }

        [JsonPropertyName("warningCount")]
        public int WarningCount { get; set; }
    }

    public class Warning
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("number")]
        public string Number { get; set; }

        [JsonPropertyName("values")]
        public Values Values { get; set; }
    }

    public class Values
    {
        [JsonPropertyName("value")]
        public List<string> Value { get; set; }
    }

    public class MatchSummary
    {
        [JsonPropertyName("matches")]
        public int Matches { get; set; }

        [JsonPropertyName("totalMatchScore")]
        public string TotalMatchScore { get; set; }
    }

    public class WarningsError
    {
        [JsonPropertyName("responseCode")]
        public string ResponseCode { get; set; }

        [JsonPropertyName("responseMessage")]
        public string ResponseMessage { get; set; }

        [JsonPropertyName("responseType")]
        public string ResponseType { get; set; }
    }

}
