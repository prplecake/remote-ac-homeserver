using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RemoteAc.Core.Interfaces;
using RemoteAc.Core.Interfaces.Repositories;
using RemoteAc.Core.Interfaces.Services;
using RemoteAc.Infrastructure.Repositories;
using RemoteAc.Web.Api.Services;
using RemoteAc.Web.Api.Extensions;

namespace RemoteAc.Web.Api;

public class Startup
{
    private readonly IConfiguration _configuration;
    private IWebHostEnvironment Env;
    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
        _configuration = configuration;
        Env = env;
    }
    public void Configure(IApplicationBuilder app)
    {
        if (Env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "Remote AC Controller API");
            });
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
        }
        app.UseCors("AllowAllOrigins");
        app.UseStatusCodePages();
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            endpoints.MapRazorPages();
        });
    }
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton(_configuration);

        services.ConfigureDatabase(_configuration);

        // Repositories
        services.AddScoped<IAppStateRepository, AppStateRepository>();
        services.AddScoped<IDhtSensorDataRepository, DhtSensorDataRepository>();
        services.AddScoped<ISensorClientRepository, SensorClientRepository>();

        services.AddScoped<IAppStateService, AppStateService>();
        services.AddScoped<IDhtSensorDataService, DhtSensorDataService>();
        services.AddScoped<IRemoteControlService, RemoteControlService>();
        services.AddScoped<ISensorClientService, SensorClientService>();

        services.AddHostedService<UdpListenerService>();

        services.AddHttpContextAccessor();

        services.AddSingleton<IUriService>(o =>
        {
            var accessor = o.GetRequiredService<IHttpContextAccessor>();
            var request = accessor.HttpContext.Request;
            var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
            return new UriService(uri);
        });

        var mvcBuilder = services.AddControllersWithViews();
        var razorBuilder = services.AddRazorPages();
        if (Env.IsDevelopment())
        {
            mvcBuilder.AddRazorRuntimeCompilation();
            razorBuilder.AddRazorRuntimeCompilation();
        }

        services.AddApiVersioning(opt =>
        {
            opt.DefaultApiVersion = new ApiVersion(1, 0);
            opt.AssumeDefaultVersionWhenUnspecified = true;
            opt.ReportApiVersions = true;
            opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("x-api-version"),
                new MediaTypeApiVersionReader("x-api-version"));
        });
        services.AddVersionedApiExplorer(opt =>
        {
            opt.GroupNameFormat = "'v'V";
            opt.SubstituteApiVersionInUrl = true;
        });
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            { Title = "Remote AC Controller API",
              Description = "The API for the remote AC controller.",
              Version = "v1" });
        });

        // CORS
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins",
                builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
        });
    }
}
