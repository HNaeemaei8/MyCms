using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyCms.Core.Security;
using MyCms.Domain.Context;
using MyCms.Domain.Entities.Page;

namespace MyCms.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PagesController : Controller
    {
        private readonly MyCmsContext _context;

        public string Deletepath { get; private set; }

        public PagesController(MyCmsContext context)
        {
            _context = context;
        }

        // GET: Admin/Pages
        public async Task<IActionResult> Index()
        {
            var myCmsContext = _context.Pages.Include(p => p.PageGroup);
            return View(await myCmsContext.ToListAsync());
        }

        // GET: Admin/Pages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var page = await _context.Pages
                .Include(p => p.PageGroup)
                .FirstOrDefaultAsync(m => m.PageId == id);
            if (page == null)
            {
                return NotFound();
            }

            return View(page);
        }

        // GET: Admin/Pages/Create
        public IActionResult Create()
        {
            ViewData["GroupId"] = new SelectList(_context.PageGroups, "GroupId", "Title");
            return View();
        }

        // POST: Admin/Pages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PageId,GroupId,Title,ShortDescription,Text,PageVisit,Tags,ImageName,CreateDate,IsActive")] Page page , IFormFile ImgUp)
        {
            if (ModelState.IsValid)
            {
                
                page.PageVisit = 0;
                if (ImgUp != null && ImgUp.IsImage())
                {
                    page.ImageName = Guid.NewGuid().ToString() + Path.GetExtension(ImgUp.FileName);
                    string savePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/PageImages", page.ImageName);
                    using (var stream=new FileStream(savePath,FileMode.Create) )
                    {
                        ImgUp.CopyTo(stream);
                    }
                }
                _context.Add(page);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupId"] = new SelectList(_context.PageGroups, "GroupId", "Title", page.GroupId);
            return View(page);
        }

        // GET: Admin/Pages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var page = await _context.Pages.FindAsync(id);
            if (page == null)
            {
                return NotFound();
            }
            ViewData["GroupId"] = new SelectList(_context.PageGroups, "GroupId", "Title", page.GroupId);
            return View(page);
        }

        // POST: Admin/Pages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PageId,GroupId,Title,ShortDescription,Text,PageVisit,Tags,ImageName,CreateDate,IsActive")] Page page, IFormFile ImgUp)
        {
            if (id != page.PageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (ImgUp != null && ImgUp.IsImage())
                    {
                        if (page.ImageName !=null)
                        {
                            string DeletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/PageImages", page.ImageName);
                            if (System.IO.File.Exists(Deletepath))
                            {
                                System.IO.File.Delete(DeletePath);
                            }
                        }
                        page.ImageName = Guid.NewGuid().ToString() + Path.GetExtension(ImgUp.FileName);
                        string savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/PageImages", page.ImageName);
                        using (var stream = new FileStream(savePath, FileMode.Create))
                        {
                            ImgUp.CopyTo(stream);
                        }
                    }
                    _context.Update(page);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PageExists(page.PageId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupId"] = new SelectList(_context.PageGroups, "GroupId", "Title", page.GroupId);
            return View(page);
        }

        // GET: Admin/Pages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var page = await _context.Pages
                .Include(p => p.PageGroup)
                .FirstOrDefaultAsync(m => m.PageId == id);
            if (page == null)
            {
                return NotFound();
            }

            return View(page);
        }

        // POST: Admin/Pages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var page = await _context.Pages.FindAsync(id);
            _context.Pages.Remove(page);
            string DeletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/PageImages", page.ImageName);
            if (System.IO.File.Exists(Deletepath))
            {
                System.IO.File.Delete(DeletePath);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PageExists(int id)
        {
            return _context.Pages.Any(e => e.PageId == id);
        }
    }
}
