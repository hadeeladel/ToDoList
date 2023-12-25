using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoListAPI.Models;

namespace ToDoListAPI.Commands
{
    public record AddItemCommand(string userId, string taskName, string taskDescription, bool tFinished) :IRequest<task>;
    
}
