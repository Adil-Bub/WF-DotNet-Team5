using backendAPIs.Models;
using backendAPIs.Repository.Interfaces;
using backendAPIs.Models.Request;
using Microsoft.EntityFrameworkCore;

namespace backendAPIs.Repository
{
    public class ItemRepo : IItemRepo
    {
        private readonly LoansContext _db;

        public ItemRepo(LoansContext db) 
        {
            _db = db;
        }
        public bool AddItem(ItemMaster item)
        {
            try
            {
                _db.ItemMasters.Add(item);
                _db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public ItemMaster? DeleteItem(string itemId)
        {
            var item = _db.ItemMasters.FirstOrDefault(i => i.ItemId == itemId);

            if(item == null)
            {
                return null;
            }
            try
            {
                _db.ItemMasters.Remove(item);
                _db.SaveChanges();

                return item;
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public List<ItemMaster> GetAllItems()
        {
            return _db.ItemMasters.ToList();
        }

        public ItemMaster? GetItemById(string itemId)
        {
            return _db.ItemMasters.FirstOrDefault(item => item.ItemId == itemId);
        }

        public bool UpdateItem(UpdateItemRequest item)
        {
            var existingItem = _db.ItemMasters.FirstOrDefault(i => i.ItemId == item.ItemId);
            
            if(existingItem !=null)
            {
                existingItem.ItemDescription = item.ItemDescription ?? existingItem.ItemDescription;
                existingItem.IssueStatus = item.IssueStatus ?? existingItem.IssueStatus;
                existingItem.ItemMake = item.ItemMake ?? existingItem.ItemMake;
                existingItem.ItemCategory = item.ItemCategory ?? existingItem.ItemCategory;
                existingItem.ItemValuation = item.ItemValuation;

                var entry = _db.Entry(existingItem);
                if (entry.State == EntityState.Modified || entry.State == EntityState.Added)
                    entry.State = EntityState.Detached;
                _db.Entry(existingItem).State = EntityState.Modified;

                try
                {
                    _db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            return false;
        }
    }
}
