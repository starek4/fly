using System.Collections.Generic;

namespace Shared.API.PostModels
{
    public class BasePostData
    {
        public Dictionary<string, string> Data { get; } = new Dictionary<string, string>();
    }
}
