using Hospital.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

int count = PatientStorage.Patients.Count;

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();

app.Run();