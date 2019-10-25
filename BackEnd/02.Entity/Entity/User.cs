using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class User
    {
        public long id { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string player_name { get; set; }
        public long player_id { get; set; }
        public string token { get; set; }
    }
}
