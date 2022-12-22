using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreAPI.Database.Entities;

namespace StoreAPI.Database.EntitiesConfiguration
{
    public class UserConfig: IEntityTypeConfiguration<User>
    {

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                        .IsRequired()
                        .ValueGeneratedOnAdd();
            builder.Property(x => x.FirstName)
                        .IsRequired()
                        .HasMaxLength(20);
            builder.Property(x => x.LastName)
                        .IsRequired()
                        .HasMaxLength(20);
            builder.Property(x => x.IsDeleted)
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false)
                        .IsRequired();

        }

        //public Guid Id { get; set; }
        //public string Name { get; set; }
        //public bool IsDeleted { get; set; }
    }
}
