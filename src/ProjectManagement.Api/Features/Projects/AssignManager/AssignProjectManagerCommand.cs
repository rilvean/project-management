using MediatR;

namespace ProjectManagement.Api.Features.Projects.AssignManager;

public sealed record AssignProjectManagerCommand(
    Guid ProjectId,
    Guid ManagerId
) : IRequest;