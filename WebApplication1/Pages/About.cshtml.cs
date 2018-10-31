using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Pages
{
    public class AboutModel : PageModel
    {
        private readonly WebApplication1.Models.BookContext _context;

        public AboutModel(WebApplication1.Models.BookContext context)
        {
            _context = context;
        }
        public IList<EnrollmentPublishGroup> BookGroup { get; set; }
        public string Message { get; set; }

        public async Task OnGetAsync()
        {

            IQueryable<EnrollmentPublishGroup> data =
                from book in _context.Book
                group book by book.Publishing into publishGroup
                select new EnrollmentPublishGroup()
                {

                    Publish = publishGroup.Key,
                    BooksCount = publishGroup.Count()

                };
            BookGroup = await data.AsNoTracking().ToListAsync();

        }

    }
}
