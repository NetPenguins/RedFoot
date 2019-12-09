using System;

namespace PAlert
{
    /// <summary>
    /// Model for the item detail view
    /// </summary>
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public ItemDetailViewModel(Item item = null)
        {
            if (item != null)
            {
                Title = item.Name;
                Item = item;
            }
        }
    }
}
