using Material.BLL.Services;
using Material.BLL.Services.Interfaces;
using Material.DAL;
using Material.Web.Helpers;
using Material.DAL.Repository;
using Material.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Material.Web;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        // Добавляем контроллеры
        services.AddControllers();

        // Добавляем AutoMapper
        services.AddAutoMapper(typeof(Startup));

        // Регистрация зависимостей
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddSingleton<TokenHelper>();

        // Настройка подключения к базе данных
        var connectionString = Environment.GetEnvironmentVariable("SQLSERVER_CONNECTION_STRING") 
                               ?? Configuration.GetConnectionString("ConnectionString");

        services.AddDbContext<MaterialDbContext>(options =>
            options.UseSqlServer(connectionString));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Обработка ошибок в зависимости от окружения
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        // Настройка статических файлов и маршрутов
        app.UseStaticFiles();
        app.UseRouting();

        // Настройка аутентификации и авторизации
        app.UseAuthentication();
        app.UseAuthorization();

        // Настройка маршрутов контроллеров
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers(); // Маппинг маршрутов для контроллеров
        });
    }
}
