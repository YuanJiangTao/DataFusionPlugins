using MineSafetySystemProtocal.Helper;
using MineSafetySystemProtocal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MineSafetySystemProtocal.YongMeiSanHeng.Model
{
    class AlarmToday : BasePoint
    {
        private PointTypeConverter _pointType = PointTypeConverter.D;
        public AlarmToday(string[] segments, DateTime now)
        {
            if (segments == null || segments.Length != 16)
            {
                IsValid = false;
                return;
            }
            try
            {
                var pointCode = GetPointCode(segments[0]);
                if (pointCode.Contains("A")) _pointType = PointTypeConverter.A;
                var alarmState = ConvertAlarmState((ProtocalAlarmState)segments[1].ToInt(), pointCode);
                var realValue = segments[2];
                var alarmLimit = segments[3];
                var disAlarmLimit = segments[4];
                var startTime = segments[5].ToDateTime();
                var endTime = segments[6] == "X" ? now : segments[6].ToDateTime();
                var maxValue = segments[7];
                var maxTime = segments[8];
                var minTime = segments[9];
                var avgValue = segments[10];
                var minValue = segments[11];
                AlarmTodayModel = new AlarmTodayModel()
                {
                    EquipId = GetSensorEquipId(pointCode),
                    Index = 0,
                    PointCode = pointCode,
                    PointId = 0,
                    StartTime = startTime,
                    EndTime = endTime,
                    Value = realValue,
                    ValueState = alarmState.ValueState,
                    FeedState = 0,
                    EquipState = alarmState.EquipState,
                    TreatType = 0,
                    Treatment = segments[13],
                    TreatTime = segments[14].ToDateTime(),
                    TreatUser = segments[15],
                    Status = segments[6] == "X" ? 0 : 1
                };
                if (_pointType == PointTypeConverter.A)
                {
                    AnalogAlarmModel = new AnalogAlarmModel()
                    {
                        EquipId = GetSensorEquipId(pointCode),
                        Index = 0,
                        PointCode = pointCode,
                        PointId = 0,
                        StartTime = startTime,
                        EndTime = endTime,
                        Value = realValue.ToFloat(),
                        ValueState = alarmState.ValueState,
                        MaxValue = maxValue.ToFloat(),
                        MaxTime = maxTime.ToDateTime(),
                        MinValue = minValue.ToFloat(),
                        MinTime = minTime.ToDateTime(),
                        AvgValue = avgValue.ToFloat(),
                        FeedCount = 0,
                        EquipState = alarmState.EquipState,
                        Status = segments[6] == "X" ? 0 : 1
                    };
                }
            }
            catch
            {
                IsValid = false;
                return;
            }
            IsValid = true;

        }
        public bool IsAnalogAlarm => _pointType == PointTypeConverter.A;

        public AlarmTodayModel AlarmTodayModel { get; private set; }

        public AnalogAlarmModel AnalogAlarmModel { get; private set; }

        static (int EquipState, int ValueState) ConvertAlarmState(ProtocalAlarmState protocalAlarmState, string pointCode)
        {
            var equipState = 0;
            if (pointCode.Contains("A"))
                equipState = 1030000;
            var valueState = PointState.SubStation_OFF;
            switch (protocalAlarmState)
            {
                case ProtocalAlarmState.超限报警:
                    valueState = PointState.Analog_UpperLimitWarning;
                    break;
                case ProtocalAlarmState.断电报警:
                    valueState = PointState.Analog_UpperLimitSwitchingOff;
                    break;
                case ProtocalAlarmState.馈电异常:
                    valueState = PointState.Control_AbnormalFeed;
                    break;
                case ProtocalAlarmState.传感器断线:
                    if (pointCode.Contains("A"))
                        valueState = PointState.Analog_OFF;
                    else if (pointCode.Contains("D"))
                        valueState = PointState.Switch_State0;
                    else if (pointCode.Contains("C"))
                        valueState = PointState.Control_State0;
                    break;
                case ProtocalAlarmState.分站断电:
                    valueState = PointState.SubStation_OFF;
                    break;
                case ProtocalAlarmState.分站不通:
                    valueState = PointState.SubStation_OFF;
                    break;
                case ProtocalAlarmState.标校:
                    valueState = PointState.Analog_UpperLimitWarning;
                    equipState = +1;
                    break;
                case ProtocalAlarmState.超量程:
                    valueState = PointState.Analog_Overflow;
                    break;
                default:
                    break;
            }
            return (equipState, (int)valueState);
        }

    }
}
