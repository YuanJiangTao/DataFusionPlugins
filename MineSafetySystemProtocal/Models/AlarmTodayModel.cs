using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSafetySystemProtocal.Models
{
    class AlarmTodayModel
    {
        public int EquipId { get; set; }
        public int Index { get; set; }
        public string PointCode { get; set; }
        public int PointId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Value { get; set; }
        public int ValueState { get; set; }
        public int FeedState { get; set; }
        public int EquipState { get; set; }
        public int TreatType { get; set; }
        public string Treatment { get; set; }
        public DateTime TreatTime { get; set; }
        public string TreatUser { get; set; }
        public int Status { get; set; }
        public int IsSMS { get; set; }
    }
}
