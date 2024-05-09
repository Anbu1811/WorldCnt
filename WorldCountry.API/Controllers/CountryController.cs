using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorldCountry.API.Data;
using WorldCountry.API.DTO;
using WorldCountry.API.Model;

namespace WorldCountry.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;





        public CountryController(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }






        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult<CreateCountryDTO> Create([FromBody]CreateCountryDTO countryDTO)
        {
            var isnameexist = _dbContext.AllCountries.AsQueryable().Where(x=>x.Name.ToLower().Trim() == countryDTO.Name.ToLower().Trim()).Any();

            if(isnameexist)
            {
                return Conflict("This country name is already existed");
            }

            var country = _mapper.Map<Country>(countryDTO);
          

            _dbContext.AllCountries.Add(country);
            _dbContext.SaveChanges();

            return CreatedAtAction("GetById", new { id = country.Id }, country);
        }

        

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public ActionResult<UpdateCountryDTO> Update(int id, [FromBody]UpdateCountryDTO countryDTO)
        {
            var check = _dbContext.AllCountries.AsQueryable().Where(x => x.Id == id).Any();

            if (check)
            {
                var country = _mapper.Map<Country>(countryDTO);

                _dbContext.AllCountries.Update(country);
                _dbContext.SaveChanges();

                return NoContent();


                
            }

            return Conflict("Please Enter valid ID Number");
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<IEnumerable<ShowCountryDTO>> GetAll()
        {
            var listCountry = _dbContext.AllCountries.ToList();
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
        public ActionResult<ShowCountryDTO> GetById(int id)
        {
            var check = _dbContext.AllCountries.Find(id);
            if(check == null)
            {
                return NoContent();
            }

            var country = _mapper.Map<ShowCountryDTO>(check);

            return Ok(country);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<Country> Delete(int id)
        {
            var check = _dbContext.AllCountries.Find(id);
           

            if(check == null)
            {
                return Conflict("Please Enter Valid Id Number");
            }

            _dbContext.AllCountries.Remove(check);
            _dbContext.SaveChanges();

            return NoContent();
        }

    }
}
