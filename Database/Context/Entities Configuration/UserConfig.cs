using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreAPI.Database.Entities;
using System.Data.Entity.ModelConfiguration;

namespace StoreAPI.Database_Context.EntitiesConfiguration
{
    public class UserConfig: IEntityTypeConfiguration<User>
    {

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                        .IsRequired()
                        .ValueGeneratedOnAdd();
            builder.Property(x => x.Name)
                        .IsRequired()
                        .HasMaxLength(256);
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
