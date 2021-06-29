using System.Collections.Generic;
using System.Linq;
using WebLabsV05.DAL.Entities;

namespace WT1.Models
{
    public class Cart
    {
        public Dictionary<int, CartItem> Items { get; set; }
        public Cart()
        {
            Items = new Dictionary<int, CartItem>();
        }
        /// <summary>
        /// Количество объектов в корзине
        /// </summary>
        public int Count
        {
            get
            {
                return Items.Sum(item => item.Value.Quantity);
            }
        }
        /// <summary>
        /// Общая сумма заказа
        /// </summary>
        public decimal Prices
        {
            get
            {
                return Items.Sum(item => item.Value.Quantity * item.Value.PCPart.Price);
            }
        }
        /// <summary>
        /// Добавление в корзину
        /// </summary>
        /// <param name="pcPart">добавляемый объект</param>
        public virtual void AddToCart(PCPart pcPart)
        {
            // если объект есть в корзине
            // то увеличить количество
            if (Items.ContainsKey(pcPart.PCPartId))
                Items[pcPart.PCPartId].Quantity++;
            // иначе - добавить объект в корзину
            else
                Items.Add(pcPart.PCPartId, new CartItem
                {
                    PCPart = pcPart,
                    Quantity = 1
                });
        }
        /// <summary>
        /// Удалить объект из корзины
        /// </summary>
        /// <param name="id">id удаляемого объекта</param>
        public virtual void RemoveFromCart(int id)
        {
            Items.Remove(id);
        }
        /// <summary>
        /// Очистить корзину
        /// </summary>
        public virtual void ClearAll()
        {
            Items.Clear();
        }
    }
    /// <summary>
    /// Клаcс описывает одну позицию в корзине
    /// </summary>
    public class CartItem
    {
        public PCPart PCPart { get; set; }
        public int Quantity { get; set; }
    }
}
