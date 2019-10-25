using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class File
    {
        public File()
        {
            this.id = DateTime.Now.Ticks;
        }
        public long id { get; set; }
        public string name { get; set; }
        public string physical_name { get; set; }
        public long size { get; set; }
        public string extension { get; set; }
        public string version { get; set; }
        public string creater_id { get; set; }
        public long created_time { get; set; }
        public int download { get; set; }
    }
}
