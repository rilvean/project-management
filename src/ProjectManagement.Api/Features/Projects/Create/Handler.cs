using MediatR;
using ProjectManagement.Domain.Models;
using ProjectManagement.Infrastructure.Persistence;

namespace ProjectManagement.Api.Features.Projects.Create;

public sealed class Handler(WriteDbContext db)
    : IRequestHandler<CreateProjectCommand, CreateProjectResponse>
{
    public async Task<CreateProjectResponse> Handle(CreateProjectCommand request, CancellationToken ct)
    {
        var project = new Project(
            request.Title,
            request.Description,
            request.Priority
        );

        await db.Projects.AddAsync(project, ct);
        await db.SaveChangesAsync(ct);
        return new CreateProjectResponse(project.Id);
    }
}