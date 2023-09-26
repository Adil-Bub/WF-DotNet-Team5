using backendAPIs.Models;
using backendAPIs.Models.Response;
using backendAPIs.Models.Request;
using backendAPIs.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using backendAPIs.Services;
using Microsoft.AspNetCore.Authorization;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemsController(IItemService itemService)
        {
            _itemService = itemService;
        }


        [HttpGet("all")]
        [Authorize(Roles = "admin,employee")]
        public async Task<ActionResult> GetAllItems()
        {
            var result = _itemService.GetAllItems();

            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin,employee")]
        public async Task<ActionResult> GetItemById(string id)
        {
            ItemResponse? item = _itemService.GetItemById(id);
            if (item == null)
            {
                return BadRequest("Item not found!");
            }
            else
            {
                return Ok(item);
            }
        }

        [HttpPost("add")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> AddItem(AddItemRequest item)
        {
            if(item == null)
            {
                return BadRequest("Plese enter valid details");
            }
            string itemId = _itemService.AddItem(item);
            
            if(itemId == null)
            {
                return StatusCode(500, "Failed to add item. Please try again!");
            }
            var response = new { itemId = $"{itemId}" };
            return Ok(response);
        }

        [HttpPut("{item-id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> UpdateItem([FromRoute(Name = "item-id")] string itemId, UpdateItemRequest item)
        {
            if(item.ItemId != itemId)
            {
                return BadRequest("ID mismatch");
            }
            var isUpdated = _itemService.UpdateItem(item);
            if(!isUpdated)
            {
                return StatusCode(500, "Failed to update item details. please try again");
            }
            return NoContent();

           
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DeleteItem(string id)
        {
            if (id==null)
            {
                return BadRequest("Please enter an Item id");
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
