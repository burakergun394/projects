using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PusulaGroup.WebApp.Domain.Entities;
using PusulaGroup.WebApp.Infrastructure.EntityFrameworkCore.Constants;

namespace PusulaGroup.WebApp.Infrastructure.EntityFrameworkCore.Configurations
{
    public class TourImageConfiguration : BaseConfiguration<TourImage>
    {
        public override void Builder(EntityTypeBuilder<TourImage> builder)
        {
            builder.ToTable("TOUR_IMAGES");

            builder.Property(x => x.TourId)
                .HasColumnName("TOUR_ID");

            builder.Property(x => x.Path)
               .HasColumnName("TOUR_IMAGE_PATH")
               .HasColumnType(EntityFrameworkCoreConstants.GetColumnTypeNVarChar(2000))
               .HasDefaultValue(string.Empty);

            builder.Property(x => x.IsMain)
                .HasColumnName("IS_MAIN_TOUR_IMAGE");

            var datas = new List<TourImage>();

            datas.Add(new TourImage
            {
                Id = 1,
                TourId = 1,
                IsMain = true,
                Path = @"/images/tours/tour-1.jpg",
            });

            datas.Add(new TourImage
            {
                Id = 2,
                TourId = 1,
                IsMain = false,
                Path = @"/images/tours/tour-2.jpg",
            });

            datas.Add(new TourImage
            {
                Id = 3,
                TourId = 1,
                IsMain = false,
                Path = @"/images/tours/tour-3.jpg",
            });

            datas.Add(new TourImage
            {
                Id = 4,
                TourId = 2,
                IsMain = false,
                Path = @"/images/tours/tour-1.jpg",
            });

            datas.Add(new TourImage
            {
                Id = 5,
                TourId = 2,
                IsMain = true,
                Path = @"/images/tours/tour-2.jpg",
            });

            datas.Add(new TourImage
            {
                Id = 6,
                TourId = 3,
                IsMain = true,
                Path = @"/images/tours/tour-3.jpg",
            });

            datas.Add(new TourImage
            {
                Id = 7,
                TourId = 4,
                IsMain = true,
                Path = @"/images/tours/tour-1.jpg",
            });

            datas.Add(new TourImage
            {
                Id = 8,
                TourId = 5,
                IsMain = true,
                Path = @"/images/tours/tour-2.jpg",
            });

            builder.HasData(datas);
        }
    }
}
