using MediatR;
using Microsoft.AspNetCore.Mvc;
using ZAD_Management.Application.Features.Settings.Companies.Commands.CreateCompany;
using ZAD_Management.Application.Features.Settings.Companies.Commands.UpdateCompany;
using ZAD_Management.Application.Features.Settings.Companies.DTOs;
using ZAD_Management.Application.Features.Settings.Companies.Queries.GetAllCompanies;
using ZAD_Management.Application.Features.Settings.Companies.Queries.GetCompanyById;
using ZAD_Management.Application.Features.Settings.Companies.Commands.DeleteCompany;

namespace ZAD_Management.API.Controllers.Settings;

[ApiController]
[Route("api/settings/companies")]
public class CompaniesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CompaniesController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _mediator.Send(new GetAllCompaniesQuery());

        return Ok(result);
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCompanyDto dto)
    {
        var id = await _mediator.Send(new CreateCompanyCommand(dto));

        return CreatedAtAction(
            nameof(GetById),
            new { id },
            id
        );
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetCompanyByIdQuery(id));

        if (result == null)
            return NotFound();

        return Ok(result);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UpdateCompanyDto dto)
    {
        var result = await _mediator.Send(
            new UpdateCompanyCommand(id, dto)
        );

        if (!result)
            return NotFound();

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(
            new DeleteCompanyCommand(id));

        if (!result)
            return NotFound();

        return NoContent();
    }
}