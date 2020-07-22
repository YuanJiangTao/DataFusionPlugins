using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSafetySystemProtocal.Models
{
    class AnalogStatisticModel
    {
        public int EquipId { get; set; }
        public int Index { get; set; }
        public string PointCode { get; set; }
        public int PointId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public float MinValue { get; set; }
        public DateTime MinTime { get; set; }
        public float MaxValue { get; set; }
        public DateTime MaxTime { get; set; }
        public float AvgValue { get; set; }
        public int ValidTime { get; set; }
        public int AlarmCount { get; set; }
        public int FeedCount { get; set; }
        public int Period { get; set; }
        public int EquipState { get; set; }
        public int Status { get; set; }
    }
}
