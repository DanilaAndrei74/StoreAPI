using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreAPI.Database.Entities;

namespace StoreAPI.Database.EntitiesConfiguration
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(20);
            builder.Property(x => x.Description)
                .HasMaxLength(20);
            builder.Property(x => x.IsDeleted)
                .HasDefaultValue(false);

            builder.HasMany(x => x.ProductsInStores)
                .WithOne(x => x.Product);
        }

        //public Guid Id { get; set; }
        //public string Name { get; set; }
        //public string Description { get; set; }
        //public bool IsDeleted { get; set; }

        //public ProductsInStores ProductsInStores { get; set; }
    }
}
