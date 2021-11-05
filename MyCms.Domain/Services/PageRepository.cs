using Microsoft.EntityFrameworkCore;
using MyCms.Domain.Context;
using MyCms.Domain.Entities.Page;
using MyCms.Domain.Repositories;
using MyCms.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public void AddComment(PageComment comment)
        {
             _ctx.PageComments.Add(comment);
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

        public void EditPage(Page page)
        {
            _ctx.Update(page);
        }

        public async Task<List<PageGroup>> GetAllGroup()
        {
            var list =await _ctx.PageGroups.ToListAsync();
            return list;    
        }

        public int GetCountComment(int pageId)
        {
            return _ctx.PageComments.Count(p => p.PageId == pageId);
        }

        public async Task<PageGroup> GetGroupById(int groupId)
        {
            return await _ctx.PageGroups.FindAsync(groupId);
        }

        public IEnumerable<ShowGroupViewModel> GetGroupsWithPageCount()
        {
            return _ctx.PageGroups.Include(p => p.Pages)
                .Select(s => new ShowGroupViewModel()
                {
                    GroupId = s.GroupId,
                    Title = s.Title,
                    PageCount = s.Pages.Count
                }).ToList();
        }

        public IEnumerable<Page> GetLatesPages(int take = 4)
        {
            return _ctx.Pages.OrderByDescending(p => p.CreateDate).Take(take);
        }

        public ArchiveViewModel GetPageBuyGroup(int groupID, int pageId = 1, int take = 10)
        {
            ArchiveViewModel result = new ArchiveViewModel();
            int skip = (pageId - 1) * take;
            result.PageCount = (_ctx.Pages.Count(p => p.GroupId == groupID) / take)+1;
            result.CurrentPage = pageId;
            result.Pages = _ctx.Pages.Where(p => p.GroupId == groupID).OrderByDescending(p=>p.CreateDate).Skip(skip).Take(take).ToList();

            return result;
        }

        public Page GetPageById(int PageId)
        {
            return _ctx.Pages.Find(PageId);
        }

        public IEnumerable<PageComment> GetPageComments(int pageId)
        {
            return _ctx.PageComments.Include(u=>u.User);
        }

        public IEnumerable<Page> GetPagesByInSlider(int take = 4)
        {
            return _ctx.Pages.Where(p => p.IsSlider).Take(take);
        }

        public void Save()
        {
            _ctx.SaveChanges();
        }

        public IEnumerable<Page> Search(string parameter)
        {
            return _ctx.Pages.Where(p=> EF.Functions.Like(p.Title,$"%{parameter}%") || EF.Functions.Like(p.ShortDescription, $"%{parameter}%") || EF.Functions.Like(p.Tags, $"%{parameter}%"));

        }

        public IEnumerable<Page> TopPages(int take = 4)
        {
            return _ctx.Pages.OrderByDescending(p => p.PageVisit)
                .Take(take);
            
        }
    }
}
