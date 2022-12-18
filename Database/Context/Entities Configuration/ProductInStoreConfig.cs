using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreAPI.Database.Entities;

namespace StoreAPI.Database.EntitiesConfiguration
{
    public class ProductInStoreConfig : IEntityTypeConfiguration<ProductsInStores>
    {
        public void Configure(EntityTypeBuilder<ProductsInStores> builder)
        {
            builder.HasKey(x => new { x.ProductId, x.StoreId });
            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.StoreId).IsRequired();
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);

            builder.HasOne(x => x.Product).WithMany(x => x.ProductsInStores);
            builder.HasOne(x => x.Store).WithMany(x => x.ProductsInStores);
        }
        //public Guid ProductId { get; set; }
        //public Guid StoreId { get; set; }
        //public int Quantity { get; set; }
        //public bool IsDeleted { get; set; }

        //public Product Product { get; set; }
        //public Store Store { get; set; }
    }
}