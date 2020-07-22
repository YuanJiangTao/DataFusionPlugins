using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSafetySystemProtocal.Models
{
    class RealDataModel
    {
        public int EquipId { get; set; }
        public int Index { get; set; }
        public string PointCode { get; set; }
        public int PointId { get; set; }
        public DateTime RealTime { get; set; }
        public string Value { get; set; }
        public int ValueState { get; set; }
        public int FeedState { get; set; }
        public int EquipState { get; set; }
        public int Status { get; set; }

        public void Update(string value)
        {
            this.Value = value;
        }

        public AnalogRunModel ToAnalogRunModel()
        {
            return new AnalogRunModel()
            {
                PointCode = PointCode,
                EquipId = EquipId,
                Index = Index,
                PointId = PointId,
                StartTime = RealTime,
                EndTime = RealTime,
                Value = Value,
                ValueState = ValueState,
                FeedCount = 0,
                Count = 1,
                EquipState = EquipState,
                Status = 1
            };
        }

        public SwitchRunModel ToSwitchRunModel()
        {
            return new SwitchRunModel()
            {
                PointCode = PointCode,
                EquipId = EquipId,
                Index = Index,
                PointId = PointId,
                StartTime = RealTime,
                EndTime = RealTime,
                Value = Value,
                ValueState = ValueState,
                FeedCount = 0,
                Count = 1,
                Status = 1
            };
        }
    }
}
