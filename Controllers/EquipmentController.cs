
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LabEquipmentApi.Data;
using LabEquipmentApi.Domain;

namespace LabEquipmentApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EquipmentController : ControllerBase
{
    private readonly LabDbContext _context;

    public EquipmentController(LabDbContext context)
    {
        _context = context;
    }

    // GET: api/equipment
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Equipment>>> GetEquipmentItems()
    {
        return await _context.EquipmentItems.ToListAsync();
    }

    // GET: api/equipment/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Equipment>> GetEquipment(int id)
    {
        var equipment = await _context.EquipmentItems.FindAsync(id);

        if (equipment == null)
        {
            return NotFound();
        }

        return equipment;
    }

    // POST: api/equipment
    [HttpPost]
    public async Task<ActionResult<Equipment>> PostEquipment(Equipment equipment)
    {
        _context.EquipmentItems.Add(equipment);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetEquipment),
            new { id = equipment.Id },
            equipment);
    }

    // PUT: api/equipment/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutEquipment(int id, Equipment equipment)
    {
        if (id != equipment.Id)
        {
            return BadRequest();
        }

        _context.Entry(equipment).State = EntityState.Modified;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/equipment/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEquipment(int id)
    {
        var equipment = await _context.EquipmentItems.FindAsync(id);

        if (equipment == null)
        {
            return NotFound();
        }

        _context.EquipmentItems.Remove(equipment);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
