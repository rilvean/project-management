using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Api.Features.Users.Delete;
using ProjectManagement.Infrastructure.Persistence;

namespace ProjectManagement.Api.Features.Projects.Delete;

public class Handler(WriteDbContext db)
    : IRequestHandler<DeleteProjectCommand>
{
    public async Task Handle(DeleteProjectCommand request, CancellationToken ct)
    {
        await db.Projects
            .Where(x => x.Id == request.ProjectId)
            .ExecuteDeleteAsync(ct);
    }
}