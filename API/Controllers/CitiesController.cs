using Application.Cities.Queries;
using Application.DataTransferObjects;
using Application.Validators;
using Domain.CoreServices;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : BaseAppController
    {
        [HttpGet]
        public async Task<ActionResult<OperationResult<List<BasicListDTO>>>> GetCities()
        {
            var cities = await Mediator.Send(new GetCitiesList.Query());
            return cities;
        }
       
    }
}
