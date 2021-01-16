using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MyCms.Domain.Entities.Page;
using MyCms.Domain.Entities.User;

namespace MyCms.Domain.Context
{
   public class MyCmsContext:DbContext
    {
        public MyCmsContext(DbContextOptions<MyCmsContext> options):
            base(options)
        {
            
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<PageGroup> PageGroups { get; set; }
        public DbSet<Page> Pages  { get; set; }
        public DbSet<PageComment> PageComments { get; set; }

    }
}
