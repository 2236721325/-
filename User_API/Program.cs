using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shared.BaseRepositorys;
using Shared.BaseServices;
using System.Text;
using User_API.JWTs;
using User_DLL.Contexts;
using User_DLL.IRepositorys;
using User_DLL.Models;
using User_DLL.Models.Extensions;
using User_DLL.Repositorys;
using User_Service.Helps;
using User_Service.IServices;
using User_Service.Services;

namespace User_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();



            builder.Services.AddDbContext<UserDbContext>(options => options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection"), x =>
                x.MigrationsAssembly("User_DLL")//Ǩ�ƴ����User_DLL
                ));

            builder.Services.AddScoped<DbContext, UserDbContext>();//����MyDbContext��DbContext�ľ���ʵ��
            //JWT ��֤ ��ЩapiҪ���¼״̬�¿ɷ���
            builder.Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                //����JWT�����֤����
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                //ʹ��JWT�����֤��ս

            }).AddJwtBearer(option =>
            {
                var jwtSettings = builder.Configuration.GetSection("JWT").Get<JWTSettings>();

                byte[] keyBytes = Encoding.UTF8.GetBytes(jwtSettings.SecretKey);
                var secretKey = new SymmetricSecurityKey(keyBytes);
                //��֤���������
                option.TokenValidationParameters = new()
                {
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = secretKey
                };

            });

            builder.Services.AddUser_Service();//�Լ�д����չ����


            var app = builder.Build();
            
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthentication();//��֤�Ƿ��¼ ���м��
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

       
    }
}