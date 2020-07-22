using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace DataFusionProtocal.Interfaces
{
    public class DapperBase : IDapper
    {
        private readonly string _connString;

        public DapperBase(DatabaseConfig databaseConfig)
        {
            _connString = databaseConfig.ConnectionString();
        }
        public DapperBase(string conString)
        {
            _connString = conString;
        }

        public int BulkInsert(DataTable datasource, string destinationTableName)
        {
            if (datasource.Rows.Count == 0) return 0;
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                using (SqlCommand command = new SqlCommand("", conn))
                {
                    try
                    {
                        conn.Open();

                        //Bulk insert into temp table
                        using (SqlBulkCopy bulkcopy = new SqlBulkCopy(conn))
                        {
                            bulkcopy.BulkCopyTimeout = 60;
                            bulkcopy.DestinationTableName = destinationTableName;
                            bulkcopy.WriteToServer(datasource);
                            bulkcopy.Close();
                        }
                        return datasource.Rows.Count;
                    }
                    catch (Exception ex)
                    {
                        // Handle exception properly
                        Console.WriteLine("bulk insert error:" + ex);
                        //                        Logger.LogError();

                        return -1;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

        public int BulkUpdate(DataTable datasource, string destinationTableName, params string[] keys)
        {
            if (datasource.Rows.Count == 0) return 0;
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                using (SqlCommand command = new SqlCommand("", conn))
                {
                    conn.Open();
                    try
                    {
                        string tempTableName = "#TempBulkTable";
                        string createSql = GetCreateSql(datasource, tempTableName, keys);
                        try
                        {
                            //Creating temp table on database
                            command.CommandText = createSql;
                            //command.Transaction = tran;
                            command.ExecuteNonQuery();
                        }
                        catch (Exception ex1)
                        {
                            throw new Exception($"Bulk {destinationTableName} ex1:", ex1);
                        }

                        try
                        {
                            //Bulk insert into temp table
                            using (SqlBulkCopy bulkcopy = new SqlBulkCopy(conn))
                            {
                                //bulkcopy.BatchSize = 100;
                                bulkcopy.BulkCopyTimeout = 60;
                                bulkcopy.DestinationTableName = tempTableName;
                                bulkcopy.WriteToServer(datasource);
                                bulkcopy.Close();
                            }
                        }
                        catch (Exception ex2)
                        {

                            throw new Exception($"Bulk {destinationTableName} ex2:", ex2);
                        }

                        try
                        {
                            // Updating destination table, and dropping temp table
                            command.CommandTimeout = 60;
                            command.CommandText = GetUpdateSql(datasource, tempTableName, destinationTableName, keys);
                            var count = command.ExecuteNonQuery();
                        }
                        catch (Exception ex3)
                        {

                            throw new Exception($"Bulk {destinationTableName} ex3:", ex3);
                        }
                        //tran.Commit();
                        return datasource.Rows.Count;
                    }
                    catch (Exception ex)
                    {
                        // Handle exception properly
                        throw new DataException("BulkUpdate:" + ex);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

        private static string GetUpdateSql(DataTable datasource, string tempTableName, string destinationTableName, string[] keys)
        {
            List<string> updateFields = new List<string>(), whereFields = new List<string>(), fields = new List<string>();
            var keysUpper = keys.Select(o => o.ToUpper()).ToList();
            foreach (DataColumn column in datasource.Columns)
            {
                var name = column.ColumnName.ToUpper(); ;
                if (keysUpper.Contains(name))
                {
                    whereFields.Add(string.Format("{1}.[{0}] = TT.[{0}]", name, destinationTableName));
                }
                fields.Add(string.Format("[{0}]", name));
            }

            return string.Format("DELETE FROM {0}  WHERE EXISTS (SELECT 1 FROM {1} AS TT WHERE {2}) ; INSERT INTO {0} ({3}) SELECT {3} FROM {1} ;DROP TABLE {1}"
                , destinationTableName, tempTableName, string.Join(" and ", whereFields), string.Join(" , ", fields));
        }

        private string GetCreateSql(DataTable datasource, string tempBulkTable, string[] keys)
        {
            string fields = "";
            var keysUpper = keys.Select(o => o.ToUpper()).ToList();

            foreach (DataColumn column in datasource.Columns)
            {
                if (keysUpper.Contains(column.ColumnName.ToUpper()))
                {
                    fields += string.Format("[{0}] [nvarchar](100),", column.ColumnName);
                }
                else
                {
                    fields += string.Format("[{0}] [nvarchar](max),", column.ColumnName);
                }
            }

            return string.Format(@"Create Table {2}({0} PRIMARY KEY ({1}) )", fields,
                string.Join(",", keys.Select(o => $"[{o}]")), tempBulkTable);

        }

        public IEnumerable<T> Query<T>(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (var conn = new SqlConnection(_connString))
            {
                return conn.Query<T>(sql, param, transaction, buffered, commandTimeout, commandType);
            }
        }

        public int Execute(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (var conn = new SqlConnection(_connString))
            {
                return conn.Execute(sql, param, transaction, commandTimeout, commandType);
            }
        }

        public T ExecuteScalar<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (var conn = new SqlConnection(_connString))
            {
                return conn.ExecuteScalar<T>(sql, param, transaction, commandTimeout, commandType);
            }
        }

        public int BulkInsert<T>(List<T> list, string destinationTableName)
        {
            throw new NotImplementedException();
        }
    }
}
