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
        public virtual long? id { get; set; }
        public virtual string title { get; set; }
        public virtual long? news_group_id { get; set; }
        public virtual long? created_time { get; set; }
        public NewsGroup NewsGroup { get; set; }
    }
}
