using InvitationAPI;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.Configure<GoogleCreds>(options => builder.Configuration.GetSection("GoogleCreds").Bind(options));
builder.Services.AddSingleton(typeof(GoogleSheetsHelper));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "allowed",
        builder =>
        {
            builder.WithOrigins("https://black-coast-0210f7503.1.azurestaticapps.net",
                "http://localhost:5026",
                "https://localhost:7076").AllowAnyHeader()
                .AllowAnyMethod();;
        });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("allowed");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();