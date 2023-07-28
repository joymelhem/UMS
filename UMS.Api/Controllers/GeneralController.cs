using App.Queries;
using DomainLibrary.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace UMS.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class GeneralController : ControllerBase
{    
    private readonly ITenantContext _tenantContext;
    private readonly IMediator _mediator;
    public GeneralController(IMediator mediator, ITenantContext tenantContext)
    {
        _mediator = mediator;
        _tenantContext = tenantContext;
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetByIdQuery(id));
        return Ok(result);
    }
    
    [HttpGet("Branch Id")]
    public IActionResult Get()
    {
        long branchId = _tenantContext.branchid;
        return Ok(branchId);
    }
    
    [HttpGet("All Courses")]
    [EnableQuery]
    public async Task<IActionResult> GetAllCourses()
    {
        var result = await _mediator.Send(new GetAllCoursesQuery());
        return Ok(result);
    }
    
}