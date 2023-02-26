using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PusulaGroup.Domain.Entities;
using PusulaGroup.Infrastructure.EntityFrameworkCore.Constants;

namespace PusulaGroup.Infrastructure.EntityFrameworkCore.Configurations
{
    public class ServiceConfiguration : BaseConfiguration<Service>
    {
        public override void Builder(EntityTypeBuilder<Service> builder)
        {
            builder.ToTable("SERVICES");

            builder.Property(x => x.Name)
                .HasColumnName("SERVICE_NAME")
                .HasColumnType(EntityFrameworkCoreConstants.GetColumnTypeNVarChar(50))
                .HasDefaultValue(string.Empty)
                .IsRequired();

            builder.Property(x => x.UserFriendlyName)
               .HasColumnName("SERVICE_USER_FRIENDLY_NAME")
               .HasColumnType(EntityFrameworkCoreConstants.GetColumnTypeNVarChar(50))
               .HasDefaultValue(string.Empty)
               .IsRequired();

            builder.Property(x => x.Content)
                .HasColumnName("SERVICE_CONTENT")
                .HasColumnType(EntityFrameworkCoreConstants.GetColumnTypeNVarChar(4000))
                .HasDefaultValue(string.Empty)
                .IsRequired();

            var datas = new List<Service>();

            datas.Add(new Service
            {
                Id = 1,
                Name = "Hizmet-1",
                UserFriendlyName = "Hizmet 1",
                Content = "Hizmet 1 basliyor",
            });

            datas.Add(new Service
            {
                Id = 2,
                Name = "Hizmet-2",
                UserFriendlyName = "Hizmet 2",
                Content = "Hizmet 2 basliyor",
            });

            datas.Add(new Service
            {
                Id = 3,
                Name = "Hizmet-3",
                UserFriendlyName = "Hizmet 3",
                Content = "Hizmet 3 basliyor",
            });

            datas.Add(new Service
            {
                Id = 4,
                Name = "Hizmet-4",
                UserFriendlyName = "Hizmet 4",
                Content = "Hizmet 4 basliyor",
            });

            builder.HasData(datas);
        }
    }
}
