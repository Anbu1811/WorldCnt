using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorldCountry.API.DTO.StateDTO;
using WorldCountry.API.Model;
using WorldCountry.API.Repository.IRepository;

namespace WorldCountry.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IStateRepository _StateRepository;

        public StateController(IMapper mapper, IStateRepository StateRepository)
        {
            _mapper = mapper;
            _StateRepository = StateRepository;
        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<CreateStateDTO>> Creat([FromBody]CreateStateDTO state)
        {
            var result = _StateRepository.IsRecordExiste(x=>x.Name == state.Name);

            if(result == true)
            {
                return Conflict("This country name is already existed");
            }

            var states = _mapper.Map<States>(state);

            await _StateRepository.Create(states);

            return CreatedAtAction("Get", new { id = states.Id }, state);


        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<States>>> GetAll()
        {
            var statesList = await _StateRepository.GetAll();
            
            var states = _mapper.Map<List<ShowStateDTO>>(statesList);

            return Ok(states);
        }




        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ShowStateDTO>> Get(int id)
        {
            var state = await _StateRepository.Get(id);

            var map = _mapper.Map<ShowStateDTO>(state);

            return Ok(map);
        }





        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<ShowStateDTO>> Update(int id, [FromBody] ShowStateDTO stateDTO) 
        {
            if(id != stateDTO.Id) 
            {
                return BadRequest();
            }

            var state = _mapper.Map<States>(stateDTO);

            await _StateRepository.Update(state);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<States>> Delete(int id)
        {
            var delete = await _StateRepository.Get(id);
            if(delete == null)
            {
                return Conflict("This record is found in DATABASE");
            }

             await _StateRepository.Delete(delete);
            return NoContent();
        }

    }
}
