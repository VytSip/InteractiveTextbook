using InteractiveTextbook.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InteractiveTextbook.Models
{
    public class TextBookPageViewModel
    {
        public int PageNumber { get; set; }

        public string Text { get; set; }

        public int TextbookId { get; set; }

        public virtual Textbook Textbook { get; set; }
    }
}
