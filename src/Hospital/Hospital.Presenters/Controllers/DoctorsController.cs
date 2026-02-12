using Hospital.UseCases.Doctors.Commands;
using Hospital.UseCases.Doctors.DTO;
using Hospital.UseCases.Doctors.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Hospital.Presenters.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DoctorsController : ControllerBase
{
    private readonly IUpdateDoctorCommandHandler _updateHandler;

    public DoctorsController(IUpdateDoctorCommandHandler updateHandler)
    {
        _updateHandler = updateHandler;
    }

    [HttpPut("{id:guid}")]
    public async Task<IResult> Update(
        [FromRoute] Guid id,
        [FromBody] UpdateDoctorRequest request,
        CancellationToken ct = default)
    {
        try
        {
            var command = new UpdateDoctorCommand(
                id,
                new UpdateDoctorDto(
                    request.FirstName,
                    request.MiddleName,
                    request.LastName,
                    request.Specialization));

            var result = await _updateHandler.HandleAsync(command, ct);
            return new Envelope(result);
        }
        catch (InvalidOperationException ex)
        {
            return new Envelope(HttpStatusCode.NotFound, ex.Message);
        }
        catch (Exception ex)
        {
            return new Envelope(HttpStatusCode.BadRequest, ex.Message);
        }
    }
}

public record UpdateDoctorRequest(
    string? FirstName,
    string? MiddleName,
    string? LastName,
    string? Specialization);