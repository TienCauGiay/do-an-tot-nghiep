using BE.DATN.BL.Interfaces.Repository;
using BE.DATN.BL.Interfaces.Services;
using BE.DATN.BL.Interfaces.UnitOfWork;
using BE.DATN.BL.Services;
using BE.DATN.DL.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Thêm dịch vụ vào bộ chứa
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Thêm CORS policy
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

var connectionString = builder.Configuration["ConnectionString"];

// Tiêm phụ thuộc
builder.Services.AddHttpContextAccessor(); // Thêm dịch vụ HttpContextAccessor
builder.Services.AddSingleton<ITokenService, TokenService>();
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
builder.Services.AddScoped<IClassRegistrationDetailDL, ClassRegistrationDetailDL>();
builder.Services.AddScoped<IFacultyBL, FacultyBL>();
builder.Services.AddScoped<IFacultyDL, FacultyDL>();
builder.Services.AddScoped<IUserBL, UserBL>();
builder.Services.AddScoped<IUserDL, UserDL>();
builder.Services.AddScoped<IRoleBL, RoleBL>();
builder.Services.AddScoped<IRoleDL, RoleDL>();
builder.Services.AddScoped<ISemesterBL, SemesterBL>();
builder.Services.AddScoped<ISemesterDL, SemesterDL>();
builder.Services.AddScoped<IAuthBL, AuthBL>();

builder.Services.AddScoped<IUnitOfWork>(provider => new UnitOfWork(connectionString));

var app = builder.Build();

// Cấu hình pipeline xử lý yêu cầu HTTP
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

app.UseCors("AllowAnyOrigin"); // Áp dụng cấu hình CORS

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

// Middleware để kiểm tra xác thực
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
