using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSafetySystemProtocal.Models
{
    class ControlPointModel
    {
        public int EquipId { get; set; }
        public int VoiceWarning { get; set; }
        public int SubstationEquipId { get; set; }
        public int SubstationId { get; set; }
        public int PortNO { get; set; }
        public int SensorId { get; set; }
        public int FeedEquipId { get; set; }
        public int FeedValue { get; set; }
        public string State0Name { get; set; } = "复电";
        public string State1Name { get; set; } = "断电";
        public bool State0Warning { get; set; }
        public bool State1Warning { get; set; }
        public string State0Color { get; set; } = "#00ff00";
        public string State1Color { get; set; } = "#ff0000";
        public int State0Logic { get; set; }
        public int State1Logic { get; set; }
    }
}
