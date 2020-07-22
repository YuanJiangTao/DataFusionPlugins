using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFusionProtocal.Interfaces.Utils
{
  public  class ConnectUtil
    {
        public static bool TryConnect(string connectionString)
        {
            var task = Task.Run(() =>
            {
                try
                {
                    using (var conn = new SqlConnection(connectionString))
                    {
                        var ret = conn.ExecuteScalar("select getdate()");
                    }
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            });
            if (task.Wait(2000))
            {
                return task.Result;
            }
            return false;
        }

        internal static bool TryFtpConnect(string ftpServer, string virDirectory, string ftpUserId, string password, int ftpPort)
        {
            //包裹一层,在3秒钟完成判断.
            var task = Task.Run(() =>
            {
                try
                {
                    var ftpClient = new MyFtp(ftpServer, virDirectory, ftpUserId, password, ftpPort);
                    return ftpClient.Connected;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            });

            return task.Result && task.Wait(2000);
        }
    }
}
