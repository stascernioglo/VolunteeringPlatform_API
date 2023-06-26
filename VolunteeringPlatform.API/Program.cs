using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using VolunteeringPlatform.API.Infrastructure.Extentions;
using VolunteeringPlatform.Bll;
using VolunteeringPlatform.Bll.Interfaces;
using VolunteeringPlatform.Bll.Services;
using VolunteeringPlatform.Dal;
using VolunteeringPlatform.Dal.Interfaces;
using VolunteeringPlatform.Dal.Repositories;
using VolunteeringPlatform.Domain.Auth;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("VolunteeringPlatformConnection");
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<VolunteeringPlatformDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddIdentity<User, Role>(options =>
{
    options.Password.RequiredLength = 8;
})
.AddEntityFrameworkStores<VolunteeringPlatformDbContext>();

builder.Services.Configure<IdentityOptions>(options =>
    options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier);

var authOptions = builder.Services.ConfigureAuthOptions(builder.Configuration);
builder.Services.AddJwtAuthentication(authOptions);
builder.Services.AddControllers();

builder.Services.AddScoped<IRepository, BaseRepository>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IGoodDeedService, GoodDeedService>();
builder.Services.AddScoped<IOrganizationService, OrganizationService>();
builder.Services.AddScoped<IVolunteerService, VolunteerService>();
builder.Services.AddScoped<IAzureStorageService, AzureStorageService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IProjectVolunteerService, ProjectVolunteerService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddAutoMapper(typeof(BllAssemblyMarker));
builder.Services.AddSwagger(builder.Configuration);

var app = builder.Build();

await app.SeedData();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandling();
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(configurePolicy => configurePolicy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseAuthentication();
app.UseAuthorization();
app.UseDbTransaction();

//app.UseDbTransaction();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

await app.RunAsync();
