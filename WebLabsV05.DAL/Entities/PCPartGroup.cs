using System.Collections.Generic;

namespace WebLabsV05.DAL.Entities
{
    public class PCPartGroup
    {
        public int PCPartGroupId { get; set; }
        public string GroupName { get; set; }
        /// <summary>
        /// Навигационное свойство 1-ко-многим
        /// </summary>
        public List<PCPart> PCParts { get; set; }
    }
}
