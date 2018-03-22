using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FlyApi.Exceptions;
using FlyApi.ResponseModels;

namespace FlyPhone
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
                    ExceptionHandler.NetworkError();
                }
                if (exception is DatabaseException)
                {
                    ExceptionHandler.DatabaseError();
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
                    ExceptionHandler.NetworkError();
                }
                if (exception is DatabaseException)
                {
                    ExceptionHandler.DatabaseError();
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
                    ExceptionHandler.NetworkError();
                }
                if (exception is DatabaseException)
                {
                    ExceptionHandler.DatabaseError();
                }
                return null;
            }
            return response;
        }

        public static async Task<IEnumerable<Device>> DoRequest(Task<IEnumerable<Device>> request)
        {
            IEnumerable<Device> response;
            try
            {
                response = await request;
            }
            catch (Exception exception)
            {
                if (exception is HttpRequestException)
                {
                    ExceptionHandler.NetworkError();
                }
                if (exception is DatabaseException)
                {
                    ExceptionHandler.DatabaseError();
                }
                return null;
            }
            return response;
        }
    }
}
