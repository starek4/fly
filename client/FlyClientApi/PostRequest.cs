using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Logger.Enviroment;
using Logger.Logging;

namespace FlyClientApi
{
    public class PostRequest
    {
        public async Task<string> DoRequest(HttpClient httpClient, string apiPath, string body)
        {
            ILogger logger = EnviromentHelper.GetLogger();
            var path = new Uri(BaseUrl.Path + apiPath);
            using (var content = new StringContent(body, Encoding.UTF8, "application/json"))
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
