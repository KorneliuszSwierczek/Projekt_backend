using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjektAPI1.Data;
using ProjektAPI1.Enitites;
using Swashbuckle.AspNetCore.Annotations;

namespace ProjektAPI1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("actors")]
    public class ActorController : ControllerBase
    {
        private readonly DataContext _context;

        public ActorController(DataContext context)
        {
            _context = context;
        }

        [HttpGet ("getallactors")]
        public async Task<ActionResult<List<Actor>>> GetAllActors()
        {
            var actors = await _context.actors.ToListAsync();

            return Ok(actors);

        }
        [Authorize]
        [HttpGet("{Id}")]
        public async Task<ActionResult<Actor>> GetActor(int Id)
        {
            var actors = await _context.actors.FindAsync(Id);
            if (actors == null)
                return NotFound("Actor not found");


            return Ok(actors);

        }

        [Authorize]
        [HttpPost ("AddActors")]
        public async Task<ActionResult<List<Actor>>> AddActor(Actor actor)
        {
            _context.actors.Add(actor);
            await _context.SaveChangesAsync();

            return Ok(await _context.actors.ToListAsync());

        }

        [Authorize]
        [HttpPut ("UpdateActors")]
        public async Task<ActionResult<List<Actor>>> UpdateActor(Actor updatedActor)
        {
            var dbactors = await _context.actors.FindAsync(updatedActor.Id);
            if (dbactors is null)
                return NotFound("Actor not found");

            dbactors.FirstName = updatedActor.FirstName;
            dbactors.LastName = updatedActor.LastName;

            await _context.SaveChangesAsync();

            return Ok(await _context.actors.ToListAsync());

        }
        [Authorize]
        [HttpDelete ("DeleteActors")]
        public async Task<ActionResult<List<Actor>>> DeleteActor(int Id)
        {
            var dbactors = await _context.actors.FindAsync(Id);
            if (dbactors is null)
                return NotFound("Actor not found");

            _context.actors.Remove(dbactors);
            await _context.SaveChangesAsync();

            return Ok(await _context.actors.ToListAsync());


        }
    }
}
