using MineSafetySystemProtocal.Helper;
using MineSafetySystemProtocal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSafetySystemProtocal.YongMeiSanHeng.Model
{
    class AnalogPoint : BasePoint
    {
        public AnalogPoint(string[] segments, DateTime realTime)
        {
            if (segments == null || segments.Length != 20)
            {
                IsValid = false;
                return;
            }
            try
            {
                var pointCode = GetPointCode(segments[0]);  //0 测点编号
                var substationCode = GetPointCode(segments[1]); //1 分站号
                var pointName = segments[2];        //2 测点名称
                var pointLocation = segments[3];    //3 测点所属区域名称
                //var unitType = (UnitType)Enum.Parse(typeof(UnitType), segments[4]); //4 所在区域类型
                //var pointAreaLocation = (AreaLocation)Enum.Parse(typeof(AreaLocation), segments[5]);  //5 传感器所在区域的位置编码
                //var pointType = (PointType)Enum.Parse(typeof(PointType), segments[6]);      //6  测点值的类型编码
                var sensorTypeCode = segments[7];               //7   测点类型编码
                var unitName = segments[8];             //8     工程单位
                var measureHigh = segments[9].ToFloat();        //9        高量程
                var measureLow = segments[10].ToFloat();        //10       低量程
                var upperLimitWarning = segments[11].ToFloat();     //11        上限报警门限
                var lowerLimitWarning = segments[13].ToFloat();
                var upperLimitSwitchingOff = segments[15].ToFloat();
                var upperLimitResume = segments[16].ToFloat();
                var lowerLimitSwitchingOff = segments[17].ToFloat();
                var lowerLimitResume = segments[18].ToFloat();
                AnalogPointModel = new AnalogPointModel()
                {
                    EquipId = GetSensorEquipId(pointCode, PointTypeConverter.A),
                    UnitName = unitName,
                    SubstationId = substationCode.Substring(0, 3).ToInt(),
                    SubstationEquipId = substationCode.ToInt(),
                    PortNO = pointCode.Substring(3, 3).ToInt(),
                    SensorId = SensorTypeHelper.GetTargetSensorId(sensorTypeCode.ToInt()),
                    MeasureHigh = measureHigh,
                    MeasureLow = measureLow,
                    UpperLimitWarning = upperLimitWarning,
                    UpperLimitResume = upperLimitResume,
                    UpperLimitSwitchingOff = upperLimitSwitchingOff,
                    LowerLimitWarning = lowerLimitWarning,
                    LowerLimitResume = lowerLimitResume,
                    LowerLimitSwitchingOff = lowerLimitSwitchingOff
                };
                var sensorInfo = SensorTypeHelper.GetTargetSensorInfo(sensorTypeCode.ToInt());
                EquipmentInfo = new EquipmentInfoModel()
                {
                    EquipId = AnalogPointModel.EquipId,
                    PointCode = pointCode,
                    Name = sensorInfo.SensorName,
                    ETCode = sensorInfo.EtCode,
                    PointId = 0,
                    Location = pointLocation,
                    LDate = realTime,
                    RDate = realTime
                };
            }
            catch
            {
                IsValid = false;
                return;
            }
            IsValid = true;
        }
        public AnalogPointModel AnalogPointModel { get; private set; }

        public EquipmentInfoModel EquipmentInfo { get; private set; }
    }
}
