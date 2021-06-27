namespace WebLabsV05.DAL.Entities
{
    public class PCPart
    {
        public int PCPartId { get; set; } // id комплектующего для ПК
        public string PCPartName { get; set; } // название комплектующего для ПК
        public string Description { get; set; } // описание комплектующего для ПК
        public decimal Price { get; set; } // цена
        public string Image { get; set; } // имя файла изображения
                                          // Навигационные свойства
        /// <summary>
        /// группа комплектующих (например, ЦПУ, SSD, HDD и т.д.)
        /// </summary>
        public int PCPartGroupId { get; set; }
        public PCPartGroup Group { get; set; }
    }
}
