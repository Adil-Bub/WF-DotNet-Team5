using backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly LoansContext _db;

        public ItemsController(LoansContext db)
        {
            _db = db;
        }


        [HttpGet]
        public async Task<ActionResult> GetItems()
        {
            return Ok(_db.ItemMasters);
        }

        [HttpGet]
        [Route("findById")]
        public async Task<ActionResult> GetItemById(string id)
        {
            ItemMaster? item = await _db.ItemMasters.FindAsync(id);
            if (item == null)
            {
                return BadRequest("Item not found!");
            }
            else
            {
                return Ok(item);
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddItem(ItemMaster item)
        {
            if(item != null && item.ItemId != null && await _db.ItemMasters.FindAsync(item.ItemId) != null)
            {
                return BadRequest("Item with id already exists");
            }
            try
            {
                _db.ItemMasters.Add(item);
                await _db.SaveChangesAsync();
                return Ok("Successfully added item!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateItem(ItemMaster item)
        {
            _db.ItemMasters.Update(item);
            await _db.SaveChangesAsync();
            return Ok("Updated item details successfully!");
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteItem(string id)
        {
            ItemMaster? item = await _db.ItemMasters.FindAsync(id);
            if (item == null)
            {
                return BadRequest("Item does not exist!");
            }
            else
            {
                _db.ItemMasters.Remove(item);
                await _db.SaveChangesAsync();
                return Ok("Item details removed!");
            }
        }
    }
}
