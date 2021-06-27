using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebLabsV05.DAL.Entities;
using WT1.Models;

namespace WT1.Controllers
{
    public class ProductController : Controller
    {
        public List<PCPart> _pcParts;
        List<PCPartGroup> _pcPartGroups;

        int _pageSize;

        public ProductController()
        {
            _pageSize = 3;
            SetupData();
        }

        public IActionResult Index(int? group, int pageNo = 1)
        {
            var pcPartsFiltered = _pcParts
                                .Where(d => !group.HasValue || d.PCPartGroupId == group.Value);

            // Поместить список групп во ViewData
            ViewData["Groups"] = _pcPartGroups;
            // Получить id текущей группы и поместить в TempData
            ViewData["CurrentGroup"] = group ?? 0;

            return View(ListViewModel<PCPart>.GetModel(pcPartsFiltered, pageNo, _pageSize));            
        }

        /// <summary>
        /// Инициализация списков
        /// </summary>
        private void SetupData()
        {
            _pcPartGroups = new List<PCPartGroup>
            {
                new PCPartGroup {PCPartGroupId=1, GroupName="Процессоры"},
                new PCPartGroup {PCPartGroupId=2, GroupName="SSD"},
                new PCPartGroup {PCPartGroupId=3, GroupName="HDD"},                
            };
            _pcParts = new List<PCPart>
            {
                new PCPart {PCPartId = 1, PCPartName = "Процессор AMD Ryzen 5 3600",
                Description="Matisse, AM4, 6 ядер, частота 4.2/3.6 ГГц, кэш 3 МБ + 32 МБ, техпроцесс 7 нм, TDP 65W",
                Price = 475.00M, PCPartGroupId = 1, Image = "ryzen5.jpeg" },
                new PCPart {PCPartId = 2, PCPartName = "Процессор Intel Core i5-10400F",
                Description="Comet Lake, LGA1200, 6 ядер, частота 4.3/2.9 ГГц, кэш 12 МБ, техпроцесс 14 нм, TDP 65W",
                Price = 431.56M, PCPartGroupId = 1, Image = "corei5.jpeg" },
                new PCPart {PCPartId = 3, PCPartName = "SSD Crucial BX500 240GB",
                Description="2.5\", SATA 3.0, контроллер Silicon Motion SM2258XT, микросхемы 3D TLC NAND, последовательный доступ: 540/500 MBps",
                Price = 95.98M, PCPartGroupId = 2, Image = "crucialbx500.jpeg" },
                new PCPart {PCPartId = 4, PCPartName = "SSD Samsung 870 Evo 250GB",
                Description="2.5\", SATA 3.0, контроллер Samsung MKX, микросхемы 3D TLC NAND, последовательный доступ: 560/530 MBps, случайный доступ: 98000/88000 IOps",
                Price = 128.12M, PCPartGroupId = 2, Image = "samsung870evo.jpeg" },
                new PCPart {PCPartId = 5, PCPartName = "Жесткий диск Seagate Barracuda 2TB",
                Description="3.5\", SATA 3.0 (6Gbps), 7200 об/мин, буфер 256 МБ",
                Price = 157.20M, PCPartGroupId = 3, Image = "seagate.jpeg" },

            };
        }
    }
}
