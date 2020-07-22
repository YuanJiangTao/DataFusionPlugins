using DataFusionProtocal.Interfaces.Utils;
using MineSafetySystemProtocal.Models;
using MineSafetySystemProtocal.Repositories;
using MineSafetySystemProtocal.YongMeiSanHeng.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSafetySystemProtocal.YongMeiSanHeng
{
    class Dev : Base
    {
        public Dev(ParseProtocal protocol, DataRepo dataRepo, Action<string> log)
            : base(protocol, dataRepo, log)
        {
            AnalogPointModels = dataRepo.GetAnalogPointModels();
            SwitchPointModels = dataRepo.GetSwitchPointModels();
            ControlPointModels = dataRepo.GetControlPointModels();
            EquipmentInfoModels = dataRepo.GetEquipmentInfoModels();
        }
        public override string FileName => "DEV";

        public List<AnalogPointModel> AnalogPointModels { get; private set; }
        public List<SwitchPointModel> SwitchPointModels { get; private set; }
        public List<ControlPointModel> ControlPointModels { get; private set; }
        public List<EquipmentInfoModel> EquipmentInfoModels { get; private set; }

        protected override void ParseContent(List<string> lines, DateTime realTime)
        {
            var lineContents = lines.Select(p => GetLines(p));
            var tempAnalogPoints = lineContents.Where(p => p.Length == 20).Select(p => new AnalogPoint(p, realTime)).Where(p => p.IsValid);
            if (tempAnalogPoints != null && tempAnalogPoints.Any())
            {
                AnalogPointModels.Clear();
                var analogPointModels = tempAnalogPoints.Select(p => p.AnalogPointModel).ToList();
                var analogPointDt = ListToDataTableHelper.Models2Datatable<AnalogPointModel>("AnalogPoint", analogPointModels);
                DataRepo.Dapper.BulkUpdate(analogPointDt, "AnalogPoint", "EquipId");
                AnalogPointModels.AddRange(analogPointModels);
            }
            var tempSwitchPoints = lineContents.Where(p => p.Length == 15).Select(p => new SwitchPoint(p, realTime)).Where(p => p.IsValid);
            if (tempSwitchPoints != null && tempSwitchPoints.Any())
            {
                SwitchPointModels.Clear();
                var switchPointModels = tempSwitchPoints.Select(p => p.SwitchPointModel).ToList();
                var switchPointDt = ListToDataTableHelper.Models2Datatable<SwitchPointModel>("SwitchPoint", switchPointModels);
                DataRepo.Dapper.BulkUpdate(switchPointDt, "SwitchPoint", "EquipId");
                SwitchPointModels.AddRange(switchPointModels);
            }
            var tempControlPoints = lineContents.Where(p => p.Length == 4).Select(p => new ControlPoint(p, realTime)).Where(p => p.IsValid);
            if (tempControlPoints != null && tempControlPoints.Any())
            {
                ControlPointModels.Clear();
                var controlPointModels = tempControlPoints.Select(p => p.ControlPointModel);
                var controlPointDt = ListToDataTableHelper.Models2Datatable<ControlPointModel>("ControlPoint", controlPointModels);
                DataRepo.Dapper.BulkUpdate(controlPointDt, "ControlPoint", "EquipId");
                ControlPointModels.AddRange(controlPointModels);
            }
            var equipmentInfoModels = tempAnalogPoints.Select(p => p.EquipmentInfo).Union(tempSwitchPoints.Select(p => p.EquipmentInfo)).Union(tempControlPoints.Select(p => p.EquipmentInfo));
            if (equipmentInfoModels.Any())
            {
                var equipmentInfoDt = ListToDataTableHelper.Models2Datatable<EquipmentInfoModel>("EquipmentInfo", equipmentInfoModels);
                DataRepo.Dapper.BulkUpdate(equipmentInfoDt, "EquipmentInfo", "EquipId");
                EquipmentInfoModels.AddRange(equipmentInfoModels);
            }
        }
    }
}
