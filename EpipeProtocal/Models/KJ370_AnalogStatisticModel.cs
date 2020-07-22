using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpipeProtocal.Models
{
    class KJ370_AnalogStatisticModel
    {
        public string PointID { get; set; }
        public string PointName { get; set; }
        public int SubStationID { get; set; }
        public int PortNO { get; set; }
        public string Location { get; set; }
        public string UnitName { get; set; }
        public string MonitoringValue { get; set; } = "";
        public int State { get; set; }
        public float MaxValue { get; set; }
        public DateTime MaxValueTime { get; set; }
        public float MinValue { get; set; }
        public DateTime MinValueTime { get; set; }
        public float SumValue { get; set; }
        public int SumCount { get; set; }
        public string AvgValue { get; set; } = "";
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
