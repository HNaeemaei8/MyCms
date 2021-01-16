using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MyCms.Core.Convertor;
using MyCms.Core.Security;
using MyCms.Core.Senders;
using MyCms.Domain.Entities.User;
using MyCms.Domain.Repositories;
using MyCms.Domain.ViewModels;

namespace MyCms.Web.Controllers
{
    public class AccountController : Controller
    {
        private IUserRepository _userRepository;

        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }


        [Route("Login")]
        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            var User = _userRepository.LoginUser(login.Email.FixText(), PasswordHelper.EncodePasswordMd5(login.Password));
            if (User == null)
            {
                ModelState.AddModelError("Email", "کاربری یافت نشد ");
                return View(login);
            }
            if (!User.IsActive)
            {
                ModelState.AddModelError("Email", "حساب کاربری شما فعال نشده است ");
                return View(login);
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,login.Email),
               // new Claim(ClaimTypes.NameIdentifier,login.Email),
                new Claim(ClaimTypes.Name,User.UserName),
                 //new Claim("RoleId",User.RoleId.ToString()),
                 new Claim("RoleId",_userRepository.GetRoleNameByRoleId(User.RoleId)),
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var pricipal = new ClaimsPrincipal(identity);

            var properties = new AuthenticationProperties()
            {
                IsPersistent = login.RememberMe
            };

            HttpContext.SignInAsync(pricipal, properties);

            return Redirect("/");
        }

        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [Route("Register")]
        [HttpPost]
        public IActionResult Register(RegisterViewModel register)
        {
            if (ModelState.IsValid)
            {
                if (!_userRepository.IsExistUserName(register.UserName.Trim().ToLower()))
                {
                    if (!_userRepository.IsExistEmail(register.Email.ToLower().Trim()))
                    {
                        Domain.Entities.User.User user=new User()
                        {
                            UserName = register.UserName.ToLower().Trim(),
                            Email = register.Email.ToLower().Trim(),
                            ActiveCode = Guid.NewGuid().ToString(),
                            CreateDate = DateTime.Now,
                            IsActive = false,
                            Password = PasswordHelper.EncodePasswordMd5(register.Password),
                            RoleId = 1
                        };
                        _userRepository.AddUser(user);
                        _userRepository.Save();
                        string body = $"<h1>{user.UserName} عزیز</h1>" +
                                      $"<p>با تشکر از ثبت نام شما .</p>" +
                                      $"<p>جهت فعالسازی حساب خود روی لینک زیر کلیک کنید</p>" +
                                      $"<a href='http://localhost:58318/Account/ActiveUser/{user.ActiveCode}'>فعالسازی</a>";
                        SendEmail.Send(user.Email,"ایمیل فعالسازی",body);

                        return View("SuccessRegister", user);
                    }
                    else
                    {
                        ViewBag.IsEmailExist = true;
                    }
                }
                else
                {
                    ViewBag.IsUserNameExist = true;
                }
            }
            return View(register);
        }

        public bool CheckEmail(string email)
        {
            return _userRepository.IsExistEmail(email.ToLower().Trim());
        }

        public IActionResult ActiveUser(string id)
        {
            bool IsActive = _userRepository.ActiveUser(id);
            return View(IsActive);
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Login");
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ForgotPassword(RecoveryPasswordViewModel recovery)

        {
            var user = _userRepository.GetUerByEmail(recovery.Email.FixText());
            if (user == null)
            {
                return NotFound();
            }
            string Body = $"جهت بازیابی حساب خود روی لینک زیر کلیک کنید " +
                "<a href='http://localhost:58318/Account/RecoveryPassword/{user.ActiveCode}'>بازیابی</a>";
            SendEmail.Send(user.Email, "بازیابی", Body);
            ViewBag.IsSuccess = true;
            return View();
        }
        public IActionResult RecoveryPassword(string id)
        {
            return View(new ForgotPasswordViewModel()
            {
                ActiveCode = id
            }); ;
        }
        [HttpPost]
        public IActionResult RecoveryPassword(ForgotPasswordViewModel forgot)
        {
            var user = _userRepository.GetUserByActiveCode(forgot.ActiveCode);
            if (user == null)
            
                return NotFound();
            
                user.Password = PasswordHelper.EncodePasswordMd5(forgot.Password);
                user.ActiveCode = Guid.NewGuid().ToString();
                _userRepository.EditUser(user);
                _userRepository.Save();
         
            TempData["ChangePass"] = "True";
      //      TempData.Keep();
            return Redirect("/Login");
            }
        }

    }
