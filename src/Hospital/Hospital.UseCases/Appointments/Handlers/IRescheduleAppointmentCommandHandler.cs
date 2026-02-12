namespace Hospital.UseCases.Appointments.Handlers;

using Hospital.Domain.Appointment.ValueObjects;
using Hospital.Domain.Doctors.ValueObjects;
using Hospital.Domain.PendingEmail;
using Hospital.Domain.PendingEmail.ValueObjects;
using Hospital.Domain.Repositories;
using Hospital.UseCases.Appointments.Commands;
using Hospital.UseCases.Appointments.DTO;
using Hospital.UseCases.Appointments.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public interface IRescheduleAppointmentCommandHandler
{
    Task<AppointmentID> HandleAsync(
        RescheduleAppointmentCommand command,
        CancellationToken ct = default);
}

public class RescheduleAppointmentCommandHandler(
    IAppointmentsRepository appointmentsRepository,
    IPatientsRepository patientsRepository,
    IPendingEmailsRepository pendingEmailsRepository,
    IUnitOfWork unitOfWork,
    ITransactionFactory transactionFactory)
    : IRescheduleAppointmentCommandHandler
{
    public async Task<AppointmentID> HandleAsync(
        RescheduleAppointmentCommand command,
        CancellationToken ct = default)
    {
        await using var transaction = await transactionFactory.BeginTransactionAsync(ct);

        try
        {
            var appointment = await appointmentsRepository.GetByIdAsync(
                command.AppointmentId,
                withLock: true,
                ct);

            if (appointment == null)
                throw new InvalidOperationException($"Запись с ID {command.AppointmentId} не найдена");

            if (appointment.AssignedDoctor.DoctorId.Value != command.DoctorId)
                throw new InvalidOperationException("Запись не принадлежит указанному врачу");

            var newSlotStart = new DateTime(
                command.NewDateTime.Year,
                command.NewDateTime.Month,
                command.NewDateTime.Day,
                command.NewDateTime.Hour,
                0,
                0);
            var newSlotEnd = newSlotStart.AddHours(1);

            var hasConflict = await appointmentsRepository.HasDoctorTimeConflictAsync(
                appointment.AssignedDoctor.DoctorId,
                newSlotStart,
                withLock: true,
                ct);

            if (hasConflict)
            {
                var alternativeSlots = await GetAlternativeSlotsAsync(
                    appointment.AssignedDoctor.DoctorId,
                    command.NewDateTime.Date,
                    ct);

                await transaction.DisposeAsync();

                throw new TimeSlotConflictException(
                    $"Врач занят в слоте {newSlotStart:HH:mm}–{newSlotEnd:HH:mm}",
                    alternativeSlots);
            }

            var patient = await patientsRepository.GetByIdAsync(
                appointment.Patient.PatientId.Value,
                ct: ct);

            if (patient == null)
                throw new InvalidOperationException("Пациент не найден");

            var notification = PendingEmail.Create(
                PendingEmailID.New(),
                patient.Id,
                "patient@example.com",
                "Ваша запись на приём перенесена",
                $"Уважаемый(ая) {patient.FullName.FirstName},\n\n" +
                $"Ваша запись на приём к врачу перенесена.\n" +
                $"Старое время: {appointment.DateTime.Value}\n" +
                $"Новое время: {command.NewDateTime}\n\n" +
                $"С уважением, Администрация клиники");

            await pendingEmailsRepository.AddAsync(notification, ct);
            await unitOfWork.SaveChangesAsync(ct);
            await transaction.CommitAsync(ct);

            return appointment.Id;
        }
        catch (TimeSlotConflictException)
        {
            throw;
        }
        catch
        {
            throw;
        }
    }

    private async Task<List<AvailableTimeSlotDto>> GetAlternativeSlotsAsync(
        DoctorID doctorId,
        DateTime date,
        CancellationToken ct)
    {
        var alternativeSlots = new List<AvailableTimeSlotDto>();

        for (int hour = 9; hour < 18; hour++)
        {
            var slotStart = new DateTime(date.Year, date.Month, date.Day, hour, 0, 0);
            var hasConflict = await appointmentsRepository.HasDoctorTimeConflictAsync(
                doctorId,
                slotStart,
                withLock: false,
                ct);

            if (!hasConflict)
            {
                alternativeSlots.Add(new AvailableTimeSlotDto(
                    slotStart,
                    slotStart.AddHours(1)));
            }
        }

        return alternativeSlots;
    }
}