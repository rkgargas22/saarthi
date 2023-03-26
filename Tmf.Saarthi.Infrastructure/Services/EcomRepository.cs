using Microsoft.Extensions.Options;
using System.Xml.Serialization;
using Tmf.Saarthi.Core.Options;
using Tmf.Saarthi.Core.ResponseModels.Ecom;
using Tmf.Saarthi.Infrastructure.HttpService;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request.Ecom;
using System.Xml.Serialization;
using System.Text.Json;
using Microsoft.Data.SqlClient;
using Tmf.Saarthi.Infrastructure.SqlService;
using System.Data;

namespace Tmf.Saarthi.Infrastructure.Services
{
 
    public class EcomRepository : IEcomRepository
    {
        private readonly IHttpService _httpService;
        private readonly EcomOptions _ecomOptions;
        private readonly ISqlUtility _sqlUtility;
        private readonly ConnectionStringsOptions _connectionStringsOptions;
        public EcomRepository(IHttpService httpService, IOptions<EcomOptions> ecomOptions, ISqlUtility sqlUtility, IOptions<ConnectionStringsOptions> connectionStringsOptions)
        {
            _httpService = httpService;
            _ecomOptions = ecomOptions.Value;
            _sqlUtility = sqlUtility;
            _connectionStringsOptions = connectionStringsOptions.Value;
        }

        public async Task<List<GenerateManifestResponse>> GenerateManifest(EcomGenerateManifestModel ecomGenerateManifestModel)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("BpNo", "1");
            headers.Add("UserType", "User");

            //var dict_input = new Dictionary<string, string>
            //{
            //    { "json_input", JsonSerializer.Serialize(ecomGenerateManifestModel.JsonInput) }
            //};

            var dict_input = JsonSerializer.Serialize(ecomGenerateManifestModel.JsonInput);

            var result = await _httpService.PostAsync(_ecomOptions.BaseUrl.GenerateManifestUrl + _ecomOptions.GenerateManifest, ecomGenerateManifestModel.JsonInput, headers);

            if (result == null)
            {
                return new List<GenerateManifestResponse>();
            }

            var jsonSerializerOptions = new JsonSerializerOptions() { WriteIndented = true };

            return JsonSerializer.Deserialize<List<GenerateManifestResponse>>(result, jsonSerializerOptions) ?? throw new ArgumentNullException();
        }

        public async Task<List<RescheduleOrCancelAppointmentResponse>> RescheduleOrCancelAppointment(EcomRescheduleOrCancelAppointmentModel ecomRescheduleOrCancelAppointment)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("BpNo", "1");
            headers.Add("UserType", "User");

            var dict_input = JsonSerializer.Serialize(ecomRescheduleOrCancelAppointment.JsonInput);

            var result = await _httpService.PostAsync(_ecomOptions.BaseUrl.RescheduleOrCancelRequestUrl + _ecomOptions.RescheduleOrCancelRequest, ecomRescheduleOrCancelAppointment.JsonInput, headers);

            if (result == null)
            {
                return new List<RescheduleOrCancelAppointmentResponse>();
            }

            var jsonSerializerOptions = new JsonSerializerOptions() { WriteIndented = true };

            return JsonSerializer.Deserialize<List<RescheduleOrCancelAppointmentResponse>>(result, jsonSerializerOptions) ?? throw new ArgumentNullException();
        }

        public async Task<PushShipmentTrackResponse> PushShipmentTrack(EcomPushShipmentTrackModel ecomPushShipmentTrackModel)
        {

            bool res = await UpdatePushShipmentTrack(ecomPushShipmentTrackModel);
            PushShipmentTrackResponse pushShipmentTrackResponse = new PushShipmentTrackResponse();
            if (res) {
                pushShipmentTrackResponse.Status = res;
                pushShipmentTrackResponse.Message = "Success";
            }
            else {
                pushShipmentTrackResponse.Status = false;
                pushShipmentTrackResponse.Message = "failed";
            }

            return pushShipmentTrackResponse;
        }

        private async Task<bool> UpdatePushShipmentTrack(EcomPushShipmentTrackModel ecomPushShipmentTrackModel)
        {
                        
            List<SqlParameter> parameters = new List<SqlParameter>()
                        {
                            new SqlParameter("AgentId",ecomPushShipmentTrackModel.AgentId),
                            new SqlParameter("AwbNumber",ecomPushShipmentTrackModel.AwbNumber),
                            new SqlParameter("AgentName",ecomPushShipmentTrackModel.AgentName),
                            new SqlParameter("Latitude",ecomPushShipmentTrackModel.Latitude),
                            new SqlParameter("Longitude",ecomPushShipmentTrackModel.Longitude),
                            new SqlParameter("ReasonCodeDescription",ecomPushShipmentTrackModel.ReasonCodeDescription),
                            new SqlParameter("RescheduledDate",ecomPushShipmentTrackModel.RescheduledDate),
                            new SqlParameter("RescheduledTime",ecomPushShipmentTrackModel.RescheduledTime),
                            new SqlParameter("OrderId",ecomPushShipmentTrackModel.OrderId),
                            new SqlParameter("ReasonCodeNumber",ecomPushShipmentTrackModel.ReasonCodeNumber),
                            new SqlParameter("Timestamp",ecomPushShipmentTrackModel.Timestamp),
                            new SqlParameter("VendorCode",ecomPushShipmentTrackModel.VendorCode),
                            //new SqlParameter("Document",ecomPushShipmentTrackModel.Document)
                        };


                await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_pushShipmentTrack", parameters);

            return true;        


        }

    }
}
