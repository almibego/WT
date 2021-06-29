using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebLabsV05.DAL.Data;
using WebLabsV05.DAL.Entities;
using WT1.Models;
using WT1.Extensions;


namespace WT1.Controllers
{
    public class ProductController : Controller
    {
        ApplicationDbContext _context;
        int _pageSize;

        public ProductController(ApplicationDbContext context)
        {
            _pageSize = 3;
            _context = context;
        }


        [Route("Catalog")]
        [Route("Catalog/Page_{pageNo}")]
        public IActionResult Index(int? group, int pageNo = 1)
        {
            var pcPartsFiltered = _context.PCParts
                                .Where(d => !group.HasValue || d.PCPartGroupId == group.Value);

            // Поместить список групп во ViewData
            ViewData["Groups"] = _context.PCPartGroups;
            // Получить id текущей группы и поместить в TempData
            ViewData["CurrentGroup"] = group ?? 0;

            var model = ListViewModel<PCPart>.GetModel(pcPartsFiltered, pageNo, _pageSize);
            if (Request.IsAjaxRequest())
                return PartialView("_listpartial", model);
            else
                return View(model);
        }        
    }
}
