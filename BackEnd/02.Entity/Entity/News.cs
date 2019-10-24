using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    [Table("news")]
    public class News
    {
        [Key]
        public virtual string id { get; set; }
        public virtual string title { get; set; }
        public virtual string news_group_id { get; set; }

        public NewsGroup NewsGroup { get; set; }
    }
}
