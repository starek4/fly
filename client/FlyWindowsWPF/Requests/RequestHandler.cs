using System;
using System.Net.Http;
using System.Threading.Tasks;
using FlyClientApi.Exceptions;
using FlyWindowsWPF.TrayIcon;
using Models;

namespace FlyWindowsWPF.Requests
{
    public static class RequestHandler
    {
        
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
                    if (!ErrorHandler.IsNetworkError)
                    {
                        ErrorHandler.NetworkError(trayController);
                        ErrorHandler.IsNetworkError = true;
                    }
                }
                if (exception is DatabaseException)
                {
                    ErrorHandler.DatabaseError();
                }
                return;
            }
            trayController.MakeIconRed();
            ErrorHandler.IsNetworkError = false;
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
                    if (!ErrorHandler.IsNetworkError)
                    {
                        ErrorHandler.NetworkError(trayController);
                        ErrorHandler.IsNetworkError = true;
                    }
                }
                if (exception is DatabaseException)
                {
                    ErrorHandler.DatabaseError();
                }
                return false;
            }
            trayController.MakeIconRed();
            ErrorHandler.IsNetworkError = false;
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
                    if (!ErrorHandler.IsNetworkError)
                    {
                        ErrorHandler.NetworkError(trayController);
                        ErrorHandler.IsNetworkError = true;
                    }
                }
                if (exception is DatabaseException)
                {
                    ErrorHandler.DatabaseError();
                }
                return null;
            }
            trayController.MakeIconRed();
            ErrorHandler.IsNetworkError = false;
            return response;
        }
    }
}
