using System;
using System.Net.Http;
using System.Threading.Tasks;
using DatabaseController.Models;
using FlyClientApi.Exceptions;
using FlyWindowsWPF.TrayIcon;

namespace FlyWindowsWPF.Requests
{
    public static class RequestHandler
    {
        private static bool _isNetworkError;
        public static async Task DoRequest(Task request, TrayController trayController)
        {
            try
            {
                await request;
            }
            catch (Exception exception)
            {
                if (exception is HttpRequestException)
                {
                    if (!_isNetworkError)
                    {
                        ErrorHandler.NetworkError(trayController);
                        _isNetworkError = true;
                    }
                }
                if (exception is DatabaseException)
                {
                    ErrorHandler.DatabaseError();
                }
            }
            trayController.MakeIconRed();
            _isNetworkError = false;
        }

        public static async Task<bool> DoRequest(Task<bool> request, TrayController trayController)
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
                    if (!_isNetworkError)
                    {
                        ErrorHandler.NetworkError(trayController);
                        _isNetworkError = true;
                    }
                }
                if (exception is DatabaseException)
                {
                    ErrorHandler.DatabaseError();
                }
                return false;
            }
            trayController.MakeIconRed();
            _isNetworkError = false;
            return response;
        }

        public static async Task<Device> DoRequest(Task<Device> request, TrayController trayController)
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
                    if (!_isNetworkError)
                    {
                        ErrorHandler.NetworkError(trayController);
                        _isNetworkError = true;
                    }
                }
                if (exception is DatabaseException)
                {
                    ErrorHandler.DatabaseError();
                }
                return null;
            }
            trayController.MakeIconRed();
            _isNetworkError = false;
            return response;
        }
    }
}
