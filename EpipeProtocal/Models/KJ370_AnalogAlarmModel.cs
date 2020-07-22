using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpipeProtocal.Models
{
    class KJ370_AnalogAlarmModel
    {
        public string PointID { get; set; }
        public int SubStationID { get; set; }
        public int PortNO { get; set; }
        public string PointName { get; set; }
        public string Location { get; set; }
        public float AlarmValue { get; set; }
        public float MaxValue { get; set; }
        public DateTime MaxValueTime { get; set; }
        public double MinValue { get; set; }
        public DateTime MinValueTime { get; set; }
        public float SumValue { get; set; }
        public int SumCount { get; set; }
        public float AvgValue { get; set; }
        public int AlarmState { get; set; }
        public int State { get; set; }
        public int AbnormalFeed { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int SpanTime { get; set; }
        public string Treatment { get; set; }
        public DateTime TreatmentTime { get; set; }
        public int Writer { get; set; }
    }
}
