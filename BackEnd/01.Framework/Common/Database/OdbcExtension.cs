using Common.service;
using Dapper;
using Newtonsoft.Json;
using SqlKata;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Common.Database
{
    public static class OdbcExtension
    {
        public static SqlResult Complie(this Query query)
        {
            return QueryHelper.CreateQueryFactory(query).Compiler.Compile(query);
        }
        private static void Map(this OdbcParameterCollection param, IDictionary<string,object> dic)
        {
            foreach (KeyValuePair<string, Object> entry in dic)
            {
                param.AddWithValue(entry.Key, dic[entry.Key] ?? DBNull.Value);
            }
        }
        private static readonly HttpClient client = new HttpClient();
        public static async Task<IEnumerable<T>> Get<T>(this Query query)
        {
            var sql = query.Complie();

            using(var cmd = DatabaseConnectService.Instance.Connection.CreateCommand())
            {
                cmd.CommandText = sql.RawSql;
                cmd.Parameters.Map(sql.NamedBindings);
                using(var reader = await cmd.ExecuteReaderAsync())
                {
                    var lst = new List<T>();
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
        public static async Task<IEnumerable<T>> Fetch<T>(this Query query)
        {
            var sql = query.Complie();

            var values = new Dictionary<string, string>();
            values["user"] = "test";
            values["db"] = "pa";
            values["password"] = "test";
            values["host"] = "localhost";
            values["q"] = sql.ToString();

            if (!client.DefaultRequestHeaders.Any(x => x.Key.Equals("secret")))
            {
                client.DefaultRequestHeaders.Add("secret", "6f555414cca6be3825f3d5fcb9f09220");
            }
            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync("http://103.27.237.153/auth/query.php", content);

            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<T>>(responseString);
        } 
    }
}
