using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class BusinessException : SystemException
    {
        public HttpStatusCode exit_code { get; set; }
        public BusinessException(string mess,HttpStatusCode Code = HttpStatusCode.InternalServerError) : base(mess)
        {
            this.exit_code = Code;
        }
    }
}
