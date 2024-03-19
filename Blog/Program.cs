using Blog.Api.Middleware;
using Blog.Data.UnitOfWork;
using Blog.Services;
using Blog.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.IO.Compression;
using System.Security.Cryptography;
using static System.Net.Mime.MediaTypeNames;

namespace Blog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;
            var configuration = builder.Configuration;
            
            builder.Services.AddOptions<Api.ApplicationSettings.Main>()
                .Bind(configuration.GetSection(Api.ApplicationSettings.Main.Section))
                .ValidateDataAnnotations();

            services.AddHttpContextAccessor();
            services.AddControllers();
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<GzipCompressionProvider>();
            });
            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });

            services.AddAutoMapper(typeof(Blog.Model.DefaultProfile));


            services.AddDbContext<Data.Context.BlogContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("BlogDbConnection")));

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            });



            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(types => types.ToString());
                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
                {
                    Description = "Authorization header using the Bearer scheme",
                    Name = "Authorization",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
            });

            var CoresSpecification = "CoresSpecification";
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: CoresSpecification,
                                  policy =>
                                  {
                                      policy.AllowAnyOrigin();
                                      policy.AllowAnyMethod();
                                      policy.AllowAnyHeader();
                                  });
            });

            services.AddControllers();

            // Register Services

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPostService, PostService>();


            var app = builder.Build();
            
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List));
            }

            app.UseCors(CoresSpecification);
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseJwtToken();

            app.MapControllers();

            app.Run();
        }
    }
}