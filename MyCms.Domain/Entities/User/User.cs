using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyCms.Domain.Entities.User
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Display(Name = "نقش کاربر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int RoleId { get; set; }
        [MaxLength(200)]
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string UserName { get; set; }
        [MaxLength(200)]
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
         [EmailAddress(ErrorMessage ="ایمیل وارد شده معتبر نمی باشد")]
        public string Email { get; set; }
        [MaxLength(200)]
        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Password { get; set; }

        [Display(Name = "فعال")]
        public bool IsActive { get; set; }
        [Display(Name = "کد فعال سازی")]
        [MaxLength(50)]
        public string ActiveCode { get; set; }
        [Display(Name = "تاریخ ثبت نام")]
        public DateTime CreateDate { get; set; }

        public Role Role { get; set; }

    }
}
