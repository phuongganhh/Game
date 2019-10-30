using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF.Framework
{
    public class SingleMailDto: BaseMailDto
    {
        public string ReceiverEmail { get; set; }
        public string ReceiverName { get; set; }
        public string Password { get; set; }
    }
}
