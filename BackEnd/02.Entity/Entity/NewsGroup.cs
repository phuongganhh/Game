using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    [Table("news_group")]
    public class NewsGroup
    {
        [Key]
        public string id { get; set; }
        public string name { get; set; }
        
        public IEnumerable<News> News { get; set; }
        public int TotalRecord { get; set; }
    }
}
