using ToDoListAPI.Models;

namespace ToDoListAPI.Repositories
{
    public class taskRepository : ItaskRepository
    {
        private readonly DBcontext dB;
        public taskRepository(DBcontext listDB)
        {
            this.dB = listDB;
        }

        public task addTask(string userId, string taskName, string taskDescription, bool tFinished)
        {
            task newtask = new() { UserId = userId, TaskName = taskName, TaskDescription = taskDescription, Finished=tFinished };
            try
            {
                dB.Add(newtask);
            }
            catch(Exception ex) {
                return null;
            }
            return newtask;


        }

        public int commit()
        {
            return dB.SaveChanges();
        }

        public bool deleteTask(string userId, string taskname)
        {
            var Dtask = dB.Tasks.Where(q => q.TaskName.Equals(taskname) && q.UserId.Equals(userId));
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

        public task updateTask(string userId, string taskname, bool newState)
        {
            var t = dB.Tasks.FirstOrDefault(t => t.TaskName.StartsWith(taskname) && t.UserId.Equals(userId));
            if(t == null) { return null; }
            t.Finished= newState;
            return t;

        }
    }
}
