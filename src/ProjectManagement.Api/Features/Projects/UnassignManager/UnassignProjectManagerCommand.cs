using MediatR;

namespace ProjectManagement.Api.Features.Projects.UnassignManager;

public sealed record UnassignProjectManagerCommand(Guid ProjectId)
    : IRequest;