using Lab2.Worker;
using Lab2.Worker.Dal;
using Microsoft.EntityFrameworkCore;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddHostedService<QueueWorker>();
        services.AddDbContext<AppDbContext>(
            optins =>
            {
                var cs = context.Configuration.GetConnectionString("KnifesDb");
                optins.UseMySql(cs, ServerVersion.Parse("8.0"),
                ob =>
                {
                    ob.EnableRetryOnFailure();
                });
            });
    })
.Build();


using var scope = host.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
await dbContext.Database.EnsureCreatedAsync();

await host.RunAsync();
