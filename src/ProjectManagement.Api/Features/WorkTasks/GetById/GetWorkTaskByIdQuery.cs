using MediatR;
using ProjectManagement.Api.Features.Shared;

namespace ProjectManagement.Api.Features.WorkTasks.GetById;

public sealed record GetWorkTaskByIdQuery(Guid WorkTaskId)
    : IRequest<WorkTaskResponse>;