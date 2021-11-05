using Microsoft.AspNetCore.Mvc;
using MyCms.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCms.Web.Components
{
    public class SearchController : Controller
    {
         private IPageRepository _pageRepository;
        public SearchController(IPageRepository pageRepository)
        {
            _pageRepository = pageRepository;
        }
        public IActionResult Index(string q)
        {
            return View(_pageRepository.Search(q));
        }
    }
}
