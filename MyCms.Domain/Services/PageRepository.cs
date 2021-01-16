using Microsoft.EntityFrameworkCore;
using MyCms.Domain.Context;
using MyCms.Domain.Entities.Page;
using MyCms.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyCms.Domain.Services
{
    public class PageRepository : IPageRepository
    {
        MyCmsContext _ctx;
        public PageRepository(MyCmsContext ctx)
        {
            _ctx = ctx;
        }
        public async Task AddGroup(PageGroup group)
        {
            await  _ctx.PageGroups.AddAsync(group);
        }

        public  Task DeleteGroup(PageGroup group)
        {
             _ctx.PageGroups.Remove(group);
            return Task.CompletedTask;
        }

        public Task DeleteGroup(int groupId)
        {
            var group = GetGroupById(groupId);
            DeleteGroup(group.Result);
            return Task.CompletedTask;
        }

        public  Task EditGroup(PageGroup group)
        {
             _ctx.PageGroups.Update(group);
            return Task.CompletedTask;

        }

        public async Task<List<PageGroup>> GetAllGroup()
        {
            var list =await _ctx.PageGroups.ToListAsync();
            return list;    
        }

        public async Task<PageGroup> GetGroupById(int groupId)
        {
            return await _ctx.PageGroups.FindAsync(groupId);
        }

        public void Save()
        {
            _ctx.SaveChanges();
        }
    }
}
