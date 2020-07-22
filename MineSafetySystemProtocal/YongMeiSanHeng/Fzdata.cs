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
    class Fzdata : Base
    {
        private readonly Dev _dev;
        public Fzdata(ParseProtocal protocol, DataRepo dataRepo, Action<string> log, Dev dev)
            : base(protocol, dataRepo, log)
        {
            _dev = dev;
        }
        public override string FileName => "FZDATA";

        protected override void ParseContent(List<string> lines, DateTime realTime)
        {
            var lineContents = lines.Select(p => GetLines(p));
            var tempDataList = lineContents.Where(p => p.Length == 6).Select(o => new AnalogStatistic(o, realTime)).Where(q => q.IsValid).Select(p => p.AnalogStatisticModel);
            var analogStatisticDataList = from t in tempDataList
                                          join a in _dev.AnalogPointModels
                                          on t.EquipId equals a.EquipId
                                          select t;
            var tableName = $"AnalogStatistic{realTime:yyyyMMdd}";
            var dt = ListToDataTableHelper.Models2Datatable<AnalogStatisticModel>(tableName, analogStatisticDataList.ToList());
            DataRepo.YearDapper.BulkUpdate(dt, tableName, "EquipId", "Index", "PointCode", "StartTime");
        }


    }
}
