using System.Linq;
using Kanban.Board.Core.StatusAggregate;
using Kanban.Board.Core.StatusAggregate.Specifications;
using Kanban.Board.Core.UserAggregate;
using Kanban.Board.Core.UserAggregate.Specifications;
using Kanban.Board.Core.WorkItemAggregate;
using Kanban.Board.Core.WorkItemAggregate.Specifications;
using Kanban.Board.SharedKernel.Interfaces;
using Kanban.Board.Web.ApiModels;
using Microsoft.AspNetCore.Mvc;

namespace Kanban.Board.Web.Api;
public class WorkItemsController : BaseApiController
{
  private readonly IRepository<WorkItem> _workItemRepository;
  private readonly IRepository<Status> _statusRepository;
  private readonly IRepository<User> _userRepository;
  private const string inProgress = "inprogress";

  public WorkItemsController(IRepository<WorkItem> repository, IRepository<Status> statusRepository, IRepository<User> userRepository)
  {
    _workItemRepository = repository;
    _statusRepository = statusRepository;
    _userRepository = userRepository;
  }

  [HttpGet]
  public async Task<IActionResult> List()
  {
    var workItemDTOs = (await _workItemRepository.ListAsync())
        .Select(workItem => new WorkItemDTO
        (
            id: workItem.Id,
            title: workItem.Title,
            description: workItem.Description,
            status: _statusRepository.GetBySpecAsync(new StatusByIdSpec(workItem.StatusId)),
            user: _userRepository.GetBySpecAsync(new UserByIdSpec(workItem.UserId))
        )).ToList();

    return Ok(workItemDTOs);
  }

  [HttpGet("{id:int}")]
  public async Task<IActionResult> GetById(int id)
  {
    var workItemSpec = new WorkItemByIdSpec(id);
    var workItem = await _workItemRepository.GetBySpecAsync(workItemSpec);
    if (workItem == null)
    {
      return NotFound();
    }

    var result = new WorkItemDTO
    (
        id: workItem.Id,
        title: workItem.Title,
        description: workItem.Description,
        status: _statusRepository.GetBySpecAsync(new StatusByIdSpec(workItem.StatusId)),
        user: _userRepository.GetBySpecAsync(new UserByIdSpec(workItem.UserId))
    );

    return Ok(result);
  }

  [HttpGet(nameof(Status) + "/{statusId}")]
  public async Task<IActionResult> GetByStatus(int statusId)
  {
    var workItemDTOs = (await _workItemRepository.ListAsync()).Where( workItem => workItem.StatusId == statusId)
        .Select(workItem => new WorkItemDTO
        (
            id: workItem.Id,
            title: workItem.Title,
            description: workItem.Description,
            status: _statusRepository.GetBySpecAsync(new StatusByIdSpec(workItem.StatusId)),
            user: _userRepository.GetBySpecAsync(new UserByIdSpec(workItem.UserId))
        )).ToList();

    return Ok(workItemDTOs);
  }

  [HttpGet(nameof(Status))]
  public async Task<IActionResult> ListStatuses()
  {
    return Ok(await _statusRepository.ListAsync());
  }

  [HttpPost]
  public async Task<IActionResult> Post([FromBody] CreateWorkItemDTO request)
  {
    var inprogressStatus = await _statusRepository.GetBySpecAsync(new StatusByNameSpec(inProgress));

    if (request.StatusId == inprogressStatus.Id)
    {
      var workItemCount = (await _workItemRepository.ListAsync()).Where(workItem => workItem.StatusId == inprogressStatus.Id).Count() >= 5;
      if(workItemCount)
        return Conflict("Can not have more the 5 workItems in inprogress");
    }

    var newWorkItem = new WorkItem(request.Title, request.Description, request.UserId, request.StatusId);

    var createdWorkItem = await _workItemRepository.AddAsync(newWorkItem);

    var result = new WorkItemDTO
    (
        id: createdWorkItem.Id,
        title: createdWorkItem.Title,
        description: createdWorkItem.Description,
        status: _statusRepository.GetBySpecAsync(new StatusByIdSpec(createdWorkItem.StatusId)),
        user: _userRepository.GetBySpecAsync(new UserByIdSpec(createdWorkItem.UserId))
    );

    return Ok(result);
  }

  [HttpPut("{id:int}")]
  public async Task<IActionResult> Update(int id, CreateWorkItemDTO request)
  {
    var inprogressStatus = await _statusRepository.GetBySpecAsync(new StatusByNameSpec(inProgress));

    if (request.StatusId == inprogressStatus.Id)
    {
      var workItemCount = (await _workItemRepository.ListAsync()).Where(workItem => workItem.StatusId == inprogressStatus.Id).Count() >= 5;
      if (workItemCount)
        return Conflict("Can not have more the 5 workItems in inprogress");
    }

    var workItemSpec = new WorkItemByIdSpec(id);
    var workItem = await _workItemRepository.GetBySpecAsync(workItemSpec);
    if (workItem == null) return NotFound("No such WorkItem");
    workItem.Title = request.Title;
    workItem.Description = request.Description;
    workItem.UserId = request.UserId;
    workItem.StatusId = request.StatusId;
    await _workItemRepository.UpdateAsync(workItem);

    return Ok(request);
  }
}
