using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RichardSzalay.MockHttp;
using Shared.API.Enums;
using Shared.API.Mappers;
using Shared.API.ResponseModels;

namespace Shared.API.Mock
{
    public static class ApiResponses
    {
        private static string header = "application/json";
        private static readonly List<ExpectedResponseModel> Responses = new List<ExpectedResponseModel>();
        public static readonly string ValidMetadata = "valid";
        public static readonly string InvalidMetadata = "wrong";
        public static readonly Device MockDevice = new Device { DeviceId = "0", Name = "Mock", Status = "0" };

        static ApiResponses()
        {
            // Valid responses
            var getShutdownPending = JsonConvert.SerializeObject(new GetShutdownPendingResponse {Error = String.Empty, Shutdown = false, Success = true});
            var verifyUserLoginResponse = JsonConvert.SerializeObject(new VerifyUserLoginResponse {Error = String.Empty, Success = true, Valid = false});
            var verifyDeviceIdResponse = JsonConvert.SerializeObject(new VerifyDeviceIdResponse { Error = String.Empty, Success = true, IsRegistered = false});
            var baseResponse = JsonConvert.SerializeObject(new BaseResponse { Error = String.Empty, Success = true });
            var listDevicesResponse = JsonConvert.SerializeObject(new ListDevicesResponse
            {
                Error = String.Empty, Success = true,
                Devices = new List<Device> { MockDevice }
            });

            // Invalid responses
            var getShutdownPendingInvalid = JsonConvert.SerializeObject(new GetShutdownPendingResponse { Error = String.Empty, Shutdown = true, Success = false });
            var verifyUserLoginResponseInvalid = JsonConvert.SerializeObject(new VerifyUserLoginResponse { Error = String.Empty, Success = false, Valid = true });
            var verifyDeviceIdResponseInvalid = JsonConvert.SerializeObject(new VerifyDeviceIdResponse { Error = String.Empty, Success = false, IsRegistered = true });
            var baseResponseInvalid = JsonConvert.SerializeObject(new BaseResponse { Error = String.Empty, Success = false });
            var listDevicesResponseInvalid = JsonConvert.SerializeObject(new ListDevicesResponse
            {
                Error = String.Empty,
                Success = false,
                Devices = new List<Device> { MockDevice }
            });


            // Valid requests
            Responses.Add(new ExpectedResponseModel
            {
                Path = BaseUrl.Path + ApiPathMapper.GetPath(ApiPaths.GetShutdownPending),
                Response = getShutdownPending,
                Metadata = ValidMetadata
            });

            Responses.Add(new ExpectedResponseModel
            {
                Path = BaseUrl.Path + ApiPathMapper.GetPath(ApiPaths.VerifyUserLogin),
                Response = verifyUserLoginResponse,
                Metadata = ValidMetadata
            });

            Responses.Add(new ExpectedResponseModel
            {
                Path = BaseUrl.Path + ApiPathMapper.GetPath(ApiPaths.VerifyDeviceId),
                Response = verifyDeviceIdResponse,
                Metadata = ValidMetadata
            });

            Responses.Add(new ExpectedResponseModel
            {
                Path = BaseUrl.Path + ApiPathMapper.GetPath(ApiPaths.SetShutdownPending),
                Response = baseResponse,
                Metadata = ValidMetadata
            });

            Responses.Add(new ExpectedResponseModel
            {
                Path = BaseUrl.Path + ApiPathMapper.GetPath(ApiPaths.ClearShutdownPending),
                Response = baseResponse,
                Metadata = ValidMetadata
            });

            Responses.Add(new ExpectedResponseModel
            {
                Path = BaseUrl.Path + ApiPathMapper.GetPath(ApiPaths.GetDevices),
                Response = listDevicesResponse,
                Metadata = ValidMetadata
            });

            Responses.Add(new ExpectedResponseModel
            {
                Path = BaseUrl.Path + ApiPathMapper.GetPath(ApiPaths.AddDevice),
                Response = baseResponse,
                Metadata = ValidMetadata
            });

            Responses.Add(new ExpectedResponseModel
            {
                Path = BaseUrl.Path + ApiPathMapper.GetPath(ApiPaths.DeleteDevice),
                Response = baseResponse,
                Metadata = ValidMetadata
            });

            // Invalid requests
            Responses.Add(new ExpectedResponseModel
            {
                Path = BaseUrl.Path + ApiPathMapper.GetPath(ApiPaths.GetShutdownPending),
                Response = getShutdownPendingInvalid,
                Metadata = InvalidMetadata
            });

            Responses.Add(new ExpectedResponseModel
            {
                Path = BaseUrl.Path + ApiPathMapper.GetPath(ApiPaths.VerifyUserLogin),
                Response = verifyUserLoginResponseInvalid,
                Metadata = InvalidMetadata
            });

            Responses.Add(new ExpectedResponseModel
            {
                Path = BaseUrl.Path + ApiPathMapper.GetPath(ApiPaths.VerifyDeviceId),
                Response = verifyDeviceIdResponseInvalid,
                Metadata = InvalidMetadata
            });

            Responses.Add(new ExpectedResponseModel
            {
                Path = BaseUrl.Path + ApiPathMapper.GetPath(ApiPaths.SetShutdownPending),
                Response = baseResponseInvalid,
                Metadata = InvalidMetadata
            });

            Responses.Add(new ExpectedResponseModel
            {
                Path = BaseUrl.Path + ApiPathMapper.GetPath(ApiPaths.ClearShutdownPending),
                Response = baseResponseInvalid,
                Metadata = InvalidMetadata
            });

            Responses.Add(new ExpectedResponseModel
            {
                Path = BaseUrl.Path + ApiPathMapper.GetPath(ApiPaths.GetDevices),
                Response = listDevicesResponseInvalid,
                Metadata = InvalidMetadata
            });

            Responses.Add(new ExpectedResponseModel
            {
                Path = BaseUrl.Path + ApiPathMapper.GetPath(ApiPaths.AddDevice),
                Response = baseResponseInvalid,
                Metadata = InvalidMetadata
            });

            Responses.Add(new ExpectedResponseModel
            {
                Path = BaseUrl.Path + ApiPathMapper.GetPath(ApiPaths.DeleteDevice),
                Response = baseResponseInvalid,
                Metadata = InvalidMetadata
            });

        }

        public static void FillRules(MockHttpMessageHandler handler)
        {
            foreach (var response in Responses)
            {
                handler.When(response.Path).WithPartialContent(response.Metadata).Respond(header, response.Response);
            }
        }
    }
}
