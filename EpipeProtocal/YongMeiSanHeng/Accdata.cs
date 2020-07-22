using DataFusionProtocal.Interfaces.Helper;
using DataFusionProtocal.Interfaces.Utils;
using EpipeProtocal.Models;
using EpipeProtocal.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpipeProtocal.YongMeiSanHeng
{
    class Accdata : Base
    {
        private readonly Dev _dev;
        private DateTime _lastWriteTime;
        public Accdata(ParseProtocal protocol, DataRepo dataRepo, Action<string> log, Dev dev)
            : base(protocol, dataRepo, log)
        {
            _dev = dev;
            _fluxRunModels = dataRepo.GetKJ370_FluxRunModels();
            _lastWriteTime = DateTime.Now;
        }
        private readonly List<KJ370_FluxRunModel> _fluxRunModels;
        public override string FileName => "ACCDATA";

        protected override void ParseContent(List<string> lines, DateTime realTime)
        {
            if (_lastWriteTime.Day != realTime.Day)
            {
                _fluxRunModels.Clear();
                _lastWriteTime = realTime;
            }
            var lineContents = lines.Select(p => GetLines(p)).Where(p => p.Length == 6);
            foreach (var line in lineContents)
            {
                var fluxInfos = GetFluxCode(line[0]);
                var fluxPoint = _dev.FluxPointModels.FirstOrDefault(p => p.FluxID == fluxInfos.FluxId);
                if (fluxPoint == null) continue;
                var fluxRun = _fluxRunModels.FirstOrDefault(p => p.FluxID == fluxInfos.FluxId);
                if (fluxRun == null)
                {
                    fluxRun = new KJ370_FluxRunModel()
                    {
                        FluxID = fluxPoint.FluxID,
                        SubStationID = fluxPoint.SubStationID,
                        Location = fluxPoint.Location,
                        FluxName = fluxPoint.FluxName,
                        Flag = 2,
                        MethaneChromaMaxTime = realTime,
                        TemperatureMaxTime = realTime,
                        PressureMaxTime = realTime,
                        FluxMaxTime = realTime,
                        MethaneChromaMinTime = realTime,
                        TemperatureMinTime = realTime,
                        PressureMinTime = realTime,
                        FluxMinTime = realTime,
                        UpdateTime = realTime
                    };
                    _fluxRunModels.Add(fluxRun);
                }
                var value = GetStringSum(line.Skip(2));
                switch (fluxInfos.Flag)
                {
                    case FluxTypeFlag.FluxTotal:
                        fluxRun.FluxTotal = value;
                        break;
                    case FluxTypeFlag.PureFluxTotal:
                        fluxRun.PureFluxTotal = value;
                        break;
                    case FluxTypeFlag.IndustrialFluxTotal:
                        fluxRun.PureFluxTotal = value;
                        break;
                    case FluxTypeFlag.IndustrialPureFluxTotal:
                        fluxRun.IndustrialPureFluxTotal = value;
                        break;
                    default:
                        continue;
                }
            }
            var tableName = $"KJ370_FluxRun{realTime:yyyy}";
            var dt = ListToDataTableHelper.Models2Datatable<KJ370_FluxRunModel>(tableName, _fluxRunModels);
            DataRepo.YearDapper.BulkUpdate(dt, tableName, "FluxID", "Year", "Month", "Day", "Hour", "Flag");
        }
        private double GetStringSum(IEnumerable<string> datas)
        {
            return datas.Select(p => p.ToDouble()).Sum();
        }

    }



    /// <summary>
    /// 根据管网上传中的流量累积量，进行分类，与gltech的管网上传（永煤三恒协议一致）
    /// </summary>
    enum FluxTypeFlag
    {
        /// <summary>
        /// 标况混合流量
        /// </summary>
        FluxTotal = 1,
        /// <summary>
        /// 标况纯流量
        /// </summary>
        PureFluxTotal = 2,
        /// <summary>
        /// 工况混合流量
        /// </summary>
        IndustrialFluxTotal = 3,
        /// <summary>
        /// 工况纯流量
        /// </summary>
        IndustrialPureFluxTotal = 4
    }
}
