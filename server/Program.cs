using Microsoft.EntityFrameworkCore;
using server.Core.Interfaces.Services;
using server.Infrastructure.Data;
using server.Services.ParserService;

namespace server;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddTransient<IParserService, ParserService>();

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite("Data Source=logdb.db"));
        builder.Services.AddControllers();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowAnyOrigin();
            });
        });

        var app = builder.Build();

        app.UseCors("AllowAll");
        app.MapControllers();
        app.UseRouting();
        app.UseAuthentication();

        app.Run();
    }
}
