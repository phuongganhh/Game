using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Mail
    {
        public long? id { get; set; }
        public string email { get; set; }
        public string message { get; set; }
        public int sent { get; set; }
        public DateTime created_date { get; set; }
        public DateTime sent_date { get; set; }
    }
}
