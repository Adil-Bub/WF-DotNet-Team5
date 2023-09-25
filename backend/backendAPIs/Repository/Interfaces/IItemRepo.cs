using backend.Models.Request;
using backend.Models.Response;
using backend.Models;

namespace backendAPIs.Repository.Interfaces
{
    public interface IItemRepo
    {
        public ItemMaster? GetItemById(string itemId);
        public bool AddItem(ItemMaster item);
        public List<ItemMaster> GetAllItems();
        public bool UpdateItem(ItemMaster item);
        public ItemMaster DeleteItem(string itemId);
    }
}
