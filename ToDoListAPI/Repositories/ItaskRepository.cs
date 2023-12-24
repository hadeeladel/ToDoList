using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using ToDoListAPI.Models;

namespace ToDoListAPI.Repositories
{
    public interface ItaskRepository
    {
        List<task> getTasksOfUser(string userid);
        task updateTask(string userId, String taskname,bool newState);
        (task,bool) addTask(string userId, string taskName, string taskDescription, bool tFinished);
         bool deleteTask(string userId, String taskname);
        int commit();

    }
}