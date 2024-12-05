using Material.BLL.Services;
using Material.BLL.Services.Interfaces;
using Material.DAL;
using Material.DAL.Repository;
using Material.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Material.Web;

public class Startup(IConfiguration configuration)
{
    public IConfiguration Configuration { get; } = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();
        
        var connectionString = Environment.GetEnvironmentVariable("SQLSERVER_CONNECTION_STRING") ?? Configuration.GetConnectionString("ConnectionString");
        
        services.AddDbContext<MaterialDbContext>(options =>
            options.UseSqlServer(connectionString));
        
        
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }
    }
}