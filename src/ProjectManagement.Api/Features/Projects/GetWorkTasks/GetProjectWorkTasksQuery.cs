using MediatR;
using ProjectManagement.Api.Features.Shared;

namespace ProjectManagement.Api.Features.Projects.GetWorkTasks;

public sealed record GetProjectWorkTasksQuery(Guid ProjectId)
    : IRequest<List<WorkTaskResponse>>;