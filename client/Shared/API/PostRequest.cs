using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Shared.Enviroment;
using Shared.Logging;

namespace Shared.API
{
    public static class PostRequest
    {
        public static async Task<string> DoRequest(string apiPath, Dictionary<string, string> body)
        {
            ILogger logger = EnviromentHelper.GetLogger();
            var baseAddress = new Uri("https://fly.starekit.cz/api/" + apiPath);
            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                using (var content = new FormUrlEncodedContent(body))
                {
                    try
                    {
                        using (var response = await httpClient.PostAsync(baseAddress, content))
                        {
                            return await response.Content.ReadAsStringAsync();
                        }
                    }
                    catch (HttpRequestException exception)
                    {
                        logger.Error("Error when connecting server: " + exception);
                        throw new HttpRequestException("Error when communucating with network...");
                    }
                }
            }
        }
    }
}
