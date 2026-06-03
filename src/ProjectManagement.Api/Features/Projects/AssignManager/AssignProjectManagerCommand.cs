using MediatR;

namespace ProjectManagement.Api.Features.Projects.AssignManager;

public record AssignProjectManagerCommand(
    Guid ProjectId,
    Guid ManagerId
) : IRequest;