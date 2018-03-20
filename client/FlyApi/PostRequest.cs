using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Logger.Enviroment;
using Logger.Logging;

namespace FlyApi
{
    public class PostRequest
    {
        public async Task<string> DoRequest(HttpClient httpClient, string apiPath, Dictionary<string, string> body)
        {
            ILogger logger = EnviromentHelper.GetLogger();
            var path = new Uri(BaseUrl.Path + apiPath);
            using (var content = new FormUrlEncodedContent(body))
            {
                try
                {
                    using (var response = await httpClient.PostAsync(path, content))
                    {
                        return await response.Content.ReadAsStringAsync();
                    }
                }
                catch (HttpRequestException exception)
                {
                    logger?.Error("Error when connecting server: " + Environment.NewLine + exception);
                    throw new HttpRequestException("Error when communucating with network...");
                }
            }
        }
    }
}
