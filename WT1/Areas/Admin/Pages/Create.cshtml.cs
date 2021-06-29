using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebLabsV05.DAL.Data;
using WebLabsV05.DAL.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace WT1.Areas.Admin.Pages
{
    public class CreateModel : PageModel
    {
        private readonly WebLabsV05.DAL.Data.ApplicationDbContext _context;

        private readonly IWebHostEnvironment _environment;

        public CreateModel(WebLabsV05.DAL.Data.ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _environment = env;
        }

        public IActionResult OnGet()
        {
        ViewData["PCPartGroupId"] = new SelectList(_context.PCPartGroups, "PCPartGroupId", "GroupName");
            return Page();
        }

        [BindProperty]
        public PCPart PCPart { get; set; }
        [BindProperty]
        public IFormFile Image { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.PCParts.Add(PCPart);
            await _context.SaveChangesAsync();
            if (Image != null)
            {
                var fileName = $"{PCPart.PCPartId}" +
                Path.GetExtension(Image.FileName);
                PCPart.Image = fileName;
                var path = Path.Combine(_environment.WebRootPath, "images",
                fileName);
                using (var fStream = new FileStream(path, FileMode.Create))
                {
                    await Image.CopyToAsync(fStream);
                }
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("./Index");
        }
    }
}
