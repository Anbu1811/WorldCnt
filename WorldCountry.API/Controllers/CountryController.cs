using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorldCountry.API.Data;
using WorldCountry.API.Model;

namespace WorldCountry.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public CountryController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public ActionResult<Country> Create([FromBody]Country country)
        {
            _dbContext.AllCountries.Add(country);
            _dbContext.SaveChanges();

            return Ok();
        }

        

        [HttpPut("{id:int}")]
        public ActionResult<Country> Update(int id, [FromBody]Country country)
        {
            var check = _dbContext.AllCountries.AsQueryable().Where(x => x.Id == id);

            if (check == null)
            {
                return Conflict("Please Enter valid ID Number");
            }

            _dbContext.AllCountries.Update(country);
            _dbContext.SaveChanges();

            return Ok();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Country>> GetAll()
        {
            var listCountry = _dbContext.AllCountries.ToList();
            return Ok(listCountry);
        }


        [HttpGet("{id:int}")]
        public ActionResult<Country> GetById(int id)
        {
            var check = _dbContext.AllCountries.Find(id);

            return Ok(check);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<Country> Delete(int id)
        {
            var check = _dbContext.AllCountries.Find(id);

            if(check == null)
            {
                return Conflict("Please Enter Valid Id Number");
            }

            _dbContext.AllCountries.Remove(check);
            _dbContext.SaveChanges();

            return Ok();
        }

    }
}
