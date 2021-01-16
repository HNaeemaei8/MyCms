using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyCms.Domain.Entities.Page
{
    public class Page
    {
        [Key]
        public int PageId { get; set; }
        [Display(Name ="گروه")]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید ")]
        public int GroupId { get; set; }
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        [StringLength(400,ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string Title { get; set; }
        [Display(Name = "توضیح مختصر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        [StringLength(400, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string ShortDescription { get; set; }

        [Display(Name = "متن صفحه")]
        public string Text { get; set; }
        [Display(Name = "بازدید")]
        public int PageVisit { get; set; }
        [Display(Name = "کلمات کلیدی")]
        public string Tags { get; set; }
        [Display(Name = "تصویر")]
        public string ImageName { get; set; }
        [Display(Name = "تاریخ انتشار")]
        public DateTime CreateDate { get; set; }
        [Display(Name = "فعال")]
        public bool IsActive { get; set; }

        public PageGroup PageGroup { get; set; }
        public List<PageComment>  pageComments { get; set; }
    }
}
