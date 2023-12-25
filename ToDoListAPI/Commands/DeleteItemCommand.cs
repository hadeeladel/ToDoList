using MediatR;

namespace ToDoListAPI.Commands
{
    public record DeleteItemCommand(string userId, string taskname) : IRequest<bool>;
}
