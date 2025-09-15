using CliVet.Data.Context;
using CliVet.Services;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

namespace CliVet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            //builder.Services.AddControllers()
            //.ConfigureApiBehaviorOptions(options =>
            //{
            //    options.SuppressModelStateInvalidFilter = true;
            //});
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddDbContext<CliVetContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<TutorService>();
            builder.Services.AddScoped<PetService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapScalarApiReference();
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
