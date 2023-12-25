using MediatR;
using ToDoListAPI.Models;
using ToDoListAPI.Queries;
using ToDoListAPI.Repositories;

namespace ToDoListAPI.Handlers
{
    public class GetItemByUserIdHandler : IRequestHandler<GetItemByUserIdQuery, List<task>>
    {
        private readonly ItaskRepository _repository;

        public GetItemByUserIdHandler(ItaskRepository repository)
        {
            _repository = repository;
        }

        public Task<List<task>> Handle(GetItemByUserIdQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult( _repository.getTasksOfUser(request.userid));
        }
    }
}
