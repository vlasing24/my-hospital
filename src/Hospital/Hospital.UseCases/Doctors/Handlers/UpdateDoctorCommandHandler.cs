using Hospital.Domain.Doctors.ValueObjects;
using Hospital.Domain.Repositories;
using Hospital.Domain.Utilities;
using Hospital.UseCases.Appointments.DTO;
using Hospital.UseCases.Doctors.Commands;
using Hospital.UseCases.Doctors.Extensions;

namespace Hospital.UseCases.Doctors.Handlers;

public interface IUpdateDoctorCommandHandler
{
    Task<AvailableDoctorDto> HandleAsync(
        UpdateDoctorCommand command,
        CancellationToken ct = default);
}

public class UpdateDoctorCommandHandler(
    IDoctorsRepository doctorsRepository,
    IUnitOfWork unitOfWork,
    ITransactionFactory transactionFactory)
    : IUpdateDoctorCommandHandler
{
    public async Task<AvailableDoctorDto> HandleAsync(
        UpdateDoctorCommand command,
        CancellationToken ct = default)
    {
        // 1. Начинаем транзакцию
        await using var transaction = await transactionFactory.BeginTransactionAsync(ct);

        try
        {
            // 2. Получаем врача с пессимистической блокировкой (защита от гонки транзакций)
            var doctor = await doctorsRepository.GetByIdAsync(
                command.Id,
                withLock: true, // ← FOR UPDATE блокировка
                ct);

            if (doctor == null)
                throw new InvalidOperationException($"Врач с ID {command.Id} не найден");

            // 3. Применяем бизнес-логику обновления
            var updatedDoctor = doctor;

            // Обновляем специализацию, если указана
            if (!string.IsNullOrEmpty(command.Data.Specialization))
            {
                var newSpecialization = DoctorSpecialization.Create(command.Data.Specialization);
                updatedDoctor = updatedDoctor.UpdateSpecialization(newSpecialization);
            }

            // Обновляем ФИО, если указаны
            if (command.Data.FirstName != null ||
                command.Data.MiddleName != null ||
                command.Data.LastName != null)
            {
                var currentFullName = doctor.FullName;
                var newFirstName = command.Data.FirstName ?? currentFullName.FirstName;
                var newMiddleName = command.Data.MiddleName ?? currentFullName.MiddleName;
                var newLastName = command.Data.LastName ?? currentFullName.LastName;

                var newFullName = FullName.Create(newFirstName, newMiddleName, newLastName);

                // Создаём нового врача с обновлёнными данными
                // (предполагаем, что в домене есть метод для обновления ФИО)
                // Если такого метода нет, нужно добавить его в доменную сущность Doctor
                updatedDoctor = Doctor.Create(
                    doctor.Id,
                    newFullName,
                    updatedDoctor.Specialization);
            }

            // 4. Репозиторий помечает сущность как Modified в ChangeTracker
            await doctorsRepository.UpdateAsync(updatedDoctor, ct);

            // 5. Unit of Work сохраняет ВСЕ изменения одним вызовом
            await unitOfWork.SaveChangesAsync(ct);

            // 6. Фиксируем транзакцию
            await transaction.CommitAsync(ct);

            // 7. Возвращаем обновлённого врача
            return updatedDoctor.ToResponseDto();
        }
        catch
        {
            // Автоматический откат при исключении (транзакция не зафиксирована)
            // Откат происходит при вызове Dispose() у transaction
            throw;
        }
    }
}