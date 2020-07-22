using DataFusionProtocal.Interfaces;
using MineSafetySystemProtocal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MineSafetySystemProtocal.Repositories
{
    class DataRepo
    {
        private DatabaseConfig _databaseConfig;
        private DateTime _initializationTime;
        public DataRepo(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
            Dapper = new DapperBase(databaseConfig);
            _initializationTime = DateTime.Now;
            HandleYearDapper();
        }

        public IDapper Dapper { get; private set; }

        public IDapper YearDapper { get; private set; }

        public List<EquipmentInfoModel> GetEquipmentInfoModels()
        {
            var sql = "select * from EquipmentInfo";
            return Dapper.Query<EquipmentInfoModel>(sql).ToList();
        }
        public List<AnalogPointModel> GetAnalogPointModels()
        {
            var sql = "select * from AnalogPoint ";
            return Dapper.Query<AnalogPointModel>(sql).ToList();
        }
        public List<SwitchPointModel> GetSwitchPointModels()
        {
            var sql = "select * from SwitchPoint";
            return Dapper.Query<SwitchPointModel>(sql).ToList();
        }
        public List<ControlPointModel> GetControlPointModels()
        {
            var sql = "select * from ControlPoint";
            return Dapper.Query<ControlPointModel>(sql).ToList();
        }

        public void HandleYearDapper()
        {
            var now = DateTime.Now;
            if (_initializationTime.Year == now.Year && YearDapper != null) return;
            _initializationTime = now;
            var databaseConfig = _databaseConfig.ToYearString(_initializationTime);
            YearDapper = new DapperBase(databaseConfig);
        }
    }
}
