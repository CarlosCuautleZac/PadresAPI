using Microsoft.EntityFrameworkCore;
using PadresAPI.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddDbContext<Sistem21PrimariaContext>(optionsBuilder=>optionsBuilder.UseMySql
        ("server=sistemas19.com;database=sistem21_primaria;user=sistem21_primaria;password=sistemas19_;Convert Zero Datetime=True"
            , Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.5.17-mariadb")));
var app = builder.Build();


app.UseRouting();
app.UseFileServer();
app.UseEndpoints(x => x.MapControllers());
app.Run();
