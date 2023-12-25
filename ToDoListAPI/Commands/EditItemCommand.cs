using MediatR;
using ToDoListAPI.Models;

namespace ToDoListAPI.Commands
{
    public record EditItemCommand(string userId, string taskname, bool newState) : IRequest<task>;

}
