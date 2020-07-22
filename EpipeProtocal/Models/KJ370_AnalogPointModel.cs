using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpipeProtocal.Models
{
    class KJ370_AnalogPointModel
    {
        public string PointID { get; set; }
        public string PointName { get; set; }
        public int SubStationID { get; set; }
        public int PortNO { get; set; }
        public string Location { get; set; }
        public string EquipCode { get; set; }
        public string UnitName { get; set; }
        public double ErrorRand { get; set; }
        public string SignalSystemName { get; set; } = "";
        public int SignType { get; set; }
        public int RangeCode { get; set; }
        public int Precision { get; set; }
        public int MaxValue { get; set; }
        public int MidValue { get; set; }
        public int MinValue { get; set; }
        public double UpperLimitSwitchingOff { get; set; }
        public double UpperLimitWarning { get; set; }
        public double UpperLimitResume { get; set; }
        public double UpperLimitEarlyWarning { get; set; }
        public int UpperLimitSwitchingOffPort { get; set; }
        public int UpperLimitWarningPort { get; set; }
        public double LowerLimitSwitchingOff { get; set; }
        public double LowerLimitWarning { get; set; }
        public double LowerLimitResume { get; set; }
        public double LowerLimitEarlyWarning { get; set; }
        public int LowerLimitSwitchingOffPort { get; set; }
        public int LowerLimitWarningPort { get; set; }
        public int OverFlowPort { get; set; }
        public int LineBreakPort { get; set; }
        public int NegativeFleePort { get; set; }
        public bool IsRunLog { get; set; }
        public bool IsUsed { get; set; }
        public bool IsVoiceWarning { get; set; }
        public bool IsDenseData { get; set; }
        public string Notes { get; set; } = "";
        public int RMan { get; set; }
        public DateTime RDate { get; set; }
        public int LMan { get; set; }
        public DateTime LDate { get; set; }
        public string Config { get; set; }
        public int SerialNO { get; set; }
        public string SoftVersion { get; set; } = "";
        public string ProVersion { get; set; } = "";
        public string EquipFact { get; set; } = "";
        public int SystemTick { get; set; }
        public float Power { get; set; }
        public int CommDist { get; set; }
        public string InstallLocation { get; set; } = "";
        public string SensorName { get; set; } = "";
    }
}
