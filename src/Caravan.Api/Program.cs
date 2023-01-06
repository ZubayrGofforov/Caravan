using Caravan.Api.Configuration.LayerConfigurations;
using Caravan.Service.Common.Security;
using Caravan.Service.Interfaces;
using Caravan.Service.Interfaces.Common;
using Caravan.Service.Interfaces.Security;
using Caravan.Service.Services;
using Caravan.Service.Services.Common;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IAuthManager, AuthManager>();


builder.ConfigureDataAccess();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
