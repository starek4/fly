using System.Collections.Generic;

namespace FlyApi.PostModels
{
    public class BasePostData
    {
        public Dictionary<string, string> Data { get; } = new Dictionary<string, string>();
    }
}
