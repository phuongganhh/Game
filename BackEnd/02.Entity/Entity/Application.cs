using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Application
    {
        public Application()
        {
            this.id = DateTime.Now.Ticks;
        }
        public long? id { get; set; }
        public string name { get; set; }
        public long? parent_id { get; set; }
        public string style { get; set; }
        public string url { get; set; }
        public long? application_group_id { get; set; }
        public long? updated_time { get; set; }
        public long? created_time { get; set; }
        public IEnumerable<Application> Childrent { get; set; }
    }
}
