using MediatR;
using ProjectManagement.Api.Features.Shared;

namespace ProjectManagement.Api.Features.WorkTasks.GetById;

public record GetWorkTaskByIdQuery(Guid WorkTaskId)
    : IRequest<WorkTaskResponse>;