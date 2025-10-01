namespace server;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

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
