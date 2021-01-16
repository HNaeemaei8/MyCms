using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyCms.Domain.Entities.User
{
   public class Role
    {
        [Key]
        public int RoleId { get; set; }
        public string RoleTitle { get; set; }
        public string RoleName { get; set; }


        public List<User> Users { get; set; }
    }
}
