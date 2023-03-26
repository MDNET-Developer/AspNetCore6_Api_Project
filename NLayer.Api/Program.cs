using Microsoft.EntityFrameworkCore;
using NLayer.Core.Repositories;
using NLayer.Core.UnitOfWork;
using NLayer.Repository.Repositories;
using NLayer.Repository.UnitOfWork;
using NLayer.Repository;
using System.Reflection;
using NLayer.Service.Mapping;
using AutoMapper;
using System.Xml.Linq;
using NLayer.Core.Service;
using NLayer.Service.Services;
using FluentValidation.AspNetCore;
using NLayer.Service.Validations;
using Microsoft.AspNetCore.Mvc;
using NLayer.Api.Filter;
using NLayer.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(opt => opt.Filters.Add(new ValidateFilterAtribute())).AddFluentValidation(options =>
    options.RegisterValidatorsFromAssemblyContaining<ProductDtoValidator>());

//Burada ASP.NET-in default oz filteri var onu baglayaq
builder.Services.Configure<ApiBehaviorOptions>(opt =>
{
    opt.SuppressModelStateInvalidFilter = true;
});
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), options =>
    {//Demeli biz burada yaradacagimiz migrationun yerini teyin edirik. Istesek eger options.MigrationsAssembly("NLayerApi.Repository") kimide yazib yerini teyin ede bilerik. Sadece ne vaxtsa biz bu qatmanin adini deyissek eger mecbur burada gelib deyismeliyik. Asagidaki kodun izahi ile desek Assemblyden AppDbContext olan qatmanin adini alacaq 
        options.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
    });
});

builder.Services.AddAutoMapper(opt =>
{
opt.AddProfiles(new List<Profile>(){
    new MappingProfile()
});
});

//builder.Services.AddAutoMapper(typeof(MappingProfile));


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

app.UseHttpsRedirection();
app.UseCustomException();
app.UseAuthorization();

app.MapControllers();

app.Run();
