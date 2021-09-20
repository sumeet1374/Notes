using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Notes.Api.Auth;
using Notes.Api.Filters;
using Notes.Db.Implementation;
using Notes.Db.Interfaces;
using Notes.Services.Implementation;
using Notes.Services.Interfaces;
using Notes.Services.Model;
using System.Security.Claims;

namespace Notes.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                                  builder =>
                                  {
                                      builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
                                  });
            });

            services.AddControllers(options => options.Filters.Add(new ExceptionFilter()));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Notes.Api", Version = "v1" });
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = Configuration["Auth0:Authority"];
                options.Audience = Configuration["Auth0:Audience"];
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = ClaimTypes.NameIdentifier
                };
            });

            services.AddAuthorization((options) =>
            {
                options.AddPolicy(AuthPolicies.USER, policy => policy.Requirements.Add(new NotesAuthRequirement() { IsAdmin = false }));
                options.AddPolicy(AuthPolicies.ADMIN, policy => policy.Requirements.Add(new NotesAuthRequirement() { IsAdmin = true }));
            });

          

            services.AddScoped<IAuthorizationHandler, NotesAuthorizationHandler>();
            services.Configure<IdpServiceConfiguration>(Configuration.GetSection("Auth0"));
            services.AddScoped<IDbProvider, SqlLiteDbProvider>((x)=>  new SqlLiteDbProvider(Configuration.GetConnectionString("NotesDbConnection")));
           
          
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IIdpService, IdpService>();
            services.AddScoped<INotesService, NotesService>();
            services.AddScoped<IUserDao, UserDao>();
            services.AddScoped<INotesDao, NotesDao>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Notes.Api v1"));
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
