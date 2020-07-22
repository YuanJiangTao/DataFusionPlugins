using DataFusionProtocal.Interfaces;
using EpipeProtocal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace EpipeProtocal.Repositories
{
    class DataRepo
    {
        private  DatabaseConfig _databaseConfig;
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

        public List<KJ370_AnalogPointModel> GetKJ370_AnalogPointModels()
        {
            var sql = "select * from KJ370_AnalogPoint";
            return Dapper.Query<KJ370_AnalogPointModel>(sql).ToList();
        }
        public List<KJ370_FluxPointModel> GetKJ370_FluxPointModels()
        {
            var sql = "select * from KJ370_FluxPoint";
            return Dapper.Query<KJ370_FluxPointModel>(sql).ToList();
        }

        public List<KJ370_FluxRunModel> GetKJ370_FluxRunModels()
        {
            var now = DateTime.Now;
            var sql = $"select * from KJ370_FluxRun{now:yyyy} where [Year]={now.Year} and [Month]={now.Month} and [Day]={now.Day} and [Flag]=2";
            return YearDapper.Query<KJ370_FluxRunModel>(sql).ToList();
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
