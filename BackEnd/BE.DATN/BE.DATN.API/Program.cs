using BE.DATN.BL.Interfaces.Repository;
using BE.DATN.BL.Interfaces.Services;
using BE.DATN.BL.Interfaces.UnitOfWork;
using BE.DATN.BL.Services;
using BE.DATN.DL.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Policy
builder.Services.AddCors();

var connectionString = builder.Configuration["ConnectionString"];

// Tiêm phụ thuộc
builder.Services.AddScoped<IStudentBL, StudentBL>();
builder.Services.AddScoped<IStudentDL, StudentDL>();
builder.Services.AddScoped<ITeacherBL, TeacherBL>();
builder.Services.AddScoped<ITeacherDL, TeacherDL>();
builder.Services.AddScoped<IScoreBL, ScoreBL>();
builder.Services.AddScoped<IScoreDL, ScoreDL>();
builder.Services.AddScoped<IClassesBL, ClassesBL>();
builder.Services.AddScoped<IClassesDL, ClassesDL>();
builder.Services.AddScoped<ISubjectBL, SubjectBL>();
builder.Services.AddScoped<ISubjectDL, SubjectDL>();
builder.Services.AddScoped<IClassRegistrationBL, ClassRegistrationBL>();
builder.Services.AddScoped<IClassRegistrationDL, ClassRegistrationDL>();
builder.Services.AddScoped<IFacultyBL, FacultyBL>();
builder.Services.AddScoped<IFacultyDL, FacultyDL>();
builder.Services.AddScoped<IUserBL, UserBL>();
builder.Services.AddScoped<IUserDL, UserDL>();
builder.Services.AddScoped<IRoleBL, RoleBL>();
builder.Services.AddScoped<IRoleDL, RoleDL>();
builder.Services.AddScoped<IAuthBL, AuthBL>();

builder.Services.AddScoped<IUnitOfWork>(provider => new UnitOfWork(connectionString));

// Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

// Sử dụng xác thực và phân quyền
app.UseAuthentication();
app.UseAuthorization();

app.UseCors("AllowAnyOrigin"); // Áp dụng cấu hình CORS ở đây

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Use(async (context, next) =>
{
    // Kiểm tra xem request có được xác thực hay không
    if (!context.User.Identity.IsAuthenticated)
    {
        // Nếu không được xác thực, trả về lỗi hoặc redirect đến trang đăng nhập
        context.Response.StatusCode = 401; // Unauthorized
        await context.Response.WriteAsync("Unauthorized");
        return;
    }

    // Tiếp tục xử lý request
    await next();
});

app.Run();
