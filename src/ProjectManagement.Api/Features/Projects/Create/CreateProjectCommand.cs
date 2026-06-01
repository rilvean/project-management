using MediatR;
using ProjectManagement.Domain.Enums;

namespace ProjectManagement.Api.Features.Projects.Create;

public record CreateProjectCommand(
    string Title,
    string? Description,
    ProjectPriority Priority
) : IRequest<CreateProjectResponse>;