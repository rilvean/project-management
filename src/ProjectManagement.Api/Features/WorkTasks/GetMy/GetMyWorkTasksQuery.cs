using MediatR;
using ProjectManagement.Api.Features.Shared;
using ProjectManagement.Domain.Models;

namespace ProjectManagement.Api.Features.WorkTasks.GetMy;

public record GetMyWorkTasksQuery(Guid EmployeeId)
    : IRequest<List<WorkTaskResponse>>;