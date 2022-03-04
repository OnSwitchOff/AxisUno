using AxisUno.DataBase.Enteties.Products;
using AxisUno.DataBase.Enteties.Products.Enums;
using AxisUno.DataBase.Enteties.Products.Measures;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace AxisUno.Configurations
{
    internal class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(product => product.Id);

            builder.Property(product => product.Id)
                .HasColumnName(nameof(Product.Id))
                .ValueGeneratedOnAdd();

            builder.Property(product => product.Name)
                .HasColumnName(nameof(Product.Name))
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(product => product.Code)
                .HasColumnName(nameof(Product.Code))
                .HasMaxLength(255);

            builder.Property(product => product.Barcode)
                .HasColumnName(nameof(Product.Barcode))
                .HasMaxLength(255);

            builder.Property(product => product.Type)
                .HasColumnName(nameof(Product.Type))
                .HasConversion(new EnumToNumberConverter<ProductType, byte>())
                .IsRequired();

            builder.Property(product => product.Status)
                .HasColumnName(nameof(Product.Status))
                .HasConversion(new EnumToNumberConverter<ProductStatus, byte>())
                .IsRequired();

            builder.OwnsOne(
                product => product.Measure,
                builder =>
                {
                    builder
                        .ToTable("Measures")
                        .Property(measure => measure.Name)
                            .HasColumnName(nameof(Measure.Name))
                            .IsRequired();
                });
        }
    }
}
