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

namespace ToDoListAPI.Controllers;

[Route("api/[controller]")]
[ApiController]

public class ToDoList : Controller
{
    private readonly ListDB db;
    public ToDoList(ListDB _db)
    {
        this.db = _db;
    }

    [HttpGet]

    //[Route("api/[controller]")]
    public List<task> Get()
    {
            IQueryable<task>? tasks =db.Tasks;
            List<task> result =new List<task>();
            foreach(task t in tasks)
            {
                result.Add(t);
            }
        return result;
    }

    [Authorize]
    [HttpPost("update")]
    public IActionResult Update(string taskname,bool newState) { 
      
           task? updatetask=db.Tasks.FirstOrDefault(t=>t.TaskName.StartsWith(taskname));
            if (updatetask == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            updatetask.Finished = newState;
            int result=db.SaveChanges();
            if(result==1) { return StatusCode(StatusCodes.Status202Accepted); }
            else { return StatusCode(StatusCodes.Status304NotModified); }
    }


    [HttpPost]
    [Route("Add")]
    public IActionResult AddTask(string taskName,string taskDescription ,bool tFinished)
    {

            task newtask = new() { TaskName =taskName, TaskDescription = taskDescription, Finished = tFinished };
            db.Tasks.Add(newtask);
            int done = db.SaveChanges();
            if(done == 1) { return StatusCode(StatusCodes.Status201Created); }
            else { return StatusCode(StatusCodes.Status500InternalServerError); }
        

    }

    [HttpDelete]
    [Route("Delete/Name")]
    public IActionResult Delete(string taskName)
    {
 
            IQueryable<task>? de=db.Tasks.Where(c=>c.TaskName.StartsWith(taskName));
            if (de is null)
            { return StatusCode(StatusCodes.Status404NotFound); }
            else
            {
                db.RemoveRange(de);
                int result = db.SaveChanges();
                return StatusCode(StatusCodes.Status200OK);
            }
            
        
    }



}
