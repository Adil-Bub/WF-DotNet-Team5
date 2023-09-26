using backendAPIs.Models;
using backendAPIs.Models.Request;
using backendAPIs.Models.Response;
using backendAPIs.Repository.Interfaces;
using backendAPIs.Services.Interfaces;
using backendAPIs.Util;

namespace backendAPIs.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepo _itemRepo;

        public ItemService(IItemRepo itemRepo)
        {
            _itemRepo = itemRepo;
        }
        public List<ItemResponse> GetAllItems()
        {
            var itemMasterList = _itemRepo.GetAllItems();
            var itemResponseList = itemMasterList.Select(item => new ItemResponse(item)).ToList();

            return itemResponseList;
        }

        public ItemResponse GetItemById(string itemId)
        {
            var itemMaster = _itemRepo.GetItemById(itemId);

            if(itemMaster == null)
            {
                return null;
            }
            return new ItemResponse(itemMaster);
        }

        public string AddItem(AddItemRequest item)
        {
            var itemMaster = new ItemMaster
            {
                ItemId = UIDGenerator.GenerateUniqueVarcharId("ITEM"),
                ItemDescription = item.ItemDescription,
                IssueStatus = item.IssueStatus,
                ItemMake = item.ItemMake,
                ItemCategory = item.ItemCategory,
                ItemValuation = item.ItemValuation,
            };

            var added = _itemRepo.AddItem(itemMaster);
            if(added)
            {
                return itemMaster.ItemId;
            }
            return null;
        }

        public bool UpdateItem(UpdateItemRequest item)
        {
            return _itemRepo.UpdateItem(item);
        }

        public ItemResponse DeleteItem(string id)
        {
            var itemMaster = _itemRepo.DeleteItem(id);
            if(itemMaster == null)
            {
                return null;
            }
            return new ItemResponse(itemMaster);
        }
    }
}
