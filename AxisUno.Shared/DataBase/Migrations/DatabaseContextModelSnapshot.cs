using AxisUno.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AxisUNO.DataBase.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.2");

            modelBuilder.Entity("AxisUNO.Domain.ProductGroups.ProductGroup", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("INTEGER")
                    .HasColumnName("Id");

                b.Property<decimal>("Discount")
                    .HasColumnType("TEXT")
                    .HasColumnName("Discount");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnType("TEXT")
                    .HasColumnName("Name");

                b.Property<string>("Path")
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnType("TEXT")
                    .HasColumnName("Path");

                b.HasKey("Id");

                b.ToTable("ProductGroups", (string)null);
            });

            modelBuilder.Entity("AxisUNO.Domain.Products.Product", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("INTEGER")
                    .HasColumnName("Id");

                b.Property<string>("Barcode")
                    .HasMaxLength(255)
                    .HasColumnType("TEXT")
                    .HasColumnName("Barcode");

                b.Property<string>("Code")
                    .HasMaxLength(255)
                    .HasColumnType("TEXT")
                    .HasColumnName("Code");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnType("TEXT")
                    .HasColumnName("Name");

                b.Property<int>("ProductGroupId")
                    .HasColumnType("INTEGER");

                b.Property<byte>("Status")
                    .HasColumnType("INTEGER")
                    .HasColumnName("Status");

                b.Property<byte>("Type")
                    .HasColumnType("INTEGER")
                    .HasColumnName("Type");

                b.HasKey("Id");

                b.HasIndex("ProductGroupId");

                b.ToTable("Products", (string)null);
            });

            modelBuilder.Entity("AxisUNO.Domain.Products.Product", b =>
            {
                b.HasOne("AxisUNO.Domain.ProductGroups.ProductGroup", "ProductGroup")
                    .WithMany("Products")
                    .HasForeignKey("ProductGroupId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.OwnsOne("AxisUNO.Domain.Products.Measures.Measure", "Measure", b1 =>
                {
                    b1.Property<int>("ProductId")
                        .HasColumnType("INTEGER");

                    b1.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Name");

                    b1.HasKey("ProductId");

                    b1.ToTable("Measures", (string)null);

                    b1.WithOwner()
                        .HasForeignKey("ProductId");
                });

                b.Navigation("Measure")
                    .IsRequired();

                b.Navigation("ProductGroup");
            });

            modelBuilder.Entity("AxisUNO.Domain.ProductGroups.ProductGroup", b =>
            {
                b.Navigation("Products");
            });
#pragma warning restore 612, 618
        }
    }
}
