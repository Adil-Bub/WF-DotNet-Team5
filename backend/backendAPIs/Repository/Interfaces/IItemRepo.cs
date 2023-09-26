using backendAPIs.Models.Request;
using backendAPIs.Models.Response;
using backendAPIs.Models;

namespace backendAPIs.Repository.Interfaces
{
    public interface IItemRepo
    {
        public ItemMaster? GetItemById(string itemId);
        public bool AddItem(ItemMaster item);
        public List<ItemMaster> GetAllItems();
        public bool UpdateItem(UpdateItemRequest item);
        public ItemMaster? DeleteItem(string itemId);
    }
}
