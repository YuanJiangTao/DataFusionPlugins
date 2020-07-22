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
    class Rtdata : Base
    {
        private readonly Dev _dev;
        public Rtdata(ParseProtocal protocol, DataRepo dataRepo, Action<string> log, Dev dev)
            : base(protocol, dataRepo, log)
        {
            _dev = dev;
        }
        public override string FileName => "RTDATA";


        protected override void ParseContent(List<string> lines, DateTime realTime)
        {
            var lineContents = lines.Select(p => GetLines(p));
            var realDataList = lineContents.Where(p => p.Length == 3).Select(o => new RealData(o, realTime)).Where(q => q.IsValid).Select(p => p.RealDataModel).ToList();
            var analogRunList = new List<AnalogRunModel>();
            var switchRunList = new List<SwitchRunModel>();
            for (var i = 0; i < realDataList.Count(); i++)
            {
                var item = realDataList[i];
                if (item.PointCode.Contains("D"))
                {
                    var switchPoint = _dev.SwitchPointModels.FirstOrDefault(p => p.EquipId == item.EquipId);
                    if (switchPoint != null)
                    {
                        var realValue = GetSwitchStateName((PointState)item.ValueState, switchPoint);
                        item.Update(realValue);
                    }
                    switchRunList.Add(item.ToSwitchRunModel());
                }
                else if (item.PointCode.Contains("C"))
                {
                    var controlPoint = _dev.ControlPointModels.FirstOrDefault(p => p.EquipId == item.EquipId);
                    if (controlPoint != null)
                    {
                        var realValue = GetControlStateName((PointState)item.ValueState, controlPoint);
                        item.Update(realValue);
                    }
                    switchRunList.Add(item.ToSwitchRunModel());
                }
                else if (item.PointCode.Contains("A"))
                {
                    analogRunList.Add(item.ToAnalogRunModel());
                }
            }
            var dt = ListToDataTableHelper.Models2Datatable<RealDataModel>("RealData", realDataList.ToList());
            DataRepo.Dapper.BulkUpdate(dt, "RealData", "EquipId", "Index");

            var analogRunTableName = $"AnalogRun{realTime:yyyyMMdd}";
            var analogRunDt = ListToDataTableHelper.Models2Datatable<AnalogRunModel>(analogRunTableName, analogRunList);
            DataRepo.YearDapper.BulkUpdate(analogRunDt, analogRunTableName, "EquipId", "PointCode", "Index", "StartTime");

            var switchRunTableName = $"SwitchRun{realTime:yyyyMM}";
            var switchRunDt = ListToDataTableHelper.Models2Datatable<SwitchRunModel>(switchRunTableName, switchRunList);
            DataRepo.YearDapper.BulkUpdate(switchRunDt, switchRunTableName, "EquipId", "PointCode", "Index", "StartTime");
        }
        private string GetControlStateName(PointState valueState, ControlPointModel controlPointModel)
        {
            switch (valueState)
            {
                case PointState.Control_State0:
                    return controlPointModel.State0Name;
                case PointState.Control_State1:
                    return controlPointModel.State1Name;
                default:
                    return "未知";
            }
        }
        private string GetSwitchStateName(PointState valueState, SwitchPointModel switchPointModel)
        {
            switch (valueState)
            {
                case PointState.Switch_State0:
                    return switchPointModel.State0Name;
                case PointState.Switch_State1:
                    return switchPointModel.State1Name;
                case PointState.Switch_State2:
                    return switchPointModel.State2Name;
                default:
                    return "未知";
            }
        }
    }
}
