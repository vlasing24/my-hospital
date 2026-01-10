using Hospital.Infrastructure;
using Hospital.Presenters.Extensions;

var builder = WebApplication.CreateBuilder(args);

int count = PatientStorage.Patients.Count;

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddPostgres();

var app = builder.Build();
await app.ApplyMigrations();

app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();

app.Run();