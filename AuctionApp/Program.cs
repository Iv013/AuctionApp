using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;

using LinqToDB.Data;
using AuctionApp.Data.Tables.Repository;
using AuctionApp.Data.Tables.Repository.IRepository;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllersWithViews();
DataConnection.DefaultSettings = new AuctionsDbSettings();
builder.Services.AddScoped<IAuctionRepository, AuctionRepository>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);



var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseSpa(spa => {
    spa.Options.SourcePath = "wwwroot/dist";

    if (app.Environment.IsDevelopment())
    {
        spa.UseReactDevelopmentServer(npmScript: "start");
    }
});


app.Run();
