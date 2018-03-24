using System;
using System.Net.Http;
using System.Threading.Tasks;
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
                    if (e is HttpRequestException) // This we know how to handle.
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
