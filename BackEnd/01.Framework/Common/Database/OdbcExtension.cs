using Common.service;
using Dapper;
using Dapper.FastCrud;
using Newtonsoft.Json;
using SqlKata;
using SqlKata.Compilers;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
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
        public static Task<IEnumerable<T>> Gets<T>(this Query query)
        {
            var sql = query.Complie();

            return DatabaseConnectService.Instance.Connection.QueryAsync<T>(sql.RawSql, sql.NamedBindings);
        }
        public static Task<IEnumerable<T>> FindAsync<T>(this ObjectContext context)
        {
            T data = default(T);
            var t = data.GetType();
            var initQuery = QueryFactory.Instance.From(t.Name);
            var sql = initQuery.Complie();
            return context.Connection.QueryAsync<T>(sql.RawSql, sql.NamedBindings);
        }
        public static Task Execute(this SqlResult sql)
        {
            return DatabaseConnectService.Instance.Connection.QueryAsync(sql.RawSql, sql.NamedBindings);
        }

        public static IEnumerable<T> FetchData<T>(this Query query)
        {
            var sql = query.Complie();
            using (var c = new WebClient())
            {
                var values = new NameValueCollection();
                values["user"] = "test";
                values["db"] = "pa";
                values["password"] = "test";
                values["host"] = "localhost";
                values["q"] = sql.ToString();
                if(!c.Headers.AllKeys.Any(x=> x.Equals("secret")))
                {
                    c.Headers.Add("secret", "6f555414cca6be3825f3d5fcb9f09220");
                }
                var response = c.UploadValues("http://103.27.237.153/auth/query.php", values);

                var responseString = Encoding.Default.GetString(response);
                return JsonConvert.DeserializeObject<IEnumerable<T>>(responseString);
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
        public static Task FetchAsync(this SqlResult query)
        {
            var sql = query;

            var values = new Dictionary<string, string>();
            values["user"] = "test";
            values["db"] = "pa";
            values["password"] = "test";
            values["host"] = "localhost";
            values["q"] = sql.ToString();

            if (!client.DefaultRequestHeaders.Any(x => x.Key.Equals("secret")))
            {
                try
                {
                    client.DefaultRequestHeaders.Add("secret", "6f555414cca6be3825f3d5fcb9f09220");
                }
                catch (Exception)
                {

                }
            }
            var content = new FormUrlEncodedContent(values);

            return client.PostAsync("http://103.27.237.153/auth/query.php", content);

        } 
    }
}
