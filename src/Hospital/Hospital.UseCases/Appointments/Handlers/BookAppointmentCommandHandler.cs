namespace Hospital.UseCases.Appointments.Handlers;

using Hospital.Domain.Appointment;
using Hospital.Domain.Appointment.ValueObjects;
using Hospital.Domain.Doctors.ValueObjects;
using Hospital.Domain.Repositories;
using Hospital.UseCases.Appointments.Commands;
using Hospital.UseCases.Appointments.DTO;
using Hospital.UseCases.Appointments.Exceptions; 
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public interface IBookAppointmentCommandHandler
{
    Task<AppointmentID> HandleAsync(
        BookAppointmentCommand command,
        CancellationToken ct = default);
}

public class BookAppointmentCommandHandler(
    IAppointmentsRepository appointmentsRepository,
    IPatientsRepository patientsRepository,
    IUnitOfWork unitOfWork,
    ITransactionFactory transactionFactory)
    : IBookAppointmentCommandHandler
{
    public async Task<AppointmentID> HandleAsync(
        BookAppointmentCommand command,
        CancellationToken ct = default)
    {
        await using var transaction = await transactionFactory.BeginTransactionAsync(ct);

        try
        {
            if (!await patientsRepository.ExistsAsync(command.PatientId.Value, ct))
                throw new InvalidOperationException($"Пациент с ID {command.PatientId.Value} не найден");

            var slotStart = new DateTime(
                command.AppointmentDateTime.Year,
                command.AppointmentDateTime.Month,
                command.AppointmentDateTime.Day,
                command.AppointmentDateTime.Hour,
                0,
                0);

            var hasConflict = await appointmentsRepository.HasDoctorTimeConflictAsync(
                DoctorID.From(command.DoctorId),
                slotStart,
                withLock: true,
                ct);

            if (hasConflict)
            {
                var availableDoctorIds = await appointmentsRepository.GetAvailableDoctorIdsAsync(
                    slotStart,
                    ct);

                var alternatives = availableDoctorIds
                    .Select(id => new AvailableDoctorDto(
                        id,
                        "Врач",
                        "Специализация"))
                    .ToList();

                await transaction.DisposeAsync();

                throw new DoctorSlotConflictException(
                    $"Врач занят в слоте {slotStart:HH:mm}–{slotStart.AddHours(1):HH:mm}",
                    alternatives);
            }

            var patientConflict = await appointmentsRepository.HasTimeConflictAsync(
                command.PatientId,
                AppointmentDate.Create(command.AppointmentDateTime),
                excludingAppointmentId: null,
                ct);

            if (patientConflict)
                throw new InvalidOperationException(
                    $"Пациент уже записан на приём в слот {slotStart:HH:mm}–{slotStart.AddHours(1):HH:mm}");

            var appointment = Appointment.Create(
                AppointmentID.New(),
                AppointmentPatient.Create(command.PatientId),
                AppointmentDoctor.Create(DoctorID.From(command.DoctorId)),
                AppointmentDate.Create(command.AppointmentDateTime),
                AppointmentComplaints.Create(command.Complaints));

            await appointmentsRepository.AddAsync(appointment, ct);
            await unitOfWork.SaveChangesAsync(ct);
            await transaction.CommitAsync(ct);

            return appointment.Id;
        }
        catch (DoctorSlotConflictException)
        {
            throw;
        }
        catch
        {
            throw;
        }
    }
}