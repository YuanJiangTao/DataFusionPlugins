
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpipeProtocal.Models
{
    class KJ370_RealDataModel
    {
        public string PointID { get; set; }
        public string PointName { get; set; }
        public int SubStationID { get; set; }
        public int PortNO { get; set; }
        public int PointType { get; set; }
        public string RealValue { get; set; }
        public DateTime RealDate { get; set; }
        public int RealState { get; set; }
        public int FeedState { get; set; }
    }
}
