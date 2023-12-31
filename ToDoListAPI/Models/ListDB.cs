﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace ToDoListAPI.Models;

public class ListDB :DbContext
{

    public ListDB(DbContextOptions<ListDB> options) : base(options)
    {
        
    }
    public DbSet<task> Tasks { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connection = "Data Source=.;Initial Catalog=DBList;Integrated Security=true;" +
            "MultipleActiveResultSets=true;TrustServerCertificate=True;Encrypt=True;";
        optionsBuilder.UseSqlServer(connection);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<task>().Property(s=>s.TaskName).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<task>().Property(s=>s.TaskDescription).IsRequired().HasMaxLength(200);
        task read = new() { TaskId = 1 ,UserId= "6f6aba9d - 9696 - 46d1 - a0ac - 26aaecebcc17",TaskName = "read books",TaskDescription="read at lest 1 book this month",Finished=false};
        task clean = new() { TaskId = 2, UserId = "6f6aba9d - 9696 - 46d1 - a0ac - 26aaecebcc17", TaskName = "clean room", TaskDescription = "clean your bedroom",Finished=true };
        task organize = new() { TaskId = 3, UserId = "6f6aba9d - 9696 - 46d1 - a0ac - 26aaecebcc17", TaskName = "organize", TaskDescription = "organize your laptop desktop",Finished=false };
        modelBuilder.Entity<task>().HasData(read,clean,organize);
    }
}
