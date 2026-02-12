using Hospital.Domain.Patient.ValueObjects;
using Hospital.UseCases.Appointments.Commands;
using Hospital.UseCases.Appointments.Exceptions;
using Hospital.UseCases.Appointments.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Hospital.Presenters.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly IBookAppointmentCommandHandler _bookHandler;
    private readonly IRescheduleAppointmentCommandHandler _rescheduleHandler;

    public AppointmentsController(
        IBookAppointmentCommandHandler bookHandler,
        IRescheduleAppointmentCommandHandler rescheduleHandler)
    {
        _bookHandler = bookHandler;
        _rescheduleHandler = rescheduleHandler;
    }

    [HttpPost("book")]
    public async Task<IResult> BookAppointment(
        [FromBody] BookAppointmentRequest request,
        CancellationToken ct = default)
    {
        try
        {
            var command = new BookAppointmentCommand(
                PatientID.From(request.PatientId),
                request.DoctorId,
                request.DateTime,
                request.Complaints);

            var appointmentId = await _bookHandler.HandleAsync(command, ct);

            return new Envelope(HttpStatusCode.Created, new { appointmentId = appointmentId.Value });
        }
        catch (DoctorSlotConflictException ex)
        {
            return new Envelope(
                HttpStatusCode.Conflict,
                new
                {
                    error = ex.Message,
                    availableDoctors = ex.AlternativeDoctors
                });
        }
        catch (InvalidOperationException ex)
        {
            return new Envelope(HttpStatusCode.BadRequest, ex.Message);
        }
    }

    [HttpPost("reschedule")]
    public async Task<IResult> RescheduleAppointment(
        [FromBody] RescheduleAppointmentRequest request,
        CancellationToken ct = default)
    {
        try
        {
            var command = new RescheduleAppointmentCommand(
                request.AppointmentId,
                request.DoctorId,
                request.NewDateTime);

            var appointmentId = await _rescheduleHandler.HandleAsync(command, ct);

            return new Envelope(HttpStatusCode.OK, new { appointmentId = appointmentId.Value });
        }
        catch (TimeSlotConflictException ex)
        {
            return new Envelope(
                HttpStatusCode.Conflict,
                new
                {
                    error = ex.Message,
                    availableSlots = ex.AlternativeSlots.Select(s => new
                    {
                        start = s.StartTime,
                        end = s.EndTime
                    })
                });
        }
        catch (InvalidOperationException ex)
        {
            return new Envelope(HttpStatusCode.BadRequest, ex.Message);
        }
    }
}

public record BookAppointmentRequest(
    Guid PatientId,
    Guid DoctorId,
    DateTime DateTime,
    string Complaints);

public record RescheduleAppointmentRequest(
    Guid AppointmentId,
    Guid DoctorId,
    DateTime NewDateTime);