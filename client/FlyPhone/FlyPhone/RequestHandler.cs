using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FlyClientApi.Exceptions;
using Models;

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

        public static async Task<string> DoRequest(Task<string> request)
        {
            string response;
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

        public static async Task<Device> DoRequest(Task<Device> request)
        {
            Device response;
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

        public static async Task<List<Device>> DoRequest(Task<List<Device>> request)
        {
            List<Device> response;
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
