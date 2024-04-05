using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

using API.DataAccess;

namespace API;

/// <summary>
/// Registering services for the API
/// </summary>
/// <param name="configuration">config for connection strings</param>
public class Startup(IConfiguration configuration) {
    private const string name = "API";
    private const string version = "v0";
    private const string db_name = "VoteaquoteDB";
    
    /// <summary>
    /// This method gets called by the runtime. Use this method to add services to the container.
    /// </summary>
    /// <param name="services"></param>
    public void ConfigureServices(IServiceCollection services) {
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        
        services.AddCors(options => {
            options.AddDefaultPolicy(
                policy => {
                    policy.AllowAnyOrigin();
                });
        });

        services.AddControllers();
        services.AddMvc();

        services.AddSwaggerGen(options => {
            options.SwaggerDoc(version, new OpenApiInfo {
                Version = version,
                Title = $"Vote a Quote - {name}",
                Description = $"An ASP.NET API"
            });
            options.EnableAnnotations();

            // Set the comments path for the Swagger JSON and UI.
            options.IncludeXmlComments(xmlPath);
            options.SchemaFilter<SwaggerSchemaFilter>();
        });

        services.AddDbContext<Context>(options => {
            options.UseMySql(configuration[$"ConnectionStrings:{db_name}"],
                ServerVersion.AutoDetect(configuration[$"ConnectionStrings:{db_name}"]));
        });
    }
    
    /// <summary>
    /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
        if (env.IsDevelopment()) {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseSwagger();
        app.UseSwaggerUI(options => {
            options.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"{name} {version}");
        });

        app.UseCors(builder => {
            builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        });

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints => {
            endpoints.MapControllers();
        });
    }
}