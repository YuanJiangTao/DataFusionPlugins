using DataFusionProtocal.Interfaces.Helper;
using DataFusionProtocal.Interfaces.Utils;
using EpipeProtocal.Models;
using EpipeProtocal.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpipeProtocal.YongMeiSanHeng
{
    class Fzdata : Base
    {
        private readonly Dev _dev;
        private readonly Rtdata _rtdata;
        public Fzdata(ParseProtocal protocol, DataRepo dataRepo, Action<string> log, Dev dev, Rtdata rtdata)
            : base(protocol, dataRepo, log)
        {
            _dev = dev;
            _rtdata = rtdata;
        }
        public override string FileName => "FZDATA";

        protected override void ParseContent(List<string> lines, DateTime realTime)
        {
            var lineContents = lines.Select(p => GetLines(p));
            var tempDataList = lineContents.Where(p => p.Length == 6).Select(p => GetAnalogStatisticModel(p, realTime)).Where(p => p != default);
            var tableName = $"KJ370_AnalogStatistic{realTime:yyyyMMdd}";
            var dt = ListToDataTableHelper.Models2Datatable<KJ370_AnalogStatisticModel>(tableName, tempDataList);
            DataRepo.YearDapper.BulkUpdate(dt, tableName, "PointID", "StartTime");
        }
        private KJ370_AnalogStatisticModel GetAnalogStatisticModel(string[] segments, DateTime now)
        {
            KJ370_AnalogStatisticModel model = default;
            try
            {
                var pointId = GetPointCode(segments[0]);
                var realData = _rtdata.RealDatas.FirstOrDefault(p => p.PointID == pointId);
                var analogPoint = _dev.AnalogPointModels.FirstOrDefault(p => p.PointID == pointId);
                model = new KJ370_AnalogStatisticModel()
                {
                    PointID = pointId,
                    PointName = analogPoint?.PointName,
                    SubStationID = pointId.Substring(0, 3).ToInt(),
                    PortNO = pointId.Substring(3).ToInt(),
                    Location = analogPoint?.Location,
                    UnitName = analogPoint?.UnitName,
                    MonitoringValue = realData == null ? "" : realData.RealValue,
                    State = realData != null ? realData.RealState : 0,
                    AvgValue = segments[1],
                    MaxValue = segments[2].ToFloat(),
                    MaxValueTime = segments[3].ToDateTime(),
                    MinValue = segments[4].ToFloat(),
                    MinValueTime = segments[5].ToDateTime(),
                    StartTime = now,
                    EndTime = now.AddMinutes(5),
                };
            }
            catch
            {

            }
            return model;
        }
    }
}
