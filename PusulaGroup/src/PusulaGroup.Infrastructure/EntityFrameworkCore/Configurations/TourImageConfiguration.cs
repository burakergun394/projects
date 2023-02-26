using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PusulaGroup.Domain.Entities;
using PusulaGroup.Infrastructure.EntityFrameworkCore.Constants;

namespace PusulaGroup.Infrastructure.EntityFrameworkCore.Configurations
{
    public class TourImageConfiguration : BaseConfiguration<TourImage>
    {
        public override void Builder(EntityTypeBuilder<TourImage> builder)
        {
            builder.ToTable("TOUR_IMAGES");

            builder.Property(x => x.TourId)
                .HasColumnName("TOUR_ID")
                .IsRequired();

            builder.Property(x => x.Path)
               .HasColumnName("TOUR_IMAGE_PATH")
               .HasColumnType(EntityFrameworkCoreConstants.GetColumnTypeNVarChar(2000))
               .HasDefaultValue(string.Empty)
               .IsRequired();

            builder.Property(x => x.IsMain)
                .HasColumnName("IS_MAIN_TOUR_IMAGE")
                .IsRequired();

            var datas = new List<TourImage>();

            datas.Add(new TourImage
            {
                Id = 1,
                TourId = 1,
                IsMain = true,
                Path = @"images\tur-1-resim-1.png",
            });

            datas.Add(new TourImage
            {
                Id = 2,
                TourId = 1,
                IsMain = false,
                Path = @"images\tur-1-resim-2.png",
            });

            datas.Add(new TourImage
            {
                Id = 3,
                TourId = 1,
                IsMain = false,
                Path = @"images\tur-1-resim-3.png",
            });

            datas.Add(new TourImage
            {
                Id = 4,
                TourId = 2,
                IsMain = false,
                Path = @"images\tur-2-resim-1.png",
            });

            datas.Add(new TourImage
            {
                Id = 5,
                TourId = 2,
                IsMain = true,
                Path = @"images\tur-2-resim-2.png",
            });

            datas.Add(new TourImage
            {
                Id = 6,
                TourId = 3,
                IsMain = true,
                Path = @"images\tur-3-resim-1.png",
            });

            datas.Add(new TourImage
            {
                Id = 7,
                TourId = 4,
                IsMain = true,
                Path = @"images\tur-4-resim-1.png",
            });

            datas.Add(new TourImage
            {
                Id = 8,
                TourId = 5,
                IsMain = true,
                Path = @"images\tur-5-resim-1.png",
            });

            builder.HasData(datas);
        }
    }
}
