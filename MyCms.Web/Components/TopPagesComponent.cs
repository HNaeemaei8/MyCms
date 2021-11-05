using Microsoft.AspNetCore.Mvc;
using MyCms.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCms.Web.Components
{
    public class TopPagesComponent:ViewComponent
    {
        private readonly IPageRepository _pageRepository;
        public TopPagesComponent(IPageRepository pageRepository)
        {
            _pageRepository = pageRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("/Views/News/TopPageComponent.cshtml",_pageRepository.TopPages());
        }

    }
}
