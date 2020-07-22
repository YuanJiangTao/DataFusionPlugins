using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSafetySystemProtocal.Models
{
    class SwitchPointModel
    {
        public int EquipId { get; set; }
        public int VoiceWarning { get; set; }
        public int SubstationEquipId { get; set; }
        public int SubstationId { get; set; }
        public int PortNO { get; set; }
        public int SignalId { get; set; }
        public int SensorId { get; set; }
        public string State0Name { get; set; }
        public string State1Name { get; set; }
        public string State2Name { get; set; }
        public bool State0Warning { get; set; }
        public bool State1Warning { get; set; }
        public bool State2Warning { get; set; }
        public string State0Color { get; set; } = "#ff0000";
        public string State1Color { get; set; } = "#ffff00";
        public string State2Color { get; set; } = "#00ff00";
        public int State0Logic { get; set; }
        public int State1Logic { get; set; }
        public int State2Logic { get; set; }
        public int SwitchOnPort { get; set; }
        public int SwitchOffPort { get; set; }
        public int LineBreakPort { get; set; }
        public bool SyncSwitch { get; set; }
        public byte WindCH4TKJ { get; set; }
        public byte WindCH4AMN { get; set; }

    }
}
