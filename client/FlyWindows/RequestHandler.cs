using System;
using System.Net.Http;
using System.Threading.Tasks;
using Shared.API.Exceptions;

namespace FlyWindows
{
    public static class RequestHandler
    {
        public static void DoRequest(Action action)
        {
            try
            {
                action.Invoke();
            }
            catch (HttpRequestException exception)
            {
                // TODO: Handle HTTP request exception...
                throw new NotImplementedException(exception.Message);
            }
            catch (DatabaseException exception)
            {
                // TODO: Handle database request exception...
                throw new NotImplementedException(exception.Message);
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
                        // TODO: Handle HTTP request exception...
                        return true;
                    }
                    if (e is DatabaseException)
                    {
                        // TODO: Handle database request exception...
                        return true;
                    }
                    return false;
                });
            }
            return response;
        }
    }
}
