using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestRoomTemp.Models;
using RestRoomTemp.Repos;

namespace RestRoomTemp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomTempController : ControllerBase
    {

        public readonly RoomTempContext _context;
        public IRestRoomRepoDB _repo;

        public RoomTempController(RoomTempContext context, IRestRoomRepoDB repo)
        {
            _context = context;
            _repo = repo;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<RoomTemp>> GetAll()
        {
            var roomTemps = _repo.GetAll();
            if (roomTemps.Count == 0)
            {
                return NoContent();
            }
            return Ok(roomTemps);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<RoomTemp> GetById(int id)
        {
            var roomTemp = _repo.GetById(id);
            if (roomTemp == null)
            {
                return NotFound();
            }
            return Ok(roomTemp);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<RoomTemp> Add([FromBody] RoomTemp roomTemp)
        {
            try
            {
                var temp = _repo.Add(roomTemp);
                return Created($"/api/roomtemp/{temp.Id}", temp);
            }
            catch(ArgumentNullException e)
            {
                return NotFound(e.Message);
            }
            catch(InvalidOperationException e)
            {
                return Conflict(e.Message);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
       
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<RoomTemp> Delete(int id)
        {
            try
            {
                var roomTemp = _repo.Delete(id);
                return Ok(roomTemp);
            }
            catch(ArgumentNullException e)
            {
                return NotFound(e.Message);
            }
        }



    }
}
