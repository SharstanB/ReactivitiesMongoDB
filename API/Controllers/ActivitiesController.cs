using Application.Activities.Command;
using Application.Activities.Queries;
using Application.DataTransferObjects.Activity;
using Domain.CoreServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ActivitiesController : BaseAppController
{
    [HttpGet]
    public async Task<ActionResult<OperationResult<List<GetActivitiesDTO>>>> GetActivities()
    {
        return await Mediator.Send(new GetActivitiesList.Query());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OperationResult<GetActivitiesDTO>>> GetActivities(Guid id)
    {
        var activity = await Mediator.Send(new   GetActivityDetails.Query { Id = id });
        if (activity is null)
            return NotFound();
        return activity;
    }

    [HttpPost]
    public async Task<ActionResult<OperationResult<Guid>>> AddActivity(CreateActivityDTO activity)
    {
        var newActivityResult = await Mediator.Send(new CreateActivity.Command() { Activity = activity });
        
        return newActivityResult;
    }

    [HttpPut]
    public async Task<ActionResult<OperationResult<Guid>>> EditActivity(EditActivityDTO activity)
    {
        var editActivityResult = await Mediator.Send(new EditActivity.Command() { Activity = activity });
        return editActivityResult;
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<string>> DeleteActivity(string id)
    {
        var result = await Mediator.Send(new DeleteActivity.Command() { Id = new Guid(id) });
        return result;
    }

}
