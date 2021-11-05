using Microsoft.AspNetCore.Mvc;
using MyCms.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCms.Web.Components
{
    public class ShowGroupsComponent:ViewComponent
    {
       private readonly IPageRepository _pageRepository;
         public ShowGroupsComponent(IPageRepository pageRepository)
        {
            _pageRepository = pageRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("/Views/News/ShowGroupsComponent.cshtml", _pageRepository.GetGroupsWithPageCount());
        }
    }
}
