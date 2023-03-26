using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using Tmf.Saarthi.Core.RequestModels.Ecom;
using Tmf.Saarthi.Core.ResponseModels.Ecom;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request.Ecom;
using Tmf.Saarthi.Manager.Interfaces;
using Tmf.Saarthi.Infrastructure.SqlService;
using Tmf.Saarthi.Core.Options;
using System.Text.Json;

namespace Tmf.Saarthi.Manager.Services
{
    public class EcomManager : IEcomManager
    {
        private readonly IEcomRepository _ecomRepository;
        private readonly ISqlUtility _sqlUtility;
        private readonly ConnectionStringsOptions _connectionStringsOptions;

        public EcomManager(IEcomRepository ecomRepository, ISqlUtility sqlUtility, IOptions<ConnectionStringsOptions> connectionStringsOptions)
        {
            _ecomRepository = ecomRepository;
            _sqlUtility = sqlUtility;
            _connectionStringsOptions = connectionStringsOptions.Value;

        }
        
        public async Task<List<GenerateManifestResponse>> GenerateManifest(int fleetId, Core.RequestModels.Ecom.AdditionalInformation ecomManifestRequest)
        {
            EcomGenerateManifestModel ecomGenerateManifestModel = new EcomGenerateManifestModel();
            List<Infrastructure.Models.Request.Ecom.Activity> ActivityList = new List<Infrastructure.Models.Request.Ecom.Activity>();

            DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_getManifestActivities");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {                    
                    Infrastructure.Models.Request.Ecom.Activity EcomActivity = new Infrastructure.Models.Request.Ecom.Activity
                    {
                        Code = Convert.ToString(dt.Rows[i]["Code"]),
                        DocumentRefNumber = Convert.ToString(dt.Rows[i]["DocumentRefNumber"]),
                        Optional = Convert.ToString(dt.Rows[i]["Optional"]),
                        Remarks = Convert.ToString(dt.Rows[i]["Remarks"]),
                    };
                    ActivityList.Add(EcomActivity);
                }

            }


