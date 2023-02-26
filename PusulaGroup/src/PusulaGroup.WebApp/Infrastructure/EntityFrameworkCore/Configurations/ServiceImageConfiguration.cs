using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PusulaGroup.WebApp.Domain.Entities;
using PusulaGroup.WebApp.Infrastructure.EntityFrameworkCore.Constants;

namespace PusulaGroup.WebApp.Infrastructure.EntityFrameworkCore.Configurations
{
    public class ServiceImageConfiguration : BaseConfiguration<ServiceImage>
    {
        public override void Builder(EntityTypeBuilder<ServiceImage> builder)
        {
            builder.ToTable("SERVICE_IMAGES");

            builder.Property(x => x.ServiceId)
                .HasColumnName("SERVICE_ID");

            builder.Property(x => x.Path)
               .HasColumnName("SERVICE_IMAGE_PATH")
               .HasColumnType(EntityFrameworkCoreConstants.GetColumnTypeNVarChar(2000))
               .HasDefaultValue(string.Empty);

            builder.Property(x => x.IsMain)
                .HasColumnName("IS_MAIN_SERVICE_IMAGE");

            var datas = new List<ServiceImage>();

            datas.Add(new ServiceImage
            {
                Id = 1,
                ServiceId = 1,
                IsMain = true,
                Path = @"/images/services/service-1.jpg",
            });

            datas.Add(new ServiceImage
            {
                Id = 2,
                ServiceId = 1,
                IsMain = false,
                Path = @"/images/services/service-2.jpg",
            });

            datas.Add(new ServiceImage
            {
                Id = 3,
                ServiceId = 1,
                IsMain = false,
                Path = @"/images/services/service-1.jpg",
            });

            datas.Add(new ServiceImage
            {
                Id = 4,
                ServiceId = 2,
                IsMain = false,
                Path = @"/images/services/service-1.jpg",
            });

            datas.Add(new ServiceImage
            {
                Id = 5,
                ServiceId = 2,
                IsMain = true,
                Path = @"/images/services/service-2.jpg",
            });

            datas.Add(new ServiceImage
            {
                Id = 6,
                ServiceId = 3,
                IsMain = true,
                Path = @"/images/services/service-2.jpg",
            });

            datas.Add(new ServiceImage
            {
                Id = 7,
                ServiceId = 4,
                IsMain = true,
                Path = @"/images/services/service-2.jpg",
            });

            builder.HasData(datas);
        }
    }
}
