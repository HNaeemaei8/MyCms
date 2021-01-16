using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCms.Web.Controllers
{
    [Authorize]
    public class TestController : Controller
    {
       
        public IActionResult Test1() => Content("Test1");

        [AllowAnonymous]
        public IActionResult Test2() => Content("Test2");

    }
}
