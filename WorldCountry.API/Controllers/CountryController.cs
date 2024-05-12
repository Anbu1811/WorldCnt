using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorldCountry.API.Data;
using WorldCountry.API.DTO;
using WorldCountry.API.Model;
using WorldCountry.API.Repository.IRepository;

namespace WorldCountry.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CountryController> _logger;




        public CountryController(ICountryRepository countryRepository, IMapper mapper, ILogger<CountryController> logger)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
            _logger = logger;
        }






        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<CreateCountryDTO>> Create([FromBody]CreateCountryDTO countryDTO)
        {
            var isNameExist =  _countryRepository.IsRecordExiste(x=>x.Name == countryDTO.Name);
            //var isnameexist = _dbContext.AllCountries.AsQueryable().Where(x=>x.Name.ToLower().Trim() == countryDTO.Name.ToLower().Trim()).Any();

            if(isNameExist)
            {
                return Conflict("This country name is already existed");
            }

            var country = _mapper.Map<Country>(countryDTO);
          

            //_dbContext.AllCountries.Add(country);
            //_dbContext.SaveChanges();
            await _countryRepository.Create(country);

            return CreatedAtAction("GetById", new { id = country.Id }, country);
        }

        

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public async Task<ActionResult<Country>> Update(int id, [FromBody]UpdateCountryDTO countryDTO)
        {
            //var check = _dbContext.AllCountries.AsQueryable().Where(x => x.Id == id).Any();
            //var check =  _countryRepository.GetById(countryDTO.Id);
            
            if (id != countryDTO.Id)
            {

                //_dbContext.AllCountries.Update(country);
                //_dbContext.SaveChanges();

                return BadRequest();
            }


            var country = _mapper.Map<Country>(countryDTO);

            await _countryRepository.Update(country);

            return NoContent();

            
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IEnumerable<ShowCountryDTO>>> GetAll()
        {
            //var listCountry = _dbContext.AllCountries.ToList();
            var listCountry = await _countryRepository.GetAll();

            if(listCountry == null)
            {
                return NoContent();
            }

            var country = _mapper.Map<List<ShowCountryDTO>>(listCountry);

            return Ok(country);
        }


        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<ShowCountryDTO>> GetById(int id)
        {
            //var check = _dbContext.AllCountries.Find(id);
            var check = await _countryRepository.Get(id);


            if(check == null)
            {
                _logger.LogError($"Error while try to get record id:{id}");
                return NoContent();
            }

            var country = _mapper.Map<ShowCountryDTO>(check);

            return Ok(country);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete(int id)
        {
            //var check = _dbContext.AllCountries.Find(id);

            var check = await  _countryRepository.Get(id);
           

            if(check == null)
            {
                return Conflict("Please Enter Valid Id Number");
            }

            //_dbContext.AllCountries.Remove(check);
            //_dbContext.SaveChanges();

            await _countryRepository.Delete(check);

            return NoContent();
        }

    }
}

