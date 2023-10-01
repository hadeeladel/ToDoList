using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore.Design;
using static System.Console;
using Microsoft.EntityFrameworkCore;

using ToDoListDB;




//using (ListDB a = new())
//{
//    bool deleted = await a.Database.EnsureDeletedAsync();
//    WriteLine("database deleted ");

//    bool created = await a.Database.EnsureCreatedAsync();
//    WriteLine($"Database created: {created}");
//    WriteLine("SQL script used to create database:");
//    WriteLine(a.Database.GenerateCreateScript());
//}

//if(AddProduct("work on code", "work", false))
//{
//    WriteLine("item added");
//}
//static bool AddProduct( String taskdesc, string taskna,bool state)
//{
//    using (ListDB db = new())
//    {
//        task p = new()
//        {
//            TaskDescription= taskdesc,
//            TaskName= taskna,
//            Finished=state
//        };
//        db.Tasks.Add(p);

//        int done = db.SaveChanges();
//        return (done == 1);
//    }
//}

using (ListDB a = new())
{
    bool deleted = await a.Database.EnsureDeletedAsync();
    WriteLine("database deleted ");


}


