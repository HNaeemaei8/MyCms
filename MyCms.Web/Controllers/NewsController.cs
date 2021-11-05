using Microsoft.AspNetCore.Mvc;
using MyCms.Domain.Entities.Page;
using MyCms.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCms.Web.Controllers
{
    public class NewsController : Controller
    {
        IPageRepository _pageRepository;
        IUserRepository _UserRepository;
        public NewsController(IPageRepository pageRepository, IUserRepository userRepository)
        {
            _pageRepository = pageRepository;
            _UserRepository = userRepository;
        }

        [Route("Group/{id}/{title}")]
        public IActionResult ShowPagesByGroup(int id,string title, int pageId = 1)
        {
            ViewBag.GroupTitle = title;
            return View(_pageRepository.GetPageBuyGroup(id,pageId));
        }
        [Route("News/{id}")]
        public IActionResult ShowNews(int id)
        {
            var news = _pageRepository.GetPageById(id);

            if (news ==null)
            {
                return NotFound();
            }
            news.PageVisit += 1;
            _pageRepository.EditPage(news);
            _pageRepository.Save();
            ViewBag.CountComment = _pageRepository.GetCountComment(id);
            return View(news);


        }
        public void AddComment(int id, string comment)
        {
            PageComment AddComment = new PageComment()
            {
                Comment = comment,
                CreateDate = DateTime.Now,
                UserId = _UserRepository.GetUserIdByUserName(User.Identity.Name),
                PageId=id,
                
                

            };
            _pageRepository.AddComment(AddComment);
            _pageRepository.Save();
            
        }
        public IActionResult PageComment(int id)
        {
            return PartialView(_pageRepository.GetPageComments(id));
        }
    }
}
