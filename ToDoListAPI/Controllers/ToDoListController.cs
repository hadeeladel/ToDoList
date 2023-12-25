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

namespace ToDoListAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ToDoList : Controller
{
    private readonly ItaskRepository _repository;
    private readonly UserManager<IdentityUser> _userManager;

    public ToDoList(ItaskRepository repository,UserManager<IdentityUser> userManager)
    {
        this._repository =repository;
        this._userManager =userManager;
    }

    [HttpGet]

    //[Route("api/[controller]")]
    public IActionResult Get()
    {
        //decode the token in the request to find the user name
        //get the id of the username
        //return the task that has the userID the same as the id in the toke
        //so that the user can acces just the list he created 
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var result =_repository.getTasksOfUser(userId);
        return Ok(result);
    }

    [HttpPost("update")]
    public IActionResult Update(string taskname,bool newState) {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var result=_repository.updateTask(userId,taskname,newState);
            if (result == null)
            {
                return NotFound();
                //return StatusCode(StatusCodes.Status404NotFound);
            }
            int saved=_repository.commit();
            if(saved == 1) { return Accepted(result); }
            else { return StatusCode(StatusCodes.Status304NotModified); }
    }


    [HttpPost]
    [Route("Add")]
    public IActionResult AddTask(string taskName,string taskDescription ,bool tFinished)
    {
        //decode the token in the request to find the user name
        //get the id of the username
        //add a new task with the userId feild being the user that send the request
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var newtask=_repository.addTask(userId, taskName,taskDescription,tFinished);
            int done =_repository.commit();
            if(done == 1 && newtask.Item2) { return Ok(newtask.Item1); }
            else { return StatusCode(StatusCodes.Status500InternalServerError); }
        

    }

    [HttpDelete]
    [Route("Delete/Name")]
    public IActionResult Delete(string taskName)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        bool resutl= _repository.deleteTask(userId, taskName);
            if (!resutl)
            { return StatusCode(StatusCodes.Status404NotFound); }
              int done =_repository.commit();
            if (done == 1) { return StatusCode(StatusCodes.Status200OK); }
            else { return StatusCode(StatusCodes.Status500InternalServerError); }
       
                     
    }



}
