using InteractiveTextbook.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InteractiveTextbook.Models
{
    public class PageViewModel
    {
        public int Id { get; set; }

        public int PageNumber { get; set; }

        public string Text { get; set; }

        public int TextbookId { get; set; }

        public virtual Textbook Textbook { get; set; }

        public string AudioPath { get; set; }

        public IFormFile FormFile { get; set; }

        public static Page ConvertToModel (PageViewModel page)
        {
            return new Page
            {
                PageNumber = page.PageNumber,
                AudioPath = page.AudioPath,
                TextbookId = page.TextbookId,
                Textbook = page.Textbook,
                Text = page.Text
            };
        }
    }

    public class FileModel
    {
        [Display(Name = "File")]
        public IFormFile FormFile { get; set; }

        [Display(Name = "Note")]
        [StringLength(50, MinimumLength = 0)]
        public string Note { get; set; }
    }
}
