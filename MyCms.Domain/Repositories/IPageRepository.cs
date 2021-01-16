using MyCms.Domain.Entities.Page;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyCms.Domain.Repositories
{
    interface IPageRepository
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

        void Save();
    }
}
