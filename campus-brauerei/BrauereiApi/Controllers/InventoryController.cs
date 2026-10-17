
using BrauereiApi.Data;
using BrauereiApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BreweryApi.Controllers
{
    [ApiController]
    [Route("api/inventory")]
    public class InventoryController : ControllerBase
    {
        private readonly InventoryDbContext _context;

        public InventoryController(InventoryDbContext context)
        {
            _context = context;
        }

        // GET: api/inventory
        // Alle Lagerprodukte abrufen
        [HttpGet]
        public async Task<ActionResult<List<BeerArticle>>> GetAll()
        {
            var items = await _context.BeerArticles.ToListAsync();

            return Ok(items);
        }

        // GET: api/inventory/1
        // Ein Lagerprodukt anhand der ID abrufen
        [HttpGet("{id:int}")]
        public async Task<ActionResult<BeerArticle>> GetById(int id)
        {
            var item = await _context.BeerArticles.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        // POST: api/inventory
        // Neues Lagerprodukt hinzufügen
        [HttpPost]
        public async Task<ActionResult<BeerArticle>> Create([FromBody] BeerArticle item)
        {
            item.Id = 0;

            _context.BeerArticles.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById),new { id = item.Id },item);
        }

        // PUT: api/inventory/1
        // Vorhandenes Lagerprodukt bearbeiten
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] BeerArticle updatedItem)
        {
            if (updatedItem.Id != 0 && updatedItem.Id != id)
            {
                return BadRequest("Die IDs stimmen nicht überein.");
            }

            var item = await _context.BeerArticles.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            item.Name = updatedItem.Name;
            item.Lagerbestand = updatedItem.Lagerbestand;
            item.Mindestbestand = updatedItem.Mindestbestand;
            item.Flaschengroesse = updatedItem.Flaschengroesse;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/inventory/1
        // Lagerprodukt löschen
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.BeerArticles.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            _context.BeerArticles.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
