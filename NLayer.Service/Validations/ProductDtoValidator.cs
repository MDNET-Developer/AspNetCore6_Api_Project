using FluentValidation;
using NLayer.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Validations
{
    public class ProductDtoValidator : AbstractValidator<ProductDto>
    {
        public ProductDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage($"Boş verilən olnaz !!!");
            RuleFor(x => x.Price).NotNull().InclusiveBetween(1, decimal.MaxValue).WithMessage($"Ən aşağı qiymət 1 olmalıdır");
            RuleFor(x => x.CategoryId).NotNull().NotEmpty().WithMessage($"Boş verilən olnaz !!!");
          
        }
    }
}
