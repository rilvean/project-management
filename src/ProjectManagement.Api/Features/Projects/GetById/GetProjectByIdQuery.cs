using MediatR;
using ProjectManagement.Api.Features.Projects.Shared;

namespace ProjectManagement.Api.Features.Projects.GetById;

public sealed record GetProjectByIdQuery(Guid ProjectId)
    : IRequest<ProjectResponse>;