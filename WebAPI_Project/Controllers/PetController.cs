using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            return await _petDbContext.Pets.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Pet>> GettAll(int id)
        {
            Pet?  pet= await _petDbContext.Pets.FirstOrDefaultAsync(x => x.Id == id);
            if(pet==null)
            {
                return NotFound();
            }
            return pet;

        }
    }
}
