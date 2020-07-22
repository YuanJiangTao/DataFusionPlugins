using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpipeProtocal.Models
{
    class KJ370_FluxPointModel
    {
        public int FluxID { get; set; }
        public int SubStationID { get; set; }
        public string Location { get; set; }
        public string FluxName { get; set; }
        public int ConcentrationPort { get; set; }
        public int FluxPort { get; set; }
        public int PressurePort { get; set; }
        public int TemperaturePort { get; set; }
        public int COPort { get; set; }
        public int PressureFlag { get; set; }
        public double StandardatMosphere { get; set; }
        public double FluxTotal { get; set; }
        public double PureFluxTotal { get; set; }
        public double IndustrialFluxTotal { get; set; }
        public string Notes { get; set; } = "";
        public int RMan { get; set; }
        public DateTime RDate { get; set; }
        public int LMan { get; set; }
        public DateTime LDate { get; set; }
        public double PipeDiameter { get; set; }
        public int IsReport { get; set; }
        public int OrderID { get; set; }
        public int IsUsed { get; set; }
        public int IsIndustrialFlux { get; set; }
    }
}