            Infrastructure.Models.Request.Ecom.AdditionalInformation EcomAdditionalInformation = new Infrastructure.Models.Request.Ecom.AdditionalInformation();
            if (ecomManifestRequest != null)
            {
                EcomAdditionalInformation.Timeslot = ecomManifestRequest.Timeslot;
                EcomAdditionalInformation.FormPrint = ecomManifestRequest.FormPrint;
                EcomAdditionalInformation.Date = ecomManifestRequest.Date;
            }

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("FleetId", fleetId),
            };

            DataTable EcomParms = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_getEcomRequestParms", parameters);
            EcomGenerateManifestJsonInput ecomGenerateManifestJsonInput = new EcomGenerateManifestJsonInput
            {
                Activites =  ActivityList,
                AdditionalInformation = EcomAdditionalInformation,
                OrderNumber = Convert.ToString(EcomParms.Rows[0]["OrderNumber"]),
                Product = Convert.ToString(EcomParms.Rows[0]["Product"]),
                Consignee = Convert.ToString(EcomParms.Rows[0]["Consignee"]),
                ConsigneeAddress1 = Convert.ToString(EcomParms.Rows[0]["ConsigneeAddress1"]),
                ConsigneeAddress2 = Convert.ToString(EcomParms.Rows[0]["ConsigneeAddress2"]),
                ConsigneeAddress3 = Convert.ToString(EcomParms.Rows[0]["ConsigneeAddress3"]),
                ConsigneeAddress4 = Convert.ToString(EcomParms.Rows[0]["ConsigneeAddress4"]),
                CollectableValue = Convert.ToString(EcomParms.Rows[0]["CollectableValue"]),
                DestinationCity = Convert.ToString(EcomParms.Rows[0]["DestinationCity"]),
                DropVendorCode = Convert.ToString(EcomParms.Rows[0]["DropVendorCode"]),
                ItemDescription = Convert.ToString(EcomParms.Rows[0]["ItemDescription"]),
                Mobile = Convert.ToString(EcomParms.Rows[0]["Mobile"]),
                Telephone = Convert.ToString(EcomParms.Rows[0]["Telephone"]),
                Pincode = Convert.ToString(EcomParms.Rows[0]["Pincode"]),
                State = Convert.ToString(EcomParms.Rows[0]["State"]),
                DropName = Convert.ToString(EcomParms.Rows[0]["DropName"]),
                DropAddressLine1 = Convert.ToString(EcomParms.Rows[0]["DropAddressLine1"]),
                DropAddressLine2 = Convert.ToString(EcomParms.Rows[0]["DropAddressLine2"]),
                DropAddressLine3 = Convert.ToString(EcomParms.Rows[0]["DropAddressLine3"]),
                DropAddressLine4 = Convert.ToString(EcomParms.Rows[0]["DropAddressLine4"]),
                DropMobile = Convert.ToString(EcomParms.Rows[0]["DropMobile"]),
                DropPincode = Convert.ToString(EcomParms.Rows[0]["DropPincode"])
            };

        //    List<EcomGenerateManifestJsonInput> ecomGenerateManifestJsonInputList = new List<EcomGenerateManifestJsonInput>
        //{
        //    ecomGenerateManifestJsonInput
        //};

            ecomGenerateManifestModel.JsonInput = ecomGenerateManifestJsonInput;

            List<GenerateManifestResponse> ecomManifestResponse = await _ecomRepository.GenerateManifest(ecomGenerateManifestModel);
            bool res = await UpdateManifestResponse(ecomManifestResponse, ecomManifestRequest, fleetId);            
            return ecomManifestResponse;
           
        }
        private async Task<bool> UpdateManifestResponse(List<GenerateManifestResponse> ecomManifestResponse, Core.RequestModels.Ecom.AdditionalInformation ecomAdditionalInformation, int fleetId)
        {
            bool flag = false;
            if (ecomManifestResponse[0].Success == true)
            {
                List<SqlParameter> parameters = new List<SqlParameter>()
                            {

                                new SqlParameter("FleetID",fleetId),
                                new SqlParameter("AwbNumber",ecomManifestResponse[0].AwbNumber),
                                new SqlParameter("OrderNumber",ecomManifestResponse[0].OrderNumber),
                                new SqlParameter("Comment",ecomManifestResponse[0].Reason),
                                new SqlParameter("TimeSlotDate",ecomAdditionalInformation.Date),
                                new SqlParameter("TimeSlot",ecomAdditionalInformation.Timeslot)
                            };

                await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_updateEcomManifestResponse", parameters);
                flag = true;
            }

            return flag;

        }

        
        public async Task<List<RescheduleOrCancelAppointmentResponse>> RescheduleOrCancelAppointment(int fleetId, Core.RequestModels.Ecom.AdditionalInformation ecomRescheduleOrCancelRequest)
        {
            EcomRescheduleOrCancelAppointmentModel ecomRescheduleOrCancelAppointment = new EcomRescheduleOrCancelAppointmentModel();
            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("FleetId", fleetId),
            };

            DataTable EcomParms = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_getEcomRequestParms", parameters);

            Infrastructure.Models.Request.Ecom.ConsigneeAddress consigneeAddress = new Infrastructure.Models.Request.Ecom.ConsigneeAddress();
            {
                consigneeAddress.CA1 = Convert.ToString(EcomParms.Rows[0]["CA1"]);
                consigneeAddress.CA2 = Convert.ToString(EcomParms.Rows[0]["CA2"]);
                consigneeAddress.CA3 = Convert.ToString(EcomParms.Rows[0]["CA3"]);
                consigneeAddress.CA4 = Convert.ToString(EcomParms.Rows[0]["CA4"]);
                consigneeAddress.Pincode = Convert.ToString(EcomParms.Rows[0]["Pincode"]);
            }

            EcomRescheduleOrCancelAppointmentJsonInput ecomRescheduleOrCancelAppointmentJsonInput = new EcomRescheduleOrCancelAppointmentJsonInput
            {
                Awb = Convert.ToString(EcomParms.Rows[0]["Awb"]),
                Comments = Convert.ToString(EcomParms.Rows[0]["Comments"]),
                ConsigneeAddress = consigneeAddress,
                Instruction = Convert.ToString(EcomParms.Rows[0]["Instruction"]),
                Mobile = Convert.ToString(EcomParms.Rows[0]["Mobile"]),
                ScheduledDeliveryDate = Convert.ToString(EcomParms.Rows[0]["ScheduledDeliveryDate"]),
                ScheduledDeliverySlot = Convert.ToString(EcomParms.Rows[0]["ScheduledDeliverySlot"])
            };

            List<EcomRescheduleOrCancelAppointmentJsonInput> ecomRescheduleOrCancelAppointmentJsonInputList = new List<EcomRescheduleOrCancelAppointmentJsonInput>
            {
                ecomRescheduleOrCancelAppointmentJsonInput
            };
            ecomRescheduleOrCancelAppointment.JsonInput = ecomRescheduleOrCancelAppointmentJsonInputList;

            return await _ecomRepository.RescheduleOrCancelAppointment(ecomRescheduleOrCancelAppointment);

        }
        public async Task<PushShipmentTrackResponse> PushShipmentTrack(PushShipmentStatusRequest pushShipmentStatus)
        {
            List<Infrastructure.Models.Request.Ecom.Documents> documentList = new List<Infrastructure.Models.Request.Ecom.Documents>();
            if (pushShipmentStatus.Document != null && pushShipmentStatus.Document.Count > 0)
            {
                foreach (var doc in pushShipmentStatus.Document)
                {
                    Infrastructure.Models.Request.Ecom.Documents EcomDoc = new Infrastructure.Models.Request.Ecom.Documents
                    {
                        ActivityCode = doc.ActivityCode,
                        Image = doc.Image
                    };
                    documentList.Add(EcomDoc);
                }
            }

            EcomPushShipmentTrackModel ecomPushShipmentTrackModel = new EcomPushShipmentTrackModel
            {
                AwbNumber = pushShipmentStatus.AwbNumber,
                AgentId = pushShipmentStatus.AgentId,
                AgentName = pushShipmentStatus.AgentName,
                Document = documentList,
                Latitude = pushShipmentStatus.Latitude,
                Longitude = pushShipmentStatus.Longitude,
                ReasonCodeDescription = pushShipmentStatus.ReasonCodeDescription,
                RescheduledDate = pushShipmentStatus.RescheduledDate,
                OrderId = pushShipmentStatus.OrderId,
                ReasonCodeNumber = pushShipmentStatus.ReasonCodeNumber,
                RescheduledTime = pushShipmentStatus.RescheduledTime,
                Timestamp = pushShipmentStatus.Timestamp,
                VendorCode = pushShipmentStatus.VendorCode
            };

            return await _ecomRepository.PushShipmentTrack(ecomPushShipmentTrackModel);
        }

        
    }
}
