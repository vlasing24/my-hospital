using Hospital.Infrastructure;
using Hospital.UseCases.Patients.Commands;

namespace Hospital.UseCases.Patients.Handlers
{
    public interface IDeletePatientCommandHandler
    {
        void Handle(DeletePatientCommand command);
    }

    public class DeletePatientCommandHandler : IDeletePatientCommandHandler
    {
        public void Handle(DeletePatientCommand command)
        {
            if (!PatientStorage.Patients.Remove(command.Id))
                throw new InvalidOperationException($"Не найден пациент с ID: {command.Id}");
        }
    }
}