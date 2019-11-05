using Newtonsoft.Json;
using SqlKata;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Odbc;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.Database
{
    public static class OdbcExtension
    {
        private static bool vnIsNull(this object thiz)
        {
            return thiz == null || thiz == DBNull.Value;
        }
        private static void MapParameter(this OdbcParameterCollection param, Dictionary<string, Object> dic)
        {
            foreach (KeyValuePair<string, Object> entry in dic)
            {
                param.AddWithValue(entry.Key, dic[entry.Key] ?? DBNull.Value);
            }
        }
        private static string ConvertToSqlServer(this SqlResult query)
        {
            return query.Sql;
        }
        private static IEnumerable<PropertyInfo> GetPropertiesWithCache(this Type type, string key = null, int CacheTimeByMinute = 3)
        {
            if (key == null)
            {
                key = type.FullName;
            }
            return MemoryCacheManager.Instance.GetOrSet(key, () => type.GetProperties(), CacheTimeByMinute);
        }
        public static Task<int> ExecuteNotResult(this SqlResult query,OdbcConnection connection)
        {
            using (var conn = connection)
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query.RawSql;
                    cmd.Parameters.MapParameter(query.NamedBindings);
                    return cmd.ExecuteNonQueryAsync();
                }
            }
        }
        public static async Task<IEnumerable<T>> Result<T>(this Query query,OdbcConnection conn)
        {
            var sql = query.Complie();
            using(var cmd = conn.CreateCommand())
            {
                cmd.CommandText = sql.RawSql;
                cmd.Parameters.MapParameter(sql.NamedBindings);
                using(var reader = await cmd.ExecuteReaderAsync())
                {
                    List<T> lst = new List<T>();
                    while (await reader.ReadAsync())
                    {
                        var dic = new Dictionary<string, object>();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            dic[reader.GetName(i)] = reader.GetValue(i);
                        }
                        var json = JsonConvert.SerializeObject(dic);
                        lst.Add(JsonConvert.DeserializeObject<T>(json));
                    }
                    return lst;
                }
            }

        }
        public static SqlResult Complie(this Query query)
        {
            return QueryHelper.CreateQueryFactory(query).Compiler.Compile(query);
        }
        private static readonly HttpClient client = new HttpClient();

        public static int PostData(this SqlResult query)
        {
            var sql = query;
            using (var c = new WebClient())
            {
                var values = new NameValueCollection
                {
                    ["user"] = "test",
                    ["db"] = "pa",
                    ["password"] = "test",
                    ["host"] = "localhost",
                    ["q"] = sql.RawSql,
                    ["param"] = JsonConvert.SerializeObject(sql.NamedBindings.Select(x => x.Value).ToArray())
                };

                if (!c.Headers.AllKeys.Any(x=> x.Equals("secret")))
                {
                    c.Headers.Add("secret", "6f555414cca6be3825f3d5fcb9f09220");
                }
                var response = c.UploadValues("http://103.27.237.153/auth/execute.php", values);

                var responseString = Encoding.Default.GetString(response);
                return Convert.ToInt32(responseString);
            }
        }

        public static IEnumerable<T> FetchData<T>(this Query query)
        {
            var sql = query.Complie();
            using (var c = new WebClient())
            {
                var values = new NameValueCollection
                {
                    ["user"] = "test",
                    ["db"] = "pa",
                    ["password"] = "test",
                    ["host"] = "localhost",
                    ["q"] = sql.RawSql,
                    ["param"] = JsonConvert.SerializeObject(sql.NamedBindings.Select(x => x.Value).ToArray())
                };

                if (!c.Headers.AllKeys.Any(x=> x.Equals("secret")))
                {
                    c.Headers.Add("secret", "6f555414cca6be3825f3d5fcb9f09220");
                }
                var response = c.UploadValues("http://103.27.237.153/auth/fetch.php", values);

                var responseString = Encoding.Default.GetString(response);
                return JsonConvert.DeserializeObject<IEnumerable<T>>(responseString);
            }
        }

        public static async Task<IEnumerable<T>> FetchAsync<T>(this Query query)
        {
            var sql = query.Complie();

            var values = new Dictionary<string, string>();
            values["user"] = "test";
            values["db"] = "pa";
            values["password"] = "test";
            values["host"] = "localhost";
            values["q"] = sql.RawSql;
            values["param"] = JsonConvert.SerializeObject(sql.NamedBindings.Select(x => x.Value).ToArray());

            if (!client.DefaultRequestHeaders.Any(x => x.Key.Equals("secret")))
            {
                client.DefaultRequestHeaders.Add("secret", "6f555414cca6be3825f3d5fcb9f09220");
            }
            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync("http://103.27.237.153/auth/fetch.php", content);

            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<T>>(responseString);
        }
        public static async Task<bool> ExecuteAsync(this SqlResult query)
        {
            var sql = query;

            var values = new Dictionary<string, string>();
            values["user"] = "test";
            values["db"] = "pa";
            values["password"] = "test";
            values["host"] = "localhost";
            values["q"] = sql.RawSql;
            values["param"] = JsonConvert.SerializeObject(sql.NamedBindings.Select(x => x.Value).ToArray());

            if (!client.DefaultRequestHeaders.Any(x => x.Key.Equals("secret")))
            {
                client.DefaultRequestHeaders.Add("secret", "6f555414cca6be3825f3d5fcb9f09220");
            }
            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync("http://103.27.237.153/auth/execute.php", content);

            var responseString = await response.Content.ReadAsStringAsync();
            var result =  JsonConvert.DeserializeObject<int>(responseString);
            return result > 0;
        } 
    }
}
