using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebStoreApi.Models;
using WebStoreApi.Models.crm;

namespace WebStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly WebStoreContext _context;

        public ClientsController(WebStoreContext context)
        {
            _context = context;
        }

        // GET: api/Clients/GetAllClients
        [HttpGet("GetAllClients")]
        public async Task<ActionResult<IEnumerable<ClientDTO>>> GetClient()
        {
            return await _context.Client.Select(x => ItemToDTO(x)).ToListAsync();
        }

        // GET: api/Clients/GetClient5
        [HttpGet("GetClient{id}")]
        public async Task<ActionResult<ClientDTO>> GetClient(ulong id)
        {
            var client = await _context.Client.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            return ItemToDTO(client);
        }

        // PUT: api/Clients/UpdateClient5
        [HttpPut("UpdateClient{id}")]
        public async Task<IActionResult> UpdateClient(ulong id, ClientDTO clientDTO)
        {
            if (id != clientDTO.Id)
            {
                return BadRequest();
            }

            var client = await _context.Client.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            client.Firstname = clientDTO.Firstname;
            client.Lastname = clientDTO.Lastname;
            client.Address = clientDTO.Address;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!ClientExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Clients/PostClient
        [HttpPost("PostClient")]
        public async Task<ActionResult<ClientDTO>> CreateClient(ClientDTO clientDTO)
        {
            var client = new Client
            {
                Firstname = clientDTO.Firstname,
                Lastname = clientDTO.Lastname,
                Address = clientDTO.Address
            };

            _context.Client.Add(client);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetClient), new { id = client.Id }, ItemToDTO(client));
        }

        // DELETE: api/Clients/DeleteClient5
        [HttpDelete("DeleteClient{id}")]
        public async Task<IActionResult> DeleteClient(ulong id)
        {
            var client = await _context.Client.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            _context.Client.Remove(client);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClientExists(ulong id) => _context.Client.Any(e => e.Id == id);

        private static ClientDTO ItemToDTO(Client client) => 
            new ClientDTO
            {
                Id = client.Id,
                Firstname = client.Firstname,
                Lastname = client.Lastname,
                Address = client.Address
            };
    }
}
