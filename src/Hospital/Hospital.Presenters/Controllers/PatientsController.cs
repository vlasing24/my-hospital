using Hospital.UseCases.Patients.Commands;
using Hospital.UseCases.Patients.Handlers;
using Hospital.UseCases.Patients.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Hospital.Presenters.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly ICreatePatientCommandHandler _createHandler;
        private readonly IGetPatientQueryHandler _getHandler;
        private readonly IGetAllPatientsQueryHandler _getAllHandler;
        private readonly IUpdatePatientCommandHandler _updateHandler;
        private readonly IDeletePatientCommandHandler _deleteHandler;

        public PatientsController(
            ICreatePatientCommandHandler createHandler,
            IGetPatientQueryHandler getHandler,
            IGetAllPatientsQueryHandler getAllHandler,
            IUpdatePatientCommandHandler updateHandler,
            IDeletePatientCommandHandler deleteHandler)
        {
            _createHandler = createHandler;
            _getHandler = getHandler;
            _getAllHandler = getAllHandler;
            _updateHandler = updateHandler;
            _deleteHandler = deleteHandler;
        }

        [HttpPost]
        public IResult Create([FromBody] CreatePatientCommand command)
        {
            try
            {
                var result = _createHandler.Handle(command);
                return new Envelope(result);
            }
            catch (Exception ex)
            {
                return new Envelope(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet("{id:guid}")]
        public IResult Get([FromRoute] Guid id)
        {
            try
            {
                var query = new GetPatientQuery(id);
                var result = _getHandler.Handle(query);
                return new Envelope(result);
            }
            catch (InvalidOperationException ex)
            {
                return new Envelope(HttpStatusCode.NotFound, ex.Message);
            }
        }

        [HttpGet]
        public IResult GetAll()
        {
            try
            {
                var query = new GetAllPatientsQuery();
                var result = _getAllHandler.Handle(query);
                return new Envelope(result);
            }
            catch (Exception ex)
            {
                return new Envelope(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPut("{id:guid}")]
        public IResult Update([FromRoute] Guid id, [FromBody] UpdatePatientCommand command)
        {
            var updatedCommand = command with { Id = id };
            try
            {
                var result = _updateHandler.Handle(updatedCommand);
                return new Envelope(result);
            }
            catch (InvalidOperationException ex)
            {
                return new Envelope(HttpStatusCode.NotFound, ex.Message);
            }
        }

        [HttpDelete("{id:guid}")]
        public IResult Delete([FromRoute] Guid id)
        {
            try
            {
                var command = new DeletePatientCommand(id);
                _deleteHandler.Handle(command);
                return new Envelope(HttpStatusCode.OK, "Patient deleted successfully.");
            }
            catch (InvalidOperationException ex)
            {
                return new Envelope(HttpStatusCode.NotFound, ex.Message);
            }
        }
    }

    // --- Envelope ---
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
}