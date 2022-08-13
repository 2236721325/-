using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shared.BaseRepositorys;
using Shared.BaseServices;
using System.Text;
using User_DLL.Contexts;
using User_DLL.IRepositorys;
using User_DLL.Models;
using User_DLL.Models.Extensions;
using User_DLL.Repositorys;
using User_Service.IServices;
using User_Service.Services;

namespace JWT_API
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
            //JWT
            builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("JWT"));//Ҳ������ע�� ע���ʱ�����Ĭ��ֵ��appsettings��ȡ��

            builder.Services.AddScoped<DbContext, UserDbContext>(); //����MyDbContext��DbContext�ľ���ʵ��(��������� ֮ǰ��Ŀ���ֲ㲻��Ҫ��� ���ˣ���

            //Valitorע��
            builder.Services.AddScoped<IValidator<User>, UserValidator>();
            builder.Services.AddScoped<IValidator<CheckRecord>, CheckRecordValidator>();
            //AutoMapper
            builder.Services.AddAutoMapper(typeof(CustomAutoMapperProfile));

            builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ICheckRecordRepository, CheckRecordRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ICheckRecordService, CheckRecordService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseSwagger();
            app.UseSwaggerUI();


            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}