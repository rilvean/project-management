using MediatR;
using ProjectManagement.Api.Features.Projects.Shared;

namespace ProjectManagement.Api.Features.Projects.GetById;

public record GetProjectByIdQuery(Guid ProjectId)
    : IRequest<ProjectResponse>;