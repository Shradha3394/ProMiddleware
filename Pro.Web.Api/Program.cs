using Pro.Api.Repository.Abstract;
using Pro.Api.Repository.Concrete;
using Pro.Api.Service.Services.Abstract;
using Pro.Api.Service.Services.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var corsPolicy = "CorsPolicy";


builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicy,
        corsPolicyBuilder => corsPolicyBuilder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});


builder.Services.AddControllers().AddJsonOptions(jsonOptions =>
{
    jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = null;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IPartnerService, PartnerService>();
builder.Services.AddTransient<IApiService, ApiService>();

builder.Services.AddTransient<ITypeaheadService, TypeaheadService>();

builder.Services.AddTransient<IPartnerRepository, PartnerRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserService, UserService>();
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
app.UseCors(corsPolicy);

app.Run();
