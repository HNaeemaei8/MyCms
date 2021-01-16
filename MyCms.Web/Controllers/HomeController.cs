using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCms.Domain.Repositories;
using MyCms.Domain.ViewModels;
using MyCms.Web.Models;
using MyCms.Core.Security;

namespace MyCms.Web.Controllers
{
    public class HomeController : Controller
    {
       
        public IActionResult Index()
        {
            return View();
        }
       

}
}
