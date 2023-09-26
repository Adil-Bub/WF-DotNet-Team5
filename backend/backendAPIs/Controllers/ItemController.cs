using backend.Models;
using backend.Models.Request;
using backend.Services;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {

        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public async Task<ActionResult> GetItems()
        {

            var item = _itemService.GetAllItems();

            if (item == null || item.Count==0)
            {
                return NoContent();
            }
            return Ok(item)
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

        [HttpPut("{id}")]
        [Authorize(Roles = "admin,employee")]
        public async Task<ActionResult> UpdateItem(string id, [FromBody] UpdateItemRequest item)
        {
            if (item == null)
            {
                return BadRequest("Invalid item data");
            }

            if (id!=item.ItemId)
            {
                return BadRequest("ID mismatch");
            }

            bool updated = _itemService.UpdateItem(item);

            if (!updated)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DeleteItem(string id)
        {
            if (id==null)
            {
                return BadRequest("Please enter an item id");
            }

            var deletedItem = _itemService.DeleteItem(id);
            if (deletedItem == null)
            {
                return NotFound();
            }
            return Ok(deletedItem);

        }

    }
}

