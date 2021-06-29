using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebLabsV05.DAL.Data;
using WebLabsV05.DAL.Entities;

namespace WT1.Areas.Admin.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly WebLabsV05.DAL.Data.ApplicationDbContext _context;

        public DeleteModel(WebLabsV05.DAL.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PCPart PCPart { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PCPart = await _context.PCParts
                .Include(p => p.Group).FirstOrDefaultAsync(m => m.PCPartId == id);

            if (PCPart == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PCPart = await _context.PCParts.FindAsync(id);

            if (PCPart != null)
            {
                _context.PCParts.Remove(PCPart);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
