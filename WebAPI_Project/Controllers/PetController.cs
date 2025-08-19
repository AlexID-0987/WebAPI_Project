using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WebAPI_Project.Models;

namespace WebAPI_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly PetDbContext _petDbContext;

        public PetController(PetDbContext petDbContext)
        {
            _petDbContext = petDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pet>>> GetAll()
        {
            if (_petDbContext.Pets == null || !_petDbContext.Pets.Any())
            {
                throw new InvalidOperationException("Database is empty!");
            }

            var pets = await _petDbContext.Pets.ToListAsync();
            return Ok(pets);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Pet>> GettAll(int id)
        {
            Pet? pet = await _petDbContext.Pets.FirstOrDefaultAsync(x => x.Id == id);
            if (pet == null)
            {
                return NotFound();
            }
            return pet;

        }
        [HttpPost]
        public async Task<ActionResult<Pet>>Post(Pet Pet)
        {
            await _petDbContext.Pets.AddAsync(Pet);
            await _petDbContext.SaveChangesAsync();
            if(Pet.Id == 0)
            {
                return BadRequest("Pet could not be created.");
            }
            return Pet;
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Pet>> Put(int id, Pet Pet)
        {
            if (id != Pet.Id)
            {
                return BadRequest("Pet ID mismatch.");
            }
            _petDbContext.Entry(Pet).State = EntityState.Modified;
            try
            {
                await _petDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) 
            {
                if (!_petDbContext.Pets.Any(x => x.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Pet;
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Pet? pet = await _petDbContext.Pets.FindAsync(id);
            if (pet == null)
            {
                return NotFound(); 
            }
            _petDbContext.Pets.Remove(pet);
            await _petDbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
