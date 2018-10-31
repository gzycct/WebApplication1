using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly WebApplication1.Models.BookContext _context;

        public IndexModel(WebApplication1.Models.BookContext context)
        {
            _context = context;
        }

        public IList<Book> Book { get;set; }

        public SelectList Authors;
        public string Author { get; set; }

        public string NameSort { get; set; }
        public string DateSort { get; set; }


        public async Task OnGetAsync(string author, string searchString, string sortOrder)
        {
            IQueryable<string> AuthorQuery = from m in _context.Book
                                             orderby m.Author
                                             select m.Author;

            var books = from m in _context.Book
                        select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(s => s.Name.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(author))
            {
                books = books.Where(x => x.Author == author);

            }
            Authors = new SelectList(await AuthorQuery.Distinct().ToListAsync());

            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            switch (sortOrder)
            {
                case "name_desc":
                    books = books.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    books = books.OrderBy(s => s.ReleaseDate);
                    break;
                case "date_desc":
                    books = books.OrderByDescending(s => s.ReleaseDate);

                    break;
                default:
                    books = books.OrderBy(s => s.Name);
                    break;
            }


            Book = await books.AsNoTracking().ToListAsync();
        }

    }
}
