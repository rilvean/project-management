using MediatR;

namespace ProjectManagement.Api.Features.Projects.UnassignManager;

public record UnassignProjectManagerCommand(Guid ProjectId)
    : IRequest;