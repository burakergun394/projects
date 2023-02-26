using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PusulaGroup.Domain.Entities;
using PusulaGroup.Infrastructure.EntityFrameworkCore.Constants;

namespace PusulaGroup.Infrastructure.EntityFrameworkCore.Configurations
{
    public class ServiceImageConfiguration : BaseConfiguration<ServiceImage>
    {
        public override void Builder(EntityTypeBuilder<ServiceImage> builder)
        {
            builder.ToTable("SERVICE_IMAGES");

            builder.Property(x => x.ServiceId)
                .HasColumnName("SERVICE_ID")
                .IsRequired();

            builder.Property(x => x.Path)
               .HasColumnName("SERVICE_IMAGE_PATH")
               .HasColumnType(EntityFrameworkCoreConstants.GetColumnTypeNVarChar(2000))
               .HasDefaultValue(string.Empty)
               .IsRequired();

            builder.Property(x => x.IsMain)
                .HasColumnName("IS_MAIN_SERVICE_IMAGE")
                .IsRequired();

            var datas = new List<ServiceImage>();

            datas.Add(new ServiceImage
            {
                Id = 1,
                ServiceId = 1,
                IsMain = true,
                Path = @"images\hizmet-1-resim-1.png",
            });

            datas.Add(new ServiceImage
            {
                Id = 2,
                ServiceId = 1,
                IsMain = false,
                Path = @"images\hizmet-1-resim-2.png",
            });

            datas.Add(new ServiceImage
            {
                Id = 3,
                ServiceId = 1,
                IsMain = false,
                Path = @"images\hizmet-1-resim-3.png",
            });

            datas.Add(new ServiceImage
            {
                Id = 4,
                ServiceId = 2,
                IsMain = false,
                Path = @"images\hizmet-2-resim-1.png",
            });

            datas.Add(new ServiceImage
            {
                Id = 5,
                ServiceId = 2,
                IsMain = true,
                Path = @"images\hizmet-2-resim-2.png",
            });

            datas.Add(new ServiceImage
            {
                Id = 6,
                ServiceId = 3,
                IsMain = true,
                Path = @"images\hizmet-3-resim-1.png",
            });

            datas.Add(new ServiceImage
            {
                Id = 7,
                ServiceId = 4,
                IsMain = true,
                Path = @"images\hizmet-4-resim-1.png",
            });

            builder.HasData(datas);
        }
    }
}
