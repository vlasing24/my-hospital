using Hospital.UseCases.Patients.Commands;
using Hospital.UseCases.Patients.Handlers;
using Hospital.UseCases.Patients.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Hospital.Presenters.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController : ControllerBase
{
    private readonly ICreatePatientCommandHandler _createHandler;
    private readonly IGetPatientQueryHandler _getHandler;
    private readonly IUpdatePatientCommandHandler _updateHandler;
    private readonly IDeletePatientCommandHandler _deleteHandler;

    public PatientsController(
        ICreatePatientCommandHandler createHandler,
        IGetPatientQueryHandler getHandler,
        IUpdatePatientCommandHandler updateHandler,
        IDeletePatientCommandHandler deleteHandler)
    {
        _createHandler = createHandler;
        _getHandler = getHandler;
        _updateHandler = updateHandler;
        _deleteHandler = deleteHandler;
    }

    [HttpPost]
    public async Task<IResult> Create([FromBody] CreatePatientCommand command, CancellationToken ct = default)
    {
        try
        {
            var result = await _createHandler.HandleAsync(command, ct);
            return new Envelope(result);
        }
        catch (Exception ex)
        {
            return new Envelope(HttpStatusCode.BadRequest, ex.Message);
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<IResult> Get([FromRoute] Guid id, CancellationToken ct = default)
    {
        try
        {
            var query = new GetPatientQuery(id);
            var result = await _getHandler.HandleAsync(query, ct);
            return new Envelope(result);
        }
        catch (InvalidOperationException ex)
        {
            return new Envelope(HttpStatusCode.NotFound, ex.Message);
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<IResult> Update(
        [FromRoute] Guid id,
        [FromBody] UpdatePatientCommand command,
        CancellationToken ct = default)
    {
        var updatedCommand = command with { Id = id };
        try
        {
            var result = await _updateHandler.HandleAsync(updatedCommand, ct);
            return new Envelope(result);
        }
        catch (InvalidOperationException ex)
        {
            return new Envelope(HttpStatusCode.NotFound, ex.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IResult> Delete([FromRoute] Guid id, CancellationToken ct = default)
    {
        try
        {
            var command = new DeletePatientCommand(id);
            await _deleteHandler.HandleAsync(command, ct);
            return new Envelope(HttpStatusCode.OK, "Пациент успешно удален");
        }
        catch (InvalidOperationException ex)
        {
            return new Envelope(HttpStatusCode.NotFound, ex.Message);
        }
    }
}

public class Envelope : IResult
{
    public int Status { get; }
    public object? Data { get; }
    public string? Error { get; }

    public Envelope(HttpStatusCode statusCode, object? data = null, string? error = null)
    {
        Status = (int)statusCode;
        Data = data;
        Error = error;
    }

    public Envelope(object data)
    {
        Status = 200;
        Error = null;
        Data = data;
    }

    public Envelope(HttpStatusCode statusCode, string error)
    {
        Status = (int)statusCode;
        Error = error;
        Data = null;
    }

    public Task ExecuteAsync(HttpContext httpContext)
    {
        httpContext.Response.StatusCode = Status;
        httpContext.Response.ContentType = "application/json";
        return httpContext.Response.WriteAsJsonAsync(this);
    }
}