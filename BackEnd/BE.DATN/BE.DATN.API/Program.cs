using BE.DATN.BL.Interfaces.Repository;
using BE.DATN.BL.Interfaces.Services;
using BE.DATN.BL.Interfaces.UnitOfWork;
using BE.DATN.BL.Services;
using BE.DATN.DL.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Policy
builder.Services.AddCors();

var connectionString = builder.Configuration["ConnectionString"];

// Tiêm phụ thuộc
builder.Services.AddScoped<IScoreBL, ScoreBL>();
builder.Services.AddScoped<IScoreDL, ScoreDL>();

builder.Services.AddScoped<IUnitOfWork>(provider => new UnitOfWork(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
