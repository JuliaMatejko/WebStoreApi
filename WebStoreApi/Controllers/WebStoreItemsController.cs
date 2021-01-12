using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebStoreApi.Models;

namespace WebStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebStoreItemsController : ControllerBase
    {
        private readonly WebStoreContext _context;

        public WebStoreItemsController(WebStoreContext context)
        {
            _context = context;
        }

        // GET: api/WebStoreItems/GetAllItems
        [HttpGet("GetAllItems")]
        public async Task<ActionResult<IEnumerable<WebStoreItemDTO>>> GetWebStoreItems()
        {
            return await _context.WebStoreItems
                .Select(x => ItemToDTO(x))
                .ToListAsync();
        }

        // GET: api/WebStoreItems/GetItem5
        [HttpGet("GetItem{id}")]
        public async Task<ActionResult<WebStoreItemDTO>> GetWebStoreItem(ulong id)
        {
            var webStoreItem = await _context.WebStoreItems.FindAsync(id);

            if (webStoreItem == null)
            {
                return NotFound();
            }

            return ItemToDTO(webStoreItem);
        }

        // PUT: api/WebStoreItems/UpdateItem5
        [HttpPut("UpdateItem{id}")]
        public async Task<IActionResult> UpdateWebStoreItem(ulong id, WebStoreItemDTO webStoreItemDTO)
        {
            if (id != webStoreItemDTO.Id)
            {
                return BadRequest();
            }

            var webStoreItem = await _context.WebStoreItems.FindAsync(id);
            if (webStoreItem == null)
            {
                return NotFound();
            }
            webStoreItem.Name = webStoreItemDTO.Name;
            webStoreItem.Description = webStoreItemDTO.Description;
            webStoreItem.Price = webStoreItemDTO.Price;
            webStoreItem.IsAvailable = webStoreItemDTO.IsAvailable;
            webStoreItem.IsDiscounted = webStoreItemDTO.IsDiscounted;
            webStoreItem.Picture = webStoreItemDTO.Picture;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WebStoreItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/WebStoreItems/PostItem
        [HttpPost("PostItem")]
        public async Task<ActionResult<WebStoreItemDTO>> CreateWebStoreItem(WebStoreItemDTO webStoreItemDTO)
        {
            var webStoreItem = new WebStoreItem
            {
                Name = webStoreItemDTO.Name,
                Description = webStoreItemDTO.Description,
                Price = webStoreItemDTO.Price,
                IsAvailable = webStoreItemDTO.IsAvailable,
                IsDiscounted = webStoreItemDTO.IsDiscounted,
                Picture = webStoreItemDTO.Picture
            };
            
            _context.WebStoreItems.Add(webStoreItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetWebStoreItem), new { id = webStoreItem.Id }, ItemToDTO(webStoreItem));
        }

        // DELETE: api/WebStoreItems/DeleteItem5
        [HttpDelete("DeleteItem{id}")]
        public async Task<IActionResult> DeleteWebStoreItem(ulong id)
        {
            var webStoreItem = await _context.WebStoreItems.FindAsync(id);
            if (webStoreItem == null)
            {
                return NotFound();
            }

            _context.WebStoreItems.Remove(webStoreItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WebStoreItemExists(ulong id) 
            => _context.WebStoreItems.Any(e => e.Id == id);

        private static WebStoreItemDTO ItemToDTO(WebStoreItem webStoreItem) =>
            new WebStoreItemDTO
            {
                Id = webStoreItem.Id,
                Name = webStoreItem.Name,
                Description = webStoreItem.Description,
                Price = webStoreItem.Price,
                IsAvailable = webStoreItem.IsAvailable,
                IsDiscounted = webStoreItem.IsDiscounted,
                Picture = webStoreItem.Picture
            };
    }
}
