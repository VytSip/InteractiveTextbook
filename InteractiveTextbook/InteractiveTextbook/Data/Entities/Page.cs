using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InteractiveTextbook.Data.Entities
{
    public class Page
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int PageNumber { get; set; }

        public string Text { get; set; }

        public int TextbookId { get; set; }
        [ForeignKey("TextbookId")]
        public virtual Textbook Textbook { get; set; }
    }
}
