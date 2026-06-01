using MediatR;
using ProjectManagement.Api.Features.Shared;

namespace ProjectManagement.Api.Features.Projects.GetWorkTasks;

public record GetProjectWorkTasksQuery(Guid ProjectId)
    : IRequest<List<WorkTaskResponse>>;