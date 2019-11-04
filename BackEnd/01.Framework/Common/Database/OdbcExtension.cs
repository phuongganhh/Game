using Newtonsoft.Json;
using SqlKata;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
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
