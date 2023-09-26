using backendAPIs.Models;
using backendAPIs.Models.Request;
using backendAPIs.Models.Response;

namespace backendAPIs.Services.Interfaces
{
    public interface IItemService
    {
        public List<ItemResponse> GetAllItems();
        public ItemResponse GetItemById(string itemId);
        public string AddItem(AddItemRequest item);
        public bool UpdateItem(UpdateItemRequest item);
        public ItemResponse DeleteItem(string id);
    }
}
