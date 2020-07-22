using DataFusion.Interfaces;
using DataFusion.Interfaces.Utils;
using DataFusionProtocal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFusionPlatformPlugin.ViewModel
{
    public class Repo
    {
        private readonly RedisHelper _redis;
        private const string DATABASE_DATAKEY = "_database";
        private const string ENABLE_DATAKEY = "_enable";
        private const string KVSG_DATAKEY = "_kvsg";


        public Repo(IHostConfig hostConfig)
        {
            _redis = new RedisHelper(0);
        }
        public DatabaseSg GetDatabaseSg(string mineId, string fieldKey)
        {
            if (!_redis.HashExists(mineId, fieldKey + DATABASE_DATAKEY))
                return default;
            return _redis.HashGet<DatabaseSg>(mineId, fieldKey + DATABASE_DATAKEY);
        }

        public void SetDatabaseSg(string mineId, string fieldKey, DatabaseSg databaseSg)
        {
            _redis.HashSet<DatabaseSg>(mineId, fieldKey + DATABASE_DATAKEY, databaseSg);
        }
        public List<ProtocalKvSg> GetProtocalKvSgs(string mineId, string fieldKey)
        {
            if (!_redis.HashExists(mineId, fieldKey + KVSG_DATAKEY))
                return default;
            return _redis.HashGet<List<ProtocalKvSg>>(mineId, fieldKey + KVSG_DATAKEY);
        }
        public void SetProtocalKvSgs(string mineId,string fieldKey,List<ProtocalKvSg> protocalKvSgs)
        {
            _redis.HashSet<List<ProtocalKvSg>>(mineId, fieldKey + KVSG_DATAKEY, protocalKvSgs);
        }

        public string GetProtocalKvSg(string mineId, string fieldKey, string key)
        {
            if (!_redis.HashExists(mineId, fieldKey + "_" + key))
                return default;
            return _redis.HashGet<string>(mineId, fieldKey + "_" + key);
        }
        public void SetProtocalKvSg(string mineId, string fieldKey, string key, string value)
        {
            _redis.HashSet<string>(mineId, fieldKey + "_" + key, value);
        }
        public bool GetProtocalEnable(string mineId, string fieldKey)
        {
            if (!_redis.HashExists(mineId, fieldKey + ENABLE_DATAKEY))
                return false;
            return _redis.HashGet<bool>(mineId, fieldKey + ENABLE_DATAKEY);
        }
        public void SetProtocalEnable(string mineId, string fieldKey, bool value)
        {
            _redis.HashSet<bool>(mineId, fieldKey + ENABLE_DATAKEY, value);
        }
        public long Publish<T>(string channel, T msg)
        {
            return _redis.Publish<T>(channel, msg);
        }
    }
}
