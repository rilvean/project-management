using MediatR;
using ProjectManagement.Api.Features.Shared;

namespace ProjectManagement.Api.Features.WorkTasks.GetMy;

public sealed record GetMyWorkTasksQuery(Guid EmployeeId)
    : IRequest<List<WorkTaskResponse>>;