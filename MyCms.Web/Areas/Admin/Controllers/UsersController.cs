using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyCms.Core.Security;
using MyCms.Domain.Context;
using MyCms.Domain.Entities.User;
using MyCms.Domain.Repositories;

namespace MyCms.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: Admin/Users
        public async Task<IActionResult> Index()
        {
            // var myCmsContext = _context.Users.Include(u => u.Role);
            var myCmsContext = _userRepository.GetAllUser();
            //return View(await myCmsContext.ToListAsync());
            return View(myCmsContext.ToList());
        }

        // GET: Admin/Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = _userRepository.GetUserById(id.Value);
            //.Include(u => u.Role)
            //.FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            //    var user = await _context.Users
            //    .Include(u => u.Role)
            //    .FirstOrDefaultAsync(m => m.UserId == id);
            //if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Admin/Users/Create
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_userRepository.GetAllRoles(), "RoleId", "RoleTitle"); ;
            return View();
        }

        // POST: Admin/Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ActionName]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,RoleId,UserName,Email,Password,IsActive")] User user)
        {
            if (ModelState.IsValid)
            {
                user.ActiveCode = Guid.NewGuid().ToString();
                user.CreateDate = DateTime.Now;
                user.Password = PasswordHelper.EncodePasswordMd5(user.Password);
                _userRepository.AddUser(user);
                _userRepository.Save();
                //_context.Add(user);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_userRepository.GetAllRoles(), "RoleId", "RoleTitle", user.RoleId);
            return View(user);
        }

        // GET: Admin/Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var user = await _context.Users.FindAsync(id);
            var user = _userRepository.GetUserById(id.Value);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_userRepository.GetAllRoles(), "RoleId", "RoleTitle", user.RoleId);
            return View(user);
        }

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,RoleId,UserName,Email,Password,IsActive,ActiveCode,CreateDate")] User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(user);
                    //await _context.SaveChangesAsync();
                    _userRepository.EditUser(user);
                    _userRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!UserExists(user.UserId))
                    //{
                    //    return NotFound();
                    //}
                    //else
                    //{
                    //    throw;
                    //}
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_userRepository.GetAllRoles(), "RoleId", "RoleTitle", user.RoleId);
            return View(user);
        }

        private bool UserExists(int userId)
        {
            throw new NotImplementedException();
        }

        // GET: Admin/Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var user = await _context.Users
            //    .Include(u => u.Role)
            //    .FirstOrDefaultAsync(m => m.UserId == id);
            var user = _userRepository.GetUserById(id.Value);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var user = await _context.Users.FindAsync(id);
            //_context.Users.Remove(user);
            //await _context.SaveChangesAsync();
              _userRepository.DeleteUser(id);
            _userRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        //private bool UserExists(int id)
        //{
        //    return _context.Users.Any(e => e.UserId == id);
        //}
    }
}
