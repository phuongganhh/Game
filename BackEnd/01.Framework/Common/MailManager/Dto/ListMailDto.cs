using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF.Framework
{
    public class ListMailDto : BaseMailDto
    {
        public List<string> ReceiverEmails { get; set; }
        public List<string> ReceiverName { get; set; }
    }
}
