using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpipeProtocal.Models
{
    class KJ370_FluxRealDataModel
    {
        public int FluxID { get; set; }
        public DateTime RealDate { get; set; }
        public int MethaneChromaState { get; set; }
        public string MethaneChromaRealValue { get; set; } = "";
        public int FluxState { get; set; }
        public string FluxRealValue { get; set; } = "";
        public int PressureState { get; set; }
        public string PressureRealValue { get; set; } = "";
        public int TemperatureState { get; set; }
        public string TemperatureRealValue { get; set; } = "";
        public int COState { get; set; }
        public string CORealValue { get; set; } = "";
        public string PureFluxRealValue { get; set; } = "";
        public string IndustrialFluxRealValue { get; set; } = "";
        public string FluxHour { get; set; }
        public string PureFluxHour { get; set; }
        public string IndustrialFluxHour { get; set; }
        public string IndustrialPureFluxHour { get; set; }
    }
}
