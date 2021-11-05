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
using MyCms.Domain.Services;

namespace MyCms.Web.Controllers
{
    public class HomeController : Controller
    {
        IPageRepository _pageRepository;
        public HomeController(IPageRepository pageRepository)
        {
            _pageRepository = pageRepository;
        }   

        public IActionResult Index()
        {
            var sliders = _pageRepository.GetPagesByInSlider();
            var lates = _pageRepository.GetLatesPages();
            return View(Tuple.Create(sliders,lates));
        }
       

}
}
