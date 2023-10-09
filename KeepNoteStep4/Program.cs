using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Service;
using DAL;
using Microsoft.EntityFrameworkCore;
using log4net;
using log4net.Config;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace KeepNoteStep4
{
    public class Program
    { 

        public static void Main(string[] args)
        {

            var log4netRepository =
log4net.LogManager.GetRepository(Assembly.GetEntryAssembly());
            log4net.Config.XmlConfigurator.Configure(log4netRepository, new
FileInfo("log4net.config"));
            var builder = WebApplication.CreateBuilder(args);

            XmlConfigurator.Configure(new FileInfo("log4net.config"));

            // Add services to the container.

            // Register Repositories
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<INoteRepository, NoteRepository>();
            builder.Services.AddScoped<IReminderRepository, ReminderRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            // Register Services
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<INoteService, NoteService>();
            builder.Services.AddScoped<IReminderService, ReminderService>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddDbContext<KeepDbContext>(op =>
            {
                op.UseSqlServer(builder.Configuration["ConnectionStrings:MyConnectionString"]);
            });

            builder.Services.AddControllers();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateAudience = true,
                       ValidateIssuer = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       ValidIssuer = "abc.com",
                       ValidAudience = builder.Configuration["Jwt:Audience"],
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                           builder.Configuration["Jwt:Key"]))
                   };
               });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            
            
            app.MapControllers();

            app.Run();


        }
    }
}


