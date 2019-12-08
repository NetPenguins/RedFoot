using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UIKit;
namespace PAlert
{
    public class MockDataStore : IDataStore<Item>
    {
        public List<Item> items;

        public MockDataStore()
        {
            items = new List<Item>();
            var _items = new List<Item>
            {
                new Item { Id = Guid.NewGuid().ToString(), Text = "Florida Panther", Description="The Florida panther is a North American cougar P. c. couguar population.", Wiki="https://en.wikipedia.org/wiki/Florida_panther", Image=UIImage.FromBundle("flpanther") },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Coyote", Description="The coyote, prairie wolf or brush wolf (Canis latrans) is a canine native to North America.", Wiki="https://en.wikipedia.org/wiki/Coyote", Image=UIImage.FromBundle("coyote") }
                
            };

            foreach (Item item in _items)
            {
                items.Add(item);
            }
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var _item = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var _item = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}
