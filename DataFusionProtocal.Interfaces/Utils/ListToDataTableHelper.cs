using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;


namespace DataFusionProtocal.Interfaces.Utils
{
    public class ListToDataTableHelper
    {
        public static DataTable Models2Datatable<T>(string tableName, IEnumerable<T> models, params string[] ignoreColumns)
        {
            var type = typeof(T);
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            DataTable dt = new DataTable(tableName);
            foreach (var propertyInfo in properties)
            {
                if (ignoreColumns.Any(o => o.ToUpper().Equals(propertyInfo.Name.ToUpper())))
                    continue;
                dt.Columns.Add(new DataColumn(propertyInfo.Name, propertyInfo.PropertyType));
            }

            foreach (var model in models)
            {
                var modelType = model.GetType();

                var row = dt.NewRow();

                foreach (DataColumn column in dt.Columns)
                {
                    var p = modelType.GetProperty(column.ColumnName);

                    var value = p?.GetValue(model, null);

                    row[column.ColumnName] = value;
                }

                dt.Rows.Add(row);
            }

            return dt;
        }
    }
}
