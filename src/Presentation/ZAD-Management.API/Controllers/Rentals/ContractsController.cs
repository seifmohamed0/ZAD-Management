using MediatR;
using Microsoft.AspNetCore.Mvc;
using ZAD_Management.Application.Features.Rentals.Contracts.Commands.CreateRentalContract;
using ZAD_Management.Application.Features.Rentals.Contracts.DTOs;
using ZAD_Management.Application.Features.Rentals.Contracts.Queries.GetAllRentalContracts;
using ZAD_Management.Application.Features.Rentals.Contracts.Queries.GetRentalContractById;

namespace ZAD_Management.API.Controllers.Rentals;

[ApiController]
[Route("api/rentals/contracts")]
public class ContractsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ContractsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int? branchId)
    {
        var result = await _mediator.Send(new GetAllRentalContractsQuery(branchId));
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetRentalContractByIdQuery(id));
        if (result == null)
            return NotFound(new { message = $"Rental contract with ID {id} was not found." });

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRentalContractDto dto)
    {
        var id = await _mediator.Send(new CreateRentalContractCommand(dto));
        return CreatedAtAction(nameof(GetById), new { id }, new { id, message = "Rental contract created successfully." });
    }
}

