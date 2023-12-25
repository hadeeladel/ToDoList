using MediatR;
using ToDoListAPI.Models;

namespace ToDoListAPI.Queries
{
    public record GetItemByUserIdQuery(string userid):IRequest<List<task>>;
    
}
