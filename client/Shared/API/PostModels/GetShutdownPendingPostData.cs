﻿using Shared.API.Enums;
using Shared.API.Mappers;

namespace Shared.API.PostModels
{
    public class GetShutdownPendingPostData : BasePostData
    {
        public GetShutdownPendingPostData(string deviceId)
        {
            Data.Add(DataTypeMapper.GetPath(DataTypes.DeviceId), deviceId);
        }
    }
}
