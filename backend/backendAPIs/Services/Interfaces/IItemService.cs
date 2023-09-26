using backend.Models.Request;
using backend.Models.Response;
using backend.Models;

namespace backendAPIs.Services.Interfaces
{
    public interface IItemService
    {
        public List<ItemRespone>? GetAllItems();

        public ItemResponse? GetEmployeeById(string id);
        public bool UpdateItem(UpdateIemRequest item);
        public ItemResponse? DeleteIteme(string itemId);



    }
}
