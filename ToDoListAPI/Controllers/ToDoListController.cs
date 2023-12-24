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

namespace ToDoListAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ToDoList : Controller
{
    private readonly ItaskRepository _repository;

    public ToDoList(ItaskRepository repository)
    {
        this._repository =repository;
    }

    [HttpGet]

    //[Route("api/[controller]")]
    public List<task> Get()
    {
        //decode the token in the request to find the user name
        //get the id of the username
        //return the task that has the userID the same as the id in the toke
        //so that the user can acces just the list he created 
        var result =_repository.getTasksOfUser("");
        return result;
    }

    [HttpPost("update")]
    public IActionResult Update(string taskname,bool newState) { 
      
          var result=_repository.updateTask("",taskname,newState);
            if (result.Item1 == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            int saved=_repository.commit();
            if(saved == 1) { return StatusCode(StatusCodes.Status202Accepted); }
            else { return StatusCode(StatusCodes.Status304NotModified); }
    }


    [HttpPost]
    [Route("Add")]
    public IActionResult AddTask(string taskName,string taskDescription ,bool tFinished)
    {
        //decode the token in the request to find the user name
        //get the id of the username
        //add a new task with the userId feild being the user that send the request

            var newtask=_repository.addTask("",taskName,taskDescription,tFinished);
            int done =_repository.commit();
            if(done == 1) { return StatusCode(StatusCodes.Status201Created); }
            else { return StatusCode(StatusCodes.Status500InternalServerError); }
        

    }

    [HttpDelete]
    [Route("Delete/Name")]
    public IActionResult Delete(string taskName)
    {

       bool resutl= _repository.deleteTask("", taskName);
            if (!resutl)
            { return StatusCode(StatusCodes.Status404NotFound); }
              int done =_repository.commit();
            if (done == 1) { return StatusCode(StatusCodes.Status200OK); }
            else { return StatusCode(StatusCodes.Status500InternalServerError); }
       
                     
    }



}
