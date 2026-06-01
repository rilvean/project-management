using MediatR;

namespace ProjectManagement.Api.Features.Projects.Delete;

public record DeleteProjectCommand(Guid ProjectId)
    : IRequest;