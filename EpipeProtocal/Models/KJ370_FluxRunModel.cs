using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpipeProtocal.Models
{
    class KJ370_FluxRunModel
    {
        public int FluxID { get; set; }
        public int SubStationID { get; set; }
        public string Location { get; set; }
        public string FluxName { get; set; }
        public int ConcentrationPort { get; set; }
        public int FluxPort { get; set; }
        public int PressurePort { get; set; }
        public int TemperaturePort { get; set; }
        public bool PressureFlag { get; set; }
        public double StandardatMosphere { get; set; }
        public double MethaneChromaMax { get; set; }
        public DateTime MethaneChromaMaxTime { get; set; }
        public double TemperatureMax { get; set; }
        public DateTime TemperatureMaxTime { get; set; }
        public double PressureMax { get; set; }
        public DateTime PressureMaxTime { get; set; }
        public double FluxMax { get; set; }
        public DateTime FluxMaxTime { get; set; }
        public double MethaneChromaMin { get; set; }
        public DateTime MethaneChromaMinTime { get; set; }
        public double TemperatureMin { get; set; }
        public DateTime TemperatureMinTime { get; set; }
        public double PressureMin { get; set; }
        public DateTime PressureMinTime { get; set; }
        public double FluxMin { get; set; }
        public DateTime FluxMinTime { get; set; }
        public double FluxTotal { get; set; }
        public double PureFluxTotal { get; set; }
        public double IndustrialFluxTotal { get; set; }
        public double IndustrialPureFluxTotal { get; set; }
        public double MethaneChromaSum { get; set; }
        public double TemperatureSum { get; set; }
        public double PressureSum { get; set; }
        public double FluxSum { get; set; }
        public int CountSum { get; set; }
        public int SpanTime { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public int Flag { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
