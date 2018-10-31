using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Utils;

namespace WebApplication1.Pages.Descri
{
    public class IndexModel : PageModel
    {
        private readonly WebApplication1.Models.BookContext _context;
        public IndexModel(WebApplication1.Models.BookContext context)
        {
            _context = context;
        }

        [BindProperty]
        public FileUpload FileUpload { get; set; }
        public IList<Describe> Describe { get; private set; }

        public async Task OnGetAsync()
        {
            Describe = await _context.Describe.AsNoTracking().ToListAsync();

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Describe = await _context.Describe.AsNoTracking().ToListAsync();
                return Page();
            }


            var publicDescribeData = await FileHelpers.ProcessFormFile(FileUpload.UploadPublicDescribe, ModelState);

            var privateDescribeData = await FileHelpers.ProcessFormFile(FileUpload.UploadPrivateDescribe, ModelState);

            if (!ModelState.IsValid)
            {
                Describe = await _context.Describe.AsNoTracking().ToListAsync();
                return Page();
            }


            var descr = new Describe()
            {
                PublicDescribe = publicDescribeData,
                PublicScheduleSize = FileUpload.UploadPublicDescribe.Length,
                PrivateDescribe = privateDescribeData,
                PrivateScheduleSize = FileUpload.UploadPrivateDescribe.Length,
                Name = FileUpload.FileName,
                UploadDateTime = DateTime.UtcNow
            };

            _context.Describe.Add(descr);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

    }
}