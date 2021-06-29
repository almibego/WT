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
    public class IndexModel : PageModel
    {
        private readonly WebLabsV05.DAL.Data.ApplicationDbContext _context;

        public IndexModel(WebLabsV05.DAL.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<PCPart> PCPart { get;set; }

        public async Task OnGetAsync()
        {
            PCPart = await _context.PCParts
                .Include(p => p.Group).ToListAsync();
        }
    }
}
