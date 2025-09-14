using Application.Categories.Queries;
using Application.DataTransferObjects;
using Domain.CoreServices;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BaseAppController
    {
        [HttpGet]
        public async Task<ActionResult<OperationResult<List<BasicListDTO>>>> GetCities()
        {
            var cities = await Mediator.Send(new GetCategoriesList.Query());
            return cities;
        }
       
    }
}
