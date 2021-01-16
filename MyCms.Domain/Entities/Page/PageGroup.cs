using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyCms.Domain.Entities.Page
{
    public class PageGroup
    {
        [Key]
        public int GroupId { get; set; }
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0}را وارد کنید ")]
        [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string Title { get; set; }

        public List<Page> Pages { get; set; }
    }
}
