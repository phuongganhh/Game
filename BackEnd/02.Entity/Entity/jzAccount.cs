using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    [Table("account")]
    public class jzAccount
    {
        [Key]
        public long id { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string player_name { get; set; }
        public int VIP { get; set; }
        public DateTime? reg_date { get; set; }
        public int online { get; set; }
    }
}
