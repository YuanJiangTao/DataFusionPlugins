using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSafetySystemProtocal.Models
{
    class AnalogRunModel
    {
        public int EquipId { get; set; }
        public string PointCode { get; set; }
        public int PointId { get; set; }
        public int Index { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Value { get; set; }
        public int ValueState { get; set; }
        public int FeedCount { get; set; }
        public int Count { get; set; }
        public int EquipState { get; set; }
        public int Status { get; set; }
    }
}
