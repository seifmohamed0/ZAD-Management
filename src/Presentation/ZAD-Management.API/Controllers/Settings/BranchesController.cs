using MediatR;
using Microsoft.AspNetCore.Mvc;
using ZAD_Management.Application.Features.Settings.Branches.Commands.CreateBranch;
using ZAD_Management.Application.Features.Settings.Branches.DTOs;
using ZAD_Management.Application.Features.Settings.Branches.Queries.GetAllBranches;
using ZAD_Management.Application.Features.Settings.Branches.Queries.GetBranchById;
using ZAD_Management.Application.Features.Settings.Branches.Commands.UpdateBranch;
using ZAD_Management.Application.Features.Settings.Branches.Commands.DeleteBranch;

namespace ZAD_Management.API.Controllers.Settings;

[ApiController]
[Route("api/settings/branches")]
public class BranchesController : ControllerBase
{
    private readonly IMediator _mediator;

    public BranchesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBranchDto dto)
    {
        var id = await _mediator.Send(new CreateBranchCommand(dto));

        return Ok(id);
    }
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _mediator.Send(
            new GetAllBranchesQuery());

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(
            new GetBranchByIdQuery(id));

        if (result == null)
            return NotFound();

        return Ok(result);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UpdateBranchDto dto)
    {
        var result = await _mediator.Send(
            new UpdateBranchCommand(id, dto));

        if (!result)
            return NotFound();

        return Ok();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(
            new DeleteBranchCommand(id));

        if (!result)
            return NotFound();

        return NoContent();
    }
}