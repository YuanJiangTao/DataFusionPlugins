using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSafetySystemProtocal.Models
{
    class AnalogPointModel
    {
        public int EquipId { get; set; }
        public string UnitName { get; set; }
        public float ErrorRand { get; set; }
        public int VoiceWarning { get; set; }
        public int SubstationEquipId { get; set; }
        public int SubstationId { get; set; }
        public int PortNO { get; set; }
        public int SignalId { get; set; }
        public int RangeId { get; set; }
        public int ControlType { get; set; }
        public bool IsUpperWarning { get; set; }
        public bool IsUpperSwitchingOff { get; set; }
        public float UpperLimitSwitchingOff { get; set; }
        public float UpperLimitResume { get; set; }
        public float UpperLimitWarning { get; set; }
        public bool IsLowerWarning { get; set; }
        public bool IsLowerSwitchingOff { get; set; }
        public float LowerLimitSwitchingOff { get; set; }
        public float LowerLimitResume { get; set; }
        public float LowerLimitWarning { get; set; }
        public int SensorId { get; set; }
        public int Channel { get; set; }
        public int AlarmLevel { get; set; }
        public float MeasureLow { get; set; }
        public float MeasureMid { get; set; }
        public float MeasureHigh { get; set; }
        public int ComDist { get; set; }
        public bool SyncSwitch { get; set; }
        public float AreaSwitchingOff { get; set; }
        public float AreaResume { get; set; }
        public byte WindCH4TKJ { get; set; }
        public byte WindCH4AMN { get; set; }
    }
}
