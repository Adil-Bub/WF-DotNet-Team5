using backend.Models.Request;
using backend.Models.Response;
using backend.Models;
using backendAPIs.Models.Response;

namespace backendAPIs.Repository.Interfaces
{
    public interface IItemRepo
    {
        public ItemMaster? GetItemById(string itemId);
        public bool AddItem(ItemMaster item);
        public List<ItemResponse> GetAllItems();
        public bool UpdateItem(ItemMaster item);
        public ItemResponse DeleteItem(string itemId);
    }
}
