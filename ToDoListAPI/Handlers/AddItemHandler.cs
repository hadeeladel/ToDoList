using MediatR;
using ToDoListAPI.Commands;
using ToDoListAPI.Models;
using ToDoListAPI.Repositories;

namespace ToDoListAPI.Handlers
{
    public class AddItemHandler : IRequestHandler<AddItemCommand, task>
    {
        private readonly ItaskRepository _taskRepository;

        public AddItemHandler(ItaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public Task<task> Handle(AddItemCommand request, CancellationToken cancellationToken)
        {
            var newtask = _taskRepository.addTask(request.userId, request.taskName, request.taskDescription, request.tFinished);
            int done = _taskRepository.commit();
            if (done == 1 && newtask != null) { return Task.FromResult(newtask); }
            return null;
            
        }
    }
}
