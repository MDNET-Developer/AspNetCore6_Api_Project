using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Configurations
{
    public class ProductConfugiration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).HasMaxLength(155);
            builder.Property(x => x.Stock).IsRequired();

            //Asagidaki emeliyyati yazmasaq bele EF core bizim entitylerde yazdigimiz elaqeni anlayacaq. Lakin etibarliq baximindan yaziriq
            builder.HasOne(x => x.Category).WithMany(x => x.Products).HasForeignKey(x => x.CategoryId);

            builder.HasData(
            new Product()
            {
                Id = 1,
                CategoryId = 1,
                Name = "HP Victus 15.6",
                Stock = 10,
                Price = 1489,
                CreatedDate = DateTime.Now

            },
             new Product()
             {
                 Id = 2,
                 CategoryId = 1,
                 Name = "Lenovo Ideapad Gaming 3",
                 Stock = 11,
                 Price = 1690,
                 CreatedDate = DateTime.Now
             },
              new Product()
              {
                  Id = 3,
                  CategoryId = 2,
                  Name = "Redmi Pad",
                  Stock = 55,
                  Price = 450,
                  CreatedDate = DateTime.Now
              },
              new Product()
              {
                  Id = 4,
                  CategoryId = 2,
                  Name = "MI Pad",
                  Stock = 15,
                  Price = 855,
                  CreatedDate = DateTime.Now
              },
              new Product()
              {
                  Id = 5,
                  CategoryId = 3,
                  Name = "Poco F4 GT",
                  Stock = 35,
                  Price = 890,
                  CreatedDate = DateTime.Now
              },
               new Product()
               {
                   Id = 6,
                   CategoryId = 3,
                   Name = "Realme C2",
                   Stock = 20,
                   Price = 225,
                   CreatedDate = DateTime.Now
               },
                new Product()
                {
                    Id = 7,
                    CategoryId = 3,
                    Name = "Samsumg A75",
                    Stock = 17,
                    Price = 875,
                    CreatedDate = DateTime.Now
                }
            );
        }
    }
}
