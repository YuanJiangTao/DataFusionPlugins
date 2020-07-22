using MineSafetySystemProtocal.Helper;
using MineSafetySystemProtocal.Models;
using System;

namespace MineSafetySystemProtocal.YongMeiSanHeng.Model
{
    class RealData : BasePoint
    {
        public RealData(string[] segments, DateTime now)
        {
            if (segments == null || segments.Length != 3)
            {
                IsValid = false;
                return;
            }
            try
            {
                var pointCode = GetPointCode(segments[0]);
                var originalValue = segments[1];
                var equipState = 0;
                var valueState = 0;
                var feedState = 0;
                if (Enum.TryParse<ProtocolState>(segments[2], out var protocalState))
                {
                    var result = GetRealValueState(pointCode, originalValue, protocalState);
                    equipState = result.EquipState;
                    valueState = result.ValueState;
                    feedState = result.FeedState;
                }
                RealDataModel = new RealDataModel()
                {
                    EquipId = GetSensorEquipId(pointCode),
                    Index = 0,
                    PointCode = pointCode,
                    PointId = 0,
                    RealTime = now,
                    Value = originalValue,
                    EquipState = equipState,
                    ValueState = valueState,
                    FeedState = feedState,
                    Status = 0
                };
            }
            catch
            {
                IsValid = false;
                return;
            }
            IsValid = true;
        }
        public RealDataModel RealDataModel { get; private set; }

    }
}
