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
            var tempModels = lineContents.Where(p => p.Length == 16).Select(o => new AlarmToday(o, realTime)).Where(q => q.IsValid);
            var alarmTodayModels = tempModels.Select(p => p.AlarmTodayModel);
            /*
            var alarmTodayList = from t in alarmTodayModels
                                 join a in _dev.EquipmentInfoModels
                                 on t.EquipId equals a.EquipId
                                 select t;
            var tableName = "AlarmToday";
            var dt = ListToDataTableHelper.Models2Datatable<AlarmTodayModel>(tableName, alarmTodayList.ToList());
            DataRepo.Dapper.BulkUpdate(dt, tableName, "EquipId", "Index", "StartTime", "ValueState");
            */

            var analogAlarmList = from t in tempModels.Where(p => p.IsAnalogAlarm).Select(p => p.AnalogAlarmModel)
                                  join a in _dev.AnalogPointModels
                                  on t.EquipId equals a.EquipId
                                  select t;
            var analogAlarmTableName = $"AnalogAlarm{realTime:yyyyMM}";
            var analogAlarmDt = ListToDataTableHelper.Models2Datatable<AnalogAlarmModel>(analogAlarmTableName, analogAlarmList.ToList());
            DataRepo.YearDapper.BulkUpdate(analogAlarmDt, analogAlarmTableName, "EquipId", "Index", "PointCode", "StartTime", "ValueState");
        }
    }
}
