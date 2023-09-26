using backend.Models.Request;
using backend.Models.Response;
using backend.Repository.Interfaces;
using backend.Services.Interfaces;

namespace backendAPIs.Services
{
    public class ItemSevice
    {
        public class ItemService : IItemService
        {
            private readonly IItemRepo _itemRepo;

            public ItemService(IItemRepo itemRepo)
            {
                _itemRepo = itemRepo;
            }
            public List<ItemResponse>? GetAllItems()
            {
                return _itemRepo.GetAllItems();
            }

            public ItemResponse? GetItemById(string id)
            {
                var item = _itemRepo.GetItemById(id);
                return (item!=null) ? new ItemResponse(item) : null;
            }

            public bool UpdateItem(UpdateItemRequest item)
            {
                return _itemRepo.UpdateItem(item);
            }

            public ItemResponse? DeleteItem(string itemId)
            {
                return _itemRepo.DeleteItem(itemId);
            }

        }
    }
}
