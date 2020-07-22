using DataFusionProtocal.Interfaces.Helper;
using DataFusionProtocal.Interfaces.Utils;
using EpipeProtocal.Models;
using EpipeProtocal.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace EpipeProtocal.YongMeiSanHeng
{
    class Ycbjdata : Base
    {
        private readonly Dev _dev;
        public Ycbjdata(ParseProtocal protocol, DataRepo dataRepo, Action<string> log, Dev dev)
            : base(protocol, dataRepo, log)
        {
            _dev = dev;
        }
        public override string FileName => "YCBJDATA";

        protected override void ParseContent(List<string> lines, DateTime realTime)
        {
            var lineContents = lines.Select(p => GetLines(p));
            var tempLines = lineContents.Where(p => p.Length == 16 && p[0].Contains("A"));
            var models = new List<KJ370_AnalogAlarmModel>();
            foreach (var segments in tempLines)
            {
                var pointId = GetPointCode(segments[0]);
                var analogPoint = _dev.AnalogPointModels.FirstOrDefault(p => p.PointID == pointId);
                if (analogPoint == null) continue;
                models.Add(new KJ370_AnalogAlarmModel()
                {
                    PointID = pointId,
                    SubStationID = analogPoint.SubStationID,
                    PortNO = analogPoint.PortNO,
                    PointName = analogPoint.PointName,
                    Location = analogPoint.Location,
                    AlarmValue = segments[2].ToFloat(),
                    StartTime = segments[5].ToDateTime(),
                    EndTime = segments[6].ToUpper() == "X" ? DateTime.Now : segments[6].ToDateTime(),
                    MaxValue = segments[7].ToFloat(),
                    MaxValueTime = segments[8].ToDateTime(),
                    MinValueTime = segments[9].ToDateTime(),
                    AvgValue = segments[10].ToFloat(),
                    MinValue = segments[11].ToFloat(),
                    Treatment = segments[13],
                    TreatmentTime = segments[14].ToDateTime(),
                });
            }
            var tableName = $"KJ370_AnalogAlarm{realTime:yyyyMM}";
            var dt = ListToDataTableHelper.Models2Datatable<KJ370_AnalogAlarmModel>(tableName, models);
            DataRepo.YearDapper.BulkUpdate(dt, tableName, "FluxID", "StartTime");
        }
    }
}
