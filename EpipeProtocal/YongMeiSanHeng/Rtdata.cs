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
    class Rtdata : Base
    {
        private readonly Dev _dev;
        public Rtdata(ParseProtocal protocol, DataRepo dataRepo, Action<string> log, Dev dev)
            : base(protocol, dataRepo, log)
        {
            _dev = dev;
            RealDatas = new List<KJ370_RealDataModel>();
            _startTime = DateTime.Now;
            FluxRealDataModels = new List<KJ370_FluxRealDataModel>();
        }
        public List<KJ370_RealDataModel> RealDatas { get; private set; }

        public override string FileName => "RTDATA";

        private DateTime _startTime;

        public List<KJ370_FluxRealDataModel> FluxRealDataModels { get; private set; }
        protected override void ParseContent(List<string> lines, DateTime realTime)
        {
            if (_startTime.Hour != realTime.Hour)
            {
                FluxRealDataModels.Clear();
                _startTime = realTime;
            }

            var lineContents = lines.Select(p => GetLines(p));
            var realDatas = lineContents.Where(p => p.Length == 3 && p[0].Contains("A")).Select(p => GetRealData(p, realTime));
            if (realDatas != null && realDatas.Any())
            {
                RealDatas.Clear();
                RealDatas.AddRange(realDatas);
            }
            foreach (var fluxPoint in _dev.FluxPointModels)
            {
                var tempList = lineContents.Where(p => p.Length == 3 && p[0].Contains("Q") && GetPointCode(p[0]).Substring(1, 3).ToInt() == fluxPoint.FluxID);
                if (!tempList.Any()) continue;

                var fluxTotal = GetFluxTotalInfo(tempList, FluxTypeFlag.FluxTotal);
                var pureFluxTotal = GetFluxTotalInfo(tempList, FluxTypeFlag.PureFluxTotal);
                var industrialFluxTotal = GetFluxTotalInfo(tempList, FluxTypeFlag.IndustrialFluxTotal);
                var industrialPureFluxTotal = GetFluxTotalInfo(tempList, FluxTypeFlag.IndustrialPureFluxTotal);

                var fluxRealDataCache = FluxRealDataModels.FirstOrDefault(p => p.FluxID == fluxPoint.FluxID);
                if (fluxRealDataCache != null)
                {
                    fluxRealDataCache.FluxHour = GetFluxSum(fluxRealDataCache.FluxHour, fluxTotal);
                    fluxRealDataCache.PureFluxHour = GetFluxSum(fluxRealDataCache.FluxHour, pureFluxTotal);
                    fluxRealDataCache.IndustrialFluxHour = GetFluxSum(fluxRealDataCache.IndustrialFluxHour, industrialFluxTotal);
                    fluxRealDataCache.IndustrialPureFluxHour = GetFluxSum(fluxRealDataCache.IndustrialPureFluxHour, industrialPureFluxTotal);
                }
                else
                {
                    FluxRealDataModels.Add(new KJ370_FluxRealDataModel()
                    {
                        FluxID = fluxPoint.FluxID,
                        RealDate = realTime,
                        FluxHour = fluxTotal,
                        PureFluxHour = pureFluxTotal,
                        IndustrialFluxHour = industrialFluxTotal,
                        IndustrialPureFluxHour = industrialPureFluxTotal
                    });
                }
            }
            var tableName = $"KJ370_FluxRealData";
            var dt = ListToDataTableHelper.Models2Datatable<KJ370_FluxRealDataModel>(tableName, FluxRealDataModels);
            DataRepo.Dapper.BulkUpdate(dt, tableName, "FluxID");
            var realDataTableName = "KJ370_RealData";
            var realDataDt = ListToDataTableHelper.Models2Datatable<KJ370_RealDataModel>(realDataTableName, RealDatas);
            DataRepo.Dapper.BulkUpdate(realDataDt, realDataTableName, "PointID");
        }

        private string GetFluxSum(string value, string realValue)
        {
            return (value.ToDouble() + realValue.ToDouble()).ToString();
        }
        protected string GetFluxTotalInfo(IEnumerable<string[]> datas, FluxTypeFlag flag)
        {
             var result= datas.FirstOrDefault(p => p[0].Substring(p[0].Length - 2).ToInt() == (int)flag);
            if (result != null)
                return result[1];
            else
                return string.Empty;
        }


        protected KJ370_RealDataModel GetRealData(string[] segments, DateTime now)
        {
            var valueState = 0;
            var feedState = 0;
            if (Enum.TryParse<ProtocolState>(segments[2], out var protocalState))
            {
                var result = ConvertValueState(protocalState);
                valueState = result.ValueState;
                feedState = result.FeedState;
            }
            var pointId = GetPointCode(segments[0]);
            var analogPoint = _dev.AnalogPointModels.FirstOrDefault(p => p.PointID == pointId);
            return new KJ370_RealDataModel()
            {
                PointID = pointId,
                PointName = analogPoint?.PointName,
                SubStationID = pointId.Substring(0, 3).ToInt(),
                PortNO = pointId.Substring(4, 2).ToInt(),
                PointType = (int)PointType.Analog,
                RealValue = segments[1],
                RealDate = now,
                RealState = valueState,
                FeedState = feedState
            };
        }
    }
}
