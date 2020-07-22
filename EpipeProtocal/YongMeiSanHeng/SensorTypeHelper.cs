using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpipeProtocal.YongMeiSanHeng
{
    class SensorTypeHelper
    {

        private static List<(string sensorId, string equipCode, string sensorName, string unitName)> _sensorCodes =
            new List<(string sensorId, string equipCode, string sensorName, string unitName)>()
            {
                ("018","029999","其他",""),
                ("001","020008","管道瓦斯浓度","%"),
                ("002","020004","管道温度","℃"),
                ("003","020003","管道压力","kpa"),
                ("004","020002","流量","m3/min"),
                ("005","029901","泵站状态",""),
                ("006","029902","轴承温度","℃"),
                ("007","029903","电机温度","℃"),
                ("008","029904","电动阀开闭状态",""),
                ("009","029905","电动阀开度",""),
                ("010","029906","水泵开停状态",""),
                ("011","029907","水池水位","M"),
                ("012","029908","水温","℃"),
                ("013","020014","环境瓦斯浓度","%"),
                ("014","020013","环境温度","℃"),
                ("015","029909","电压","V"),
                ("016","029910","电流","A"),
                ("017","029911","分站状态",""),
            };
        public static (string SensorName, string EquipCode, string UnitName) GetTargetSensorInfo(string sensorId)
        {
            if (_sensorCodes.Exists(p => p.sensorId == sensorId))
            {
                var item = _sensorCodes.FirstOrDefault(p => p.sensorId == sensorId);
                return (item.sensorName, item.equipCode, item.unitName);
            }
            return (_sensorCodes[0].sensorName, _sensorCodes[0].equipCode, _sensorCodes[0].unitName);
        }
    }
}
