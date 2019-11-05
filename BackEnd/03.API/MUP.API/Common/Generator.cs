using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MUP.API.Common
{
    public class Generator
    {
        public static string Token
        {
            get
            {
                return Hash.sha256(Guid.NewGuid().ToString());
            }
        }
    }
}