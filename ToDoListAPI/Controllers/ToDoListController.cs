using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using ToDoListAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ToDoListAPI.Repositories;
using System.Security.Policy;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using MediatR;
using ToDoListAPI.Commands;
using ToDoListAPI.Queries;

namespace ToDoListAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ToDoList : Controller
{
    private readonly IMediator _mediator;

    public ToDoList(IMediator mediator)
    {
        this._mediator = mediator;
    }

    [HttpGet]

    //[Route("api/[controller]")]
    public async Task<IActionResult> Get()
    {
        string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var items = await _mediator.Send(new GetItemByUserIdQuery(userId));
        return Ok(items);
    }

    [HttpPost("update")]
    public IActionResult Update(string taskname,bool newState) {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var result = _mediator.Send(new EditItemCommand(userId,taskname,newState));
            if (result == null)
            {
                return NotFound();
            }
        return Accepted(result);
    }


    [HttpPost]
    [Route("Add")]
    public IActionResult AddTask(string taskName,string taskDescription ,bool tFinished)
    {
            //get userID from token
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = _mediator.Send(new AddItemCommand(userId, taskName, taskDescription, tFinished));
            if(result.Result != null) { return Ok(result.Result); }
            else { return StatusCode(StatusCodes.Status500InternalServerError); }
        

    }

    [HttpDelete]
    [Route("Delete/Name")]
    public IActionResult Delete(string taskName)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        Task<bool> result=_mediator.Send(new DeleteItemCommand(userId, taskName));
            if (!result.Result)
            { return StatusCode(StatusCodes.Status404NotFound); }
            return StatusCode(StatusCodes.Status200OK); 
                     
    }



}
