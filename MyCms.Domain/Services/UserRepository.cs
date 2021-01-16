using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MyCms.Domain.Context;
using MyCms.Domain.Entities.User;
using MyCms.Domain.Repositories;

namespace MyCms.Domain.Services
{
    public class UserRepository:IUserRepository
    {
        private MyCmsContext _ctx;

        public UserRepository(MyCmsContext ctx)
        {
            _ctx = ctx;
        }
        public IEnumerable<User> GetAllUser()
        {
            return _ctx.Users.Include(u => u.Role);
        }

        public User GetUserById(int userId)
        {
            return _ctx.Users.Find(userId);
        }

        public void AddUser(User user)
        {
            _ctx.Add(user);
        }

        public void EditUser(User user)
        {
            _ctx.Update(user);
        }

        public void DeleteUser(User user)
        {
            _ctx.Remove(user);
        }

        public void DeleteUser(int userId)
        {
            var user = GetUserById(userId);
            DeleteUser(userId);
        }

        public bool IsExistEmail(string email)
        {
            return _ctx.Users.Any(u=>u.Email==email);
        }

        public bool IsExistUserName(string userName)
        {
            return _ctx.Users.Any(u => u.UserName == userName);
        }

        public bool ActiveUser(string activeCode)
        {
            var user = _ctx.Users.SingleOrDefault(u => u.ActiveCode == activeCode);
            if (user == null)
                return false;
            user.IsActive = true;
            user.ActiveCode = Guid.NewGuid().ToString();
            EditUser(user);
            Save();
            return true;
        }

        public void Save()
        {
            _ctx.SaveChanges();
        }

        public void Dispose()
        {
            _ctx?.Dispose();
        }

        public User LoginUser(string email, string password)
        {
            return _ctx.Users.SingleOrDefault(u => u.Email == email && u.Password == password);

        }

        public User GetUserByUserName(string username)
        {
            return _ctx.Users.SingleOrDefault(u => u.UserName == username);
        }

        public string GetRoleNameByRoleId(int roldeId)
        {
            return _ctx.Roles.Find(roldeId).RoleName;
        }

        public IEnumerable<Role> GetAllRoles()
        {
            return _ctx.Roles.ToList();
        }

        public User GetUerByEmail(string email)
        {
            return _ctx.Users.SingleOrDefault(u => u.Email == email);
        }

        public User GetUserByActiveCode(string code)
        {
            return _ctx.Users.SingleOrDefault(u => u.ActiveCode == code);
        }
    }
}
