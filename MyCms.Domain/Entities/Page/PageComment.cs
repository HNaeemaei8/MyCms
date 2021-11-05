using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyCms.Domain.Entities.Page
{
   public class PageComment
    {
        [Key]
        public int CommentId { get; set; }

        public int PageId { get; set; }
        public int UserId { get; set; }
        [Required]
        public string Comment { get; set; }
        public DateTime CreateDate { get; set; }

        public Page Page { get; set; }
        public User.User User { get; set; }

    }
}
