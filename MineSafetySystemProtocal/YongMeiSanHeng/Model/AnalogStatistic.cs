using MineSafetySystemProtocal.Helper;
using MineSafetySystemProtocal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSafetySystemProtocal.YongMeiSanHeng.Model
{
    class AnalogStatistic : BasePoint
    {
        public AnalogStatistic()
        {

        }
        public AnalogStatistic(string[] segments, DateTime now)
        {
            if (segments == null || segments.Length != 6)
            {
                IsValid = false;
                return;
            }
            try
            {
                var pointCode = GetPointCode(segments[0]);
                var avgValue = segments[1].ToFloat();
                var maxValue = segments[2].ToFloat();
                var maxTime = segments[3].ToDateTime();
                var minValue = segments[4].ToFloat();
                var minTime = segments[5].ToDateTime();

                AnalogStatisticModel = new AnalogStatisticModel()
                {
                    EquipId = GetSensorEquipId(pointCode),
                    Index = 0,
                    PointCode = pointCode,
                    StartTime = now,
                    EndTime = now.AddMinutes(5),
                    MinValue = minValue,
                    MinTime = minTime,
                    MaxValue = maxValue,
                    MaxTime = maxTime,
                    AvgValue = avgValue,
                    ValidTime = 300,
                    AlarmCount = 0,
                    FeedCount = 301002,
                    Period = 300,
                    EquipState = 1030000,
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
        public AnalogStatisticModel AnalogStatisticModel { get; private set; }
    }
}
