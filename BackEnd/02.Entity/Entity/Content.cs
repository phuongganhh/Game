using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    [Table("content")]
    public class Content
    {
        public string id { get; set; }
        public string content_html { get; set; }
        public string content_text { get; set; }
    }
}
