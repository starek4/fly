using System;
using System.Net.Http;
using System.Threading.Tasks;
using DatabaseController.Models;
using FlyClientApi.Exceptions;

namespace FlyUnix
{
    public static class RequestHandler
    {
        public static void DoRequest(Action action)
        {
            try
            {
                action.Invoke();
            }
            catch (HttpRequestException)
            {
                ApplicationKiller.NetworkError();
            }
            catch (DatabaseException)
            {
                ApplicationKiller.DatabaseError();
            }
        }
        public static bool DoRequest(Task<bool> request)
        {
            bool response = false;
            try
            {
                response = request.Result;
            }
            catch (AggregateException exception)
            {
                exception.Handle(e =>
                {
                    if (e is HttpRequestException)
                    {
                        ApplicationKiller.NetworkError();
                        return true;
                    }
                    if (e is DatabaseException)
                    {
                        ApplicationKiller.DatabaseError();
                        return true;
                    }
                    return false;
                });
            }
            return response;
        }
        public static Device DoRequest(Task<Device> request)
        {
            Device response = null;
            try
            {
                response = request.Result;
            }
            catch (AggregateException exception)
            {
                exception.Handle(e =>
                {
                    if (e is HttpRequestException)
                    {
                        ApplicationKiller.NetworkError();
                        return true;
                    }
                    if (e is DatabaseException)
                    {
                        ApplicationKiller.DatabaseError();
                        return true;
                    }
                    return false;
                });
            }
            return response;
        }
    }
}
