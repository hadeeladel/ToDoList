using MediatR;
using ToDoListAPI.Commands;
using ToDoListAPI.Models;
using ToDoListAPI.Repositories;

namespace ToDoListAPI.Handlers
{
    public class DeleteItemHandler : IRequestHandler<DeleteItemCommand, bool>
    {
        private readonly ItaskRepository _repository;

        public DeleteItemHandler(ItaskRepository repository)
        {
            _repository = repository;
        }

        public Task<bool> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
        {
            bool itemdelelted = _repository.deleteTask(request.userId, request.taskname);
            int save=_repository.commit();
            if (itemdelelted && save==1) {
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
            
        }
    }
}
