using System.Reflection;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Common;
using MovieStoreWebapi.DbOprations;
using MovieStoreWebapi.Middlewares;
using MovieStoreWebapi.Services;


internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        //.NET5'te startup.cs dosyası yerine buraya ekledik.
         builder.Services.AddControllers();
         builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt=>{
             opt.TokenValidationParameters=new TokenValidationParameters{
                 ValidateAudience=true,
                 ValidateIssuer=true,
                 ValidateLifetime=true,
                 ValidateIssuerSigningKey=true,
                 ValidIssuer = builder.Configuration["Token:Issuer"],
                 ValidAudience = builder.Configuration["Token:Audience"],
                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
                 ClockSkew = TimeSpan.Zero,

            };
         });
        builder.Services.AddDbContext<MovieStoreDbContext>(opt=>opt.UseInMemoryDatabase(databaseName: "MovieStoreDB"));
        builder.Services.AddScoped<IMovieStoreDbContext>(grovider=>grovider.GetService<MovieStoreDbContext>());
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
       
        builder.Services.AddSingleton<ILoggerServices,ConsoleLogger>();
       // builder.Services.AddSingleton<ILoggerServices,DBLogger>();
            
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        

        var app = builder.Build();
        
       


        //Datayı başlatmak için (initialize data)
         using var scope = app.Services.CreateScope();
         var serviceProvider = scope.ServiceProvider;
         DataGenerator.Initialize(serviceProvider);


        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseAuthentication();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UserCustomExceptionMiddle();

        app.MapControllers();


        app.Run();
    }
}
