using MediatR;
using ToDoListAPI.Commands;
using ToDoListAPI.Models;
using ToDoListAPI.Repositories;

namespace ToDoListAPI.Handlers
{
    public class EditItemHandler : IRequestHandler<EditItemCommand, task>
    {
        private readonly ItaskRepository _repository;
        public Task<task> Handle(EditItemCommand request, CancellationToken cancellationToken)
        {
            var result = _repository.updateTask(request.userId, request.taskname, request.newState);
            int saved = _repository.commit();
            return Task.FromResult(result);
        }
    }
}
