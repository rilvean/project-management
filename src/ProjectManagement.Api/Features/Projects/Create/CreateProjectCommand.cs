using MediatR;
using ProjectManagement.Domain.Enums;

namespace ProjectManagement.Api.Features.Projects.Create;

public sealed record CreateProjectCommand(
    string Title,
    string? Description,
    ProjectPriority Priority
) : IRequest<CreateProjectResponse>;