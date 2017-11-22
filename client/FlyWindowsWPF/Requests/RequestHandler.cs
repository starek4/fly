using System;
using System.Net.Http;
using System.Threading.Tasks;
using Shared.API.Exceptions;
using Shared.API.ResponseModels;

namespace FlyWindowsWPF.Requests
{
    public static class RequestHandler
    {
        public static async Task DoRequest(Task request)
        {
            try
            {
                await request;
            }
            catch (Exception exception)
            {
                if (exception is HttpRequestException)
                {
                    ApplicationKiller.NetworkError();
                }
                if (exception is DatabaseException)
                {
                    ApplicationKiller.DatabaseError();
                }
            }
        }

        public static async Task<bool> DoRequest(Task<bool> request)
        {
            bool response;
            try
            {
                response = await request;
            }
            catch (Exception exception)
            {
                if (exception is HttpRequestException)
                {
                    ApplicationKiller.NetworkError();
                }
                if (exception is DatabaseException)
                {
                    ApplicationKiller.DatabaseError();
                }
                return false;
            }
            return response;
        }

        public static async Task<GetLoggedStateResponse> DoRequest(Task<GetLoggedStateResponse> request)
        {
            GetLoggedStateResponse response;
            try
            {
                response = await request;
            }
            catch (Exception exception)
            {
                if (exception is HttpRequestException)
                {
                    ApplicationKiller.NetworkError();
                }
                if (exception is DatabaseException)
                {
                    ApplicationKiller.DatabaseError();
                }
                return null;
            }
            return response;
        }
    }
}
