using MyCms.Domain.Entities.Page;
using MyCms.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyCms.Domain.Repositories
{
   public interface IPageRepository
    {
        #region Page Group

        //List<PageGroup> GetAllGroup();
        //PageGroup GetGroupById(int groupId);
        //void AddGroup(PageGroup group);
        //void EditGroup(PageGroup group);
        //void DeleteGroup(PageGroup group);
        //void DeleteGroup(int groupId);
        Task<List<PageGroup>> GetAllGroup();
        Task<PageGroup> GetGroupById(int groupId);
        Task AddGroup(PageGroup group);
        Task EditGroup(PageGroup group);
        Task DeleteGroup(PageGroup group);
        Task DeleteGroup(int groupId);

        #endregion

        #region Pages

        Page GetPageById(int PageId);
        void EditPage(Page page);
        IEnumerable<Page> Search(string parameter);
        IEnumerable<Page> GetPagesByInSlider(int take = 4);
        IEnumerable<Page> GetLatesPages(int take = 4);
        IEnumerable<ShowGroupViewModel> GetGroupsWithPageCount();
        IEnumerable<Page> TopPages(int take = 4);
        ArchiveViewModel GetPageBuyGroup(int groupID,int pageId=1, int take= 10);
        #endregion

        #region Page Comment

        int GetCountComment(int pageId);
        void AddComment(PageComment comment);
        IEnumerable<PageComment> GetPageComments(int pageId); 
        #endregion

        void Save();
    }
}
