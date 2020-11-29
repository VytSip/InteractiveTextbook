using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InteractiveTextbook.Data.Entities
{
    public class PageTimer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public TimeSpan TimeElapsed { get; set; }

        public int PageId { get; set; }
        [ForeignKey("PageId")]
        public virtual Page Page { get; set; }

        public DateTime RecordCreatedOn { get; set; }
    }
}
