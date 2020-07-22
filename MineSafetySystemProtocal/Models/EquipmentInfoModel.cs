using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSafetySystemProtocal.Models
{
    class EquipmentInfoModel
    {
        public int EquipId { get; set; }
        public string PointCode { get; set; }
        public string Name { get; set; }
        public string ETCode { get; set; }
        public int PointId { get; set; }
        public string VendorCode { get; set; } = "";
        public string SNCode { get; set; } = "";
        public int EquipState { get; set; }
        public int SrcFlag { get; set; }
        public string LocalFlag { get; set; } = "";
        public string AppCode { get; set; } = "";
        public string Config { get; set; } = "";
        public int Status { get; set; }
        public string OrgCode { get; set; } = "";
        public string Notes { get; set; } = "";
        public string RMan { get; set; } = "";
        public DateTime RDate { get; set; }
        public string LMan { get; set; } = "";
        public DateTime LDate { get; set; }
        public int IsReport { get; set; }
        public int IsUpload { get; set; } = 0;
        public string Location { get; set; }
        public int UnitId { get; set; }
    }
}
