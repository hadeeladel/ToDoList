using ToDoListAPI.Models;

namespace ToDoListAPI.Repositories
{
    public class taskRepository : ItaskRepository
    {
        private readonly ListDB dB;
        public taskRepository(ListDB listDB)
        {
            this.dB = listDB;
        }

        public (task, bool) addTask(string userId, string taskName, string taskDescription, bool tFinished)
        {
            task newtask = new() { UserId = userId, TaskName = taskName, TaskDescription = taskDescription, Finished=tFinished };
            dB.Add(newtask);
            return (newtask, true);

        }

        public int commit()
        {
            return dB.SaveChanges();
        }

        public bool deleteTask(string userId, string taskname)
        {
            var Dtask = dB.Tasks.Where(q => q.TaskName.Equals(taskname));
            if (Dtask != null)
            {
                dB.RemoveRange(Dtask);
                return true;
            }
            return false;


        }
        public List<task> getTasksOfUser(string userid)
        {
            return dB.Tasks.Where(q => q.UserId.Equals(userid)).ToList();
        }

        public (task, bool) updateTask(string userId, string taskname, bool newState)
        {
            var t = dB.Tasks.FirstOrDefault(t => t.TaskName.StartsWith(taskname));
            if(t == null) { return (null, false); }
            t.Finished = newState;
            return (t, true);

        }
    }
}
