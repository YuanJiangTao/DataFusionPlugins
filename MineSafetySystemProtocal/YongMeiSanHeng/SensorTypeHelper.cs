using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MineSafetySystemProtocal.YongMeiSanHeng
{
    class SensorTypeHelper
    {
        static SensorTypeHelper()
        {

        }
        private static List<(int sensorId, int targetSensorId, string sensorName, string etCode)> SensorPointCodes =
            new List<(int sensorId, int targetSensorId, string sensorName, string etCode)>()
            {
                (0,0,"自定义传感器","20500000"),
                (1,0x09,"一氧化碳","20500000"),
                (2,0x12,"风速","21100000"),
                (3,0x06,"温度","20400000"),
                (4,0x05,"甲烷","20100000"),
                (5,0x13,"风压","20305101"),
                (6,0x1C,"负压","20300000"),
                (7,0x18,"水位","21200000"),
                (9,0x60,"煤位","21300000"),
                (10,0x02,"流量","20202101"),
                (12,98,"硫化氢","21900000"),
                (13,0x0A,"氧气","20800000"),
                (14,0x0B,"二氧化碳","20700000"),
                (15,0x10,"粉尘","20900000"),
                (20,12,"湿度","21000000"),
                (21,20,"风量","21800000"),
                (25,0, "局扇","30000000"),
                (28,8,"开停","30100001"),
                (26,0x0F,"风门","30100004"),
                (27,0x0E,"开关","30100000"),
                (24,0x0D,"馈电","30100002"),
                (32,0x11,"烟雾","30100005"),
                (33,64,"断电器","40100001")
            };

        public static int GetTargetSensorId(int sensorId)
        {
            if (SensorPointCodes.Exists(p => p.sensorId == sensorId))
                return SensorPointCodes.FirstOrDefault(p => p.sensorId == sensorId).targetSensorId;
            return 0;
        }
        public static string GetTargetSensorName(int sensorId)
        {
            if (SensorPointCodes.Exists(p => p.sensorId == sensorId))
                return SensorPointCodes.FirstOrDefault(p => p.sensorId == sensorId).sensorName;
            return SensorPointCodes[0].sensorName;
        }
        public static (string SensorName,string EtCode) GetTargetSensorInfo(int sensorId)
        {
            if (SensorPointCodes.Exists(p => p.sensorId == sensorId))
            {
                var item = SensorPointCodes.FirstOrDefault(p => p.sensorId == sensorId);
                return (item.sensorName, item.etCode);
            }
            return (SensorPointCodes[0].sensorName, SensorPointCodes[0].etCode);
        }
    }
}
