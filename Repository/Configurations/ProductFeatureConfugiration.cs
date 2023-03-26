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
    public class ProductFeatureConfugiration : IEntityTypeConfiguration<ProductFeature>
    {
        public void Configure(EntityTypeBuilder<ProductFeature> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            //One to one relations
            builder.HasOne(x => x.Product).WithOne(x => x.ProductFeature).HasForeignKey<ProductFeature>(x => x.ProductId);

            builder.HasData(
            new ProductFeature()
            {
                Id = 1,
                ProductId = 1,
                Width = 1,
                Height = 1,
                Color = "CyberSilver"
            },

            new ProductFeature()
            {
                Id = 2,
                ProductId = 2,
                Width = 2,
                Height = 3,
                Color = "OceanBlue"
            },
            new ProductFeature()
            {
                Id = 3,
                ProductId = 3,
                Width = 2,
                Height = 1,
                Color = "NeonGreen"
            }
            );
        }
    }
}
