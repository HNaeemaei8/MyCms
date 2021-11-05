using System;
using System.Collections.Generic;
using System.Text;
using MyCms.Domain.Entities.User;

namespace MyCms.Domain.Repositories
{
    public interface IUserRepository:IDisposable
    {
        IEnumerable<User> GetAllUser();
        User GetUserById(int userId);

        User GetUserByUserName(string username);
        User GetUerByEmail(string email);
        User GetUserByActiveCode(string code);
        void AddUser(User user);
        void EditUser(User user);
        void DeleteUser(User user);
        void DeleteUser(int userId);

        bool IsExistEmail(string email);
        bool IsExistUserName(string userName);
        bool ActiveUser(string activeCode);
        void Save();

        #region Auth

        User LoginUser(string email, string password);
        int GetUserIdByUserName(string username);

        #endregion

        #region Role

        string GetRoleNameByRoleId(int roldeId);
        IEnumerable<Role> GetAllRoles();

        #endregion
    }
}
