using Caravan.Api.Configuration;
using Caravan.Api.Configuration.LayerConfigurations;
using Caravan.Api.Middlewares;
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
//builder.Services.AddScoped<IOrderService, OrderService>();

//database
builder.ConfigureDataAccess();


//Mapper
builder.Services.AddAutoMapper(typeof(MappingConfiguration));

//Middlewares
var app = builder.Build();
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
