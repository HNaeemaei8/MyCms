using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCms.Core.Security;
using MyCms.Domain.Repositories;
using MyCms.Domain.ViewModels;

namespace MyCms.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    public class HomeController : Controller
    {
        private readonly IUserRepository _userRepository;

        public HomeController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ChangePassword()
        {
            return View();
        }


        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordViewModel change)
        {
            var user = _userRepository.GetUserByUserName(User.Identity.Name);
            if (user.Password != PasswordHelper.EncodePasswordMd5(change.OldPassword))
            {
                ModelState.AddModelError("Password", "کلمه عبور فعلی صحیح نیست");
                return View();
            }
            user.Password = PasswordHelper.EncodePasswordMd5(change.Password);
            _userRepository.EditUser(user);
            _userRepository.Save();
            ViewBag.IsSuccess = true;
            return View();
        }
    }
}