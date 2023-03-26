using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs
{
    public class CustomResponseDto<T>
    {
        public T? Data { get; set; }

        [JsonIgnore]
        public int StatusCode { get; set; }
        public List<String>? Errors { get; set; }

        public static CustomResponseDto<T> Succsess(int statusCode, T data)
        {
            return new CustomResponseDto<T>() { StatusCode = statusCode, Data = data };
        }

        public static CustomResponseDto<T> Succsess(int statusCode)
        {
            return new CustomResponseDto<T>() { StatusCode = statusCode };
        }

        //Coxlu errorlar alan zaman istifade oluncaq 
        public static CustomResponseDto<T> Fail(int statusCode, List<string> errors)
        {
            return new CustomResponseDto<T>() { StatusCode = statusCode, Errors = errors };
        }


        //Tek error yazmaq istediyimiz zaman istifade olunacaq. Custom elimiz ile " " arasinda yazdigimiz errorlar bura aiddir. Asagida List<string> daxilində error ona gore yazıldı ki, burdan gələm tek errorda ele List<String> Errors -a daxildir.
        public static CustomResponseDto<T> Fail(int statusCode, string error)
        {
            return new CustomResponseDto<T>() { StatusCode = statusCode, Errors = new List<string> { error } };
        }
    }
}
