using HahnAssessmentTask.API;
using HahnAssessmentTask.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowLocalhost5173", policy =>
  {
    policy.WithOrigins("http://localhost:5173")
          .AllowAnyHeader()
          .AllowAnyMethod();
  });
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigurePersistenceApp();

builder.Configuration.AddEnvironmentVariables();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
  DotEnv.Load();
}

app.UseCors("AllowLocalhost5173");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
