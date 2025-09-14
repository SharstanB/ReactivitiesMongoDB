using Domain.Absractions;
using Domain.Entities;
using Domain.Mediator;

namespace Application.Activities.Command
{
    public class DeleteActivity
    {
        public class Command : IRequest<string>
        {
            public Guid Id { get; set; }
        }

        public class Handler(IRepositoty<Activity> activityRepositoty) : IRequestHandler<Command, string>
        {
            public async Task<string> Handle(Command command, CancellationToken cancellationToken = default)
            {
                await activityRepositoty.Delete(command.Id, cancellationToken);
                return command.Id.ToString();
            }
        }
    }
}
