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
    class Dev : Base
    {
        public Dev(ParseProtocal protocol, DataRepo dataRepo, Action<string> log)
            : base(protocol, dataRepo, log)
        {
            AnalogPointModels = dataRepo.GetKJ370_AnalogPointModels();
            FluxPointModels = dataRepo.GetKJ370_FluxPointModels();
        }

        public List<KJ370_AnalogPointModel> AnalogPointModels { get; private set; }
        public List<KJ370_FluxPointModel> FluxPointModels { get; private set; }

        public override string FileName => "DEV";

        protected override void ParseContent(List<string> lines, DateTime realTime)
        {
            var lineContents = lines.Select(p => GetLines(p));
            var tempAnalogPoints = lineContents.Where(p => p.Length == 20).Select(GetAnalogPointModel).Where(p => p != default);
            if (tempAnalogPoints != null && tempAnalogPoints.Any())
            {
                AnalogPointModels.Clear();
                var dt = ListToDataTableHelper.Models2Datatable<KJ370_AnalogPointModel>("KJ370_AnalogPoint", tempAnalogPoints);
                DataRepo.Dapper.BulkUpdate(dt, "KJ370_AnalogPoint", "PointId");
                AnalogPointModels.AddRange(tempAnalogPoints);
            }
            var tempFluxPointsLines = lineContents.Where(p => p.Length == 7);
            var fluxFluxPoints = GetKJ370_FluxPointModels(tempFluxPointsLines);
            if (fluxFluxPoints.Any())
            {
                FluxPointModels.Clear();
                var dt = ListToDataTableHelper.Models2Datatable<KJ370_FluxPointModel>("KJ370_FluxPoint", fluxFluxPoints);
                DataRepo.Dapper.BulkUpdate(dt, "KJ370_FluxPoint", "FluxId");
                FluxPointModels.AddRange(fluxFluxPoints);
            }
        }
        private List<KJ370_FluxPointModel> GetKJ370_FluxPointModels(IEnumerable<string[]> lines)
        {
            var list = new List<KJ370_FluxPointModel>();
            var now = DateTime.Now;
            if (lines != null && lines.Any())
            {
                foreach (var line in lines)
                {
                    try
                    {
                        if (line == null || line.Length != 7) continue;
                        var model = new KJ370_FluxPointModel()
                        {
                            FluxID = GetFluxId(line[0]).ToInt(),
                            SubStationID = line[1].ToInt(),
                            Location = line[3],
                            FluxName = line[3],
                            IsUsed = 1,
                            LDate = now,
                            RDate = now
                        };
                        if (!list.Exists(p => p.FluxID == model.FluxID))
                        {
                            list.Add(model);
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
            return list;
        }
        public static KJ370_AnalogPointModel GetAnalogPointModel(string[] segments)
        {
            KJ370_AnalogPointModel model = default;
            var now = DateTime.Now;
            try
            {
                if (segments == null || segments.Length != 20)
                {
                    return model;
                }
                var pointId = GetPointCode(segments[0]);
                var pointName = segments[2];        //2 测点名称
                var pointLocation = segments[3];    //3 测点所属区域名称
                var sensorInfo = SensorTypeHelper.GetTargetSensorInfo(segments[7]);
                model = new KJ370_AnalogPointModel()
                {
                    PointID = pointId,
                    SubStationID = segments[1].ToInt(),
                    PortNO = pointId.Substring(3, 3).ToInt(),
                    PointName = pointName,
                    Location = pointLocation,
                    EquipCode = sensorInfo.EquipCode,
                    UnitName = segments[8],
                    MaxValue = segments[9].ToInt(),
                    MinValue = segments[10].ToInt(),
                    UpperLimitWarning = segments[11].ToFloat(),
                    UpperLimitResume = segments[12].ToFloat(),
                    LowerLimitWarning = segments[13].ToFloat(),
                    LowerLimitResume = segments[14].ToFloat(),
                    UpperLimitSwitchingOff = segments[15].ToFloat(),
                    LowerLimitSwitchingOff = segments[17].ToFloat(),
                    LDate = now,
                    RDate = now
                };
            }
            catch
            {
            }
            return model;
        }
    }
}
