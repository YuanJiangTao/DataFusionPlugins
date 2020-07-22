using MineSafetySystemProtocal.Helper;
using MineSafetySystemProtocal.Models;
using System;

namespace MineSafetySystemProtocal.YongMeiSanHeng.Model
{
    class SwitchPoint : BasePoint
    {
        public SwitchPoint(string[] segments, DateTime realTime)
        {
            if (segments == null || segments.Length != 15)
            {
                IsValid = false;
                return;
            }
            try
            {
                PointCode = GetPointCode(segments[0]);
                SubstationCode = GetPointCode(segments[1]);
                PointName = segments[2];
                PointLocation = segments[3];
                //UnitType = (UnitType)Enum.Parse(typeof(UnitType), segments[4]);
                //PointAreaLocation = (AreaLocation)Enum.Parse(typeof(AreaLocation), segments[5]);
                //PointType = (PointType)Enum.Parse(typeof(PointType), segments[6]);
                SensorTypeCode = segments[7];
                OnDescription = segments[8];
                OffDescription = segments[9];
                AlarmState = segments[10].ToInt();
                DismissAlarmState = segments[11].ToInt();
                SwitchOffState = segments[12].ToInt();
                ResumeState = segments[13].ToInt();
                ControlRelations = segments[14];
                SwitchPointModel = new SwitchPointModel()
                {
                    EquipId = GetSensorEquipId(PointCode, PointTypeConverter.D),
                    SubstationEquipId = SubstationCode.ToInt(),
                    SubstationId = SubstationCode.Substring(0, 3).ToInt(),
                    PortNO = PointCode.Substring(3, 3).ToInt(),
                    SensorId = SensorTypeHelper.GetTargetSensorId(SensorTypeCode.ToInt()),
                    State0Name = "断线",
                    State1Name = OnDescription,
                    State2Name = OffDescription,
                    State0Warning = true,
                    State1Warning = true,
                    State2Warning = true
                };
                var sensorInfo = SensorTypeHelper.GetTargetSensorInfo(SensorTypeCode.ToInt());
                EquipmentInfo = new EquipmentInfoModel()
                {
                    EquipId = SwitchPointModel.EquipId,
                    PointCode = PointCode,
                    Name = sensorInfo.SensorName,
                    ETCode = sensorInfo.EtCode,
                    PointId = 0,
                    Location = PointLocation,
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
        public string PointCode { get; set; }
        public string SubstationCode { get; set; }
        public string PointName { get; set; }
        public string PointLocation { get; set; }

        public UnitType UnitType { get; set; }
        public AreaLocation PointAreaLocation { get; set; }
        public PointType PointType { get; set; }
        public string SensorTypeCode { get; set; }
        public string OnDescription { get; set; }
        public string OffDescription { get; set; }
        public int AlarmState { get; set; }
        public int DismissAlarmState { get; set; }
        public int SwitchOffState { get; set; }
        public int ResumeState { get; set; }
        public string ControlRelations { get; set; }

        public SwitchPointModel SwitchPointModel { get; private set; }

        public EquipmentInfoModel EquipmentInfo { get; private set; }
    }
}
