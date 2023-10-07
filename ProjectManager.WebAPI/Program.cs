using ProjectManager.WebAPI.Data;
using ProjectManager.WebAPI.Repositories;
using ProjectManager.WebAPI.Repositories.Interfaces;
using ProjectManager.WebAPI.Services;
using ProjectManager.WebAPI.Services.Interfaces;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(b =>
    {
        b.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IProjectService, ProjectService>();

builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRoleService, RoleService>();

builder.Services.AddScoped<IStatusRepository, StatusRepository>();
builder.Services.AddScoped<IStatusService, StatusService>();

builder.Services.AddScoped<IUserViewRepository, UserViewRepository>();
builder.Services.AddScoped<IUserViewService, UserViewService>();

builder.Services.AddScoped<ICompletedProjectRepository, CompletedProjectRepository>();
builder.Services.AddScoped<ICompletedProjectService, CompletedProjectService>();

builder.Services.AddScoped<IProjectsUsersRepository, ProjectsUsersRepository>();
builder.Services.AddScoped<IProjectsUsersService, ProjectsUsersService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt => opt.AddSignalRSwaggerGen());

builder.Services.AddDbContext<ProjectManagerDbContext>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(_ => true)
    .AllowCredentials());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();



