using Common.service;
using SqlKata.Compilers;
using SqlKata.Execution;
using System;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Common
{
    public class ObjectContext
    {
        public readonly Guid RequestId = Guid.NewGuid();
        
        public ICacheManager cache
        {
            get
            {
                return MemoryCacheManager.Instance;
            }
        }
        public string MD5(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public QueryFactory Query
        {
            get
            {
                return new QueryFactory(this.Database.Connection, new MySqlCompiler());
            }
        }
        public DatabaseConnectService Database
        {
            get
            {
                return DatabaseConnectService.Instance;
            }
        }
        public IDbConnection Connection
        {
            get
            {
                return DatabaseConnectService.Instance.Connection;
            }
        }
        public static ObjectContext CreateContext(Controller controller, bool isAdmin = false)
        {
            return new ObjectContext(controller);
        }

        private Controller _controller;
        //private IDbInfoRepository _repo;

        private ObjectContext(Controller controller)
        {
            _controller = controller;
            //_repo = ServiceLocator.Current.GetInstance<IDbInfoRepository>();
        }

        private HttpContextBase Context { get { return _controller.HttpContext; } }

        private HttpBrowserCapabilitiesBase Browser { get { return Context.Request.Browser; } }

        #region IP

        public string ClientIp
        {
            get
            {
                if (Context.Request.ServerVariables.AllKeys.Contains("HTTP_X_FORWARDED_FOR"))
                {
                    return Context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                }
                else if (Context.Request.ServerVariables.AllKeys.Contains("REMOTE_ADDR"))
                {
                    return Context.Request.ServerVariables["REMOTE_ADDR"];
                }
                else if (!string.IsNullOrWhiteSpace(Context.Request.UserHostAddress))
                {
                    return Context.Request.UserHostAddress;
                }
                else
                {
                    return "";
                }
            }
        }

        public string ServerIp
        {
            get
            {
                var localAddr = Context.Request.ServerVariables["LOCAL_ADDR"];
                if (!string.IsNullOrWhiteSpace(localAddr))
                {
                    return localAddr;
                }
                else
                {
                    return Dns.GetHostEntry(IPAddress.Parse(Context.Request.UserHostName)).HostName;
                }
            }
        }

        public bool IsFromLocal()
        {
            return Context.Request.ServerVariables["HTTP_HOST"].Equals("test.PA.com", StringComparison.OrdinalIgnoreCase);
        }

        #endregion

        #region Authentication

        public bool IsAuthorized
        {
            get { return Context.User != null && Context.User.Identity.IsAuthenticated; }
        }

        

        #endregion

        #region Browser infomations

        public string BrowserName
        {
            get
            {
                return Browser.Browser;
            }
        }

        public string BrowserType
        {
            get
            {
                return Browser.Type;
            }
        }

        public string BrowserPlatform
        {
            get
            {
                return Browser.Platform;
            }
        }

        public int BrowserMajorVersion
        {
            get
            {
                return Browser.MajorVersion;
            }
        }

        public HttpBrowserCapabilitiesBase BrowserCaPAbilities
        {
            get
            {
                return Browser;
            }
        }

        public bool IsSupportFrames
        {
            get
            {
                return Browser.Frames;
            }
        }

        /// <summary>
        /// check if the browser supports HTML &lt;table&gt; elements
        /// </summary>
        public bool IsSupportTables
        {
            get
            {
                return Browser.Tables;
            }
        }

        public bool IsSupportCookies
        {
            get
            {
                return Browser.Cookies;
            }
        }

        public bool IsSupportVBScript
        {
            get
            {
                return Browser.VBScript;
            }
        }

        public bool IsSupportJavaApplets
        {
            get
            {
                return Browser.JavaApplets;
            }
        }

        public string JavaScriptVersion
        {
            get
            {
                return Browser["JavaScriptVersion"];
            }
        }

        #endregion

        #region Culture

        public CultureInfo ClientCulture
        {
            get
            {
                var languages = Context.Request.UserLanguages;
                if (languages != null && languages.Length > 0)
                {
                    return new CultureInfo(languages[0]);
                }
                else
                {
                    return CultureInfo.InvariantCulture;
                }
            }
        }

        /// <summary>
        /// Now from Database
        /// </summary>
        /// <PAram name="name">Database connection name</PAram>
        public DateTime GetNow(string name = null)
        {
            return DateTime.Now;
            //return _repo.GetDate();
        }

        #endregion

        #region Controller context

        public HttpSessionStateBase Session
        {
            get { return _controller.Session; }
        }

        #endregion

        #region Business Info

        /// <summary>
        /// Get PAge size
        /// </summary>
        /// <PAram name="table">Table name</PAram>
        /// <returns></returns>
        public int GetPageSize(string table = null)
        {
            return 25;
        }

        #endregion

        #region member of Mvc

        public Controller Controller
        {
            get { return _controller; }
        }


        public dynamic ViewBag
        {
            get { return _controller.ViewBag; }
        }

        public ViewDataDictionary ViewData
        {
            get { return _controller.ViewData; }
        }
        #endregion
    }
}
