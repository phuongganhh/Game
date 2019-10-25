using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class ApplicationGroup
    {
        public ApplicationGroup()
        {
            this.id = DateTime.Now.Ticks;
        }
        public long id { get; set; }
        public string name { get; set; }
        public IEnumerable<Application> Applications { get; set; }
    }
}
