using MineSafetySystemProtocal.Helper;
using MineSafetySystemProtocal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSafetySystemProtocal.YongMeiSanHeng.Model
{
    class ControlPoint : BasePoint
    {
        public ControlPoint(string[] segments, DateTime realTime)
        {
            if (segments == null || segments.Length != 4)
            {
                IsValid = false;
                return;
            }
            try
            {
                var pointCode = GetPointCode(segments[0]);  //0 测点编号
                //var pointType = (PointType)Enum.Parse(typeof(PointType), segments[6]);      //1  测点值的类型编码
                var pointName = segments[2];        //2 测点名称
                var sensorTypeCode = segments[3];               //3   测点类型编码
                ControlPointModel = new ControlPointModel()
                {
                    EquipId = GetSensorEquipId(pointCode, PointTypeConverter.C),
                    SubstationEquipId = pointCode.Replace("C", "0").ToInt(),
                    SubstationId = pointCode.Substring(0, 3).ToInt(),
                    PortNO = pointCode.Substring(3, 3).ToInt(),
                    SensorId = SensorTypeHelper.GetTargetSensorId(sensorTypeCode.ToInt()),
                };
                var sensorInfo = SensorTypeHelper.GetTargetSensorInfo(sensorTypeCode.ToInt());
                EquipmentInfo = new EquipmentInfoModel()
                {
                    EquipId = ControlPointModel.EquipId,
                    PointCode = pointCode,
                    Name = sensorInfo.SensorName,
                    ETCode = sensorInfo.EtCode,
                    PointId = 0,
                    Location = "",
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
        public ControlPointModel ControlPointModel { get; private set; }

        public EquipmentInfoModel EquipmentInfo { get; private set; }
    }
}
