using Microsoft.AspNetCore.Diagnostics;
using NLayer.Api.Exceptions;
using NLayer.Core.DTOs;
using System.Text.Json;

namespace NLayer.Api.Middlewares
{
    public static class UseCustomExcetionHandler
    {
        //program.cs de app. olanlar uzerine mouse getirsek WebApplication gosterircek menbesini. WebApplicationa baxsaq IApplicationBuilder interfaceden miras aldigini goreceyik. Ona gorede burada extension metod yazanda IApplicationBuilder gore yaziriq
        public static void UseCustomException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(configuration =>
            {
                //Run sonlandırııc middleware-dir. Bütün proseslər buradan sonlandırılaraq geriyə döndürləcək
                configuration.Run(async context =>
                {
                    context.Response.ContentType = "application/json";

                    //Buradan errorlari ele aliriq
                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
                    //Eger client terefli errordursa 400 kodu at, eks halda 500 at
                    //Errorlar uzre statuscodlari switch uzerinde ele aliriq
                    var statuscode = exceptionFeature.Error switch
                    {
                        ClientSideException =>400,
                        _=>500
                    };

                    context.Response.StatusCode = statuscode;

                    var response = CustomResponseDto<NoContentResponseDto>.Fail(statuscode, exceptionFeature.Error.Message);
                    //Burad Json tipine cevirib teqdim etmek ucun yaziriq bu kodu
                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));

                    //program.cs de app.UseCustomException() yazaq
                });
            });
        }
    }
}
