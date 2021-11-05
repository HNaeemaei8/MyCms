using MyCms.Domain.Entities.Page;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCms.Domain.ViewModels
{
    public class ShowGroupViewModel
    {
        public int GroupId { get; set; }
        public string Title { get; set; }
        public int PageCount { get; set; }
    }
    public class ArchiveViewModel
    {
        public List<Page> Pages { get; set; }
        public int PageCount { get; set; }
        public int CurrentPage { get; set; }

    }
}
