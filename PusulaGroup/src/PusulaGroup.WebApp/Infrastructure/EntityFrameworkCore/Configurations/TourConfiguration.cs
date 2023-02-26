using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PusulaGroup.WebApp.Domain.Entities;
using PusulaGroup.WebApp.Infrastructure.EntityFrameworkCore.Constants;

namespace PusulaGroup.WebApp.Infrastructure.EntityFrameworkCore.Configurations
{
    public class TourConfiguration : BaseConfiguration<Tour>
    {
        public override void Builder(EntityTypeBuilder<Tour> builder)
        {
            builder.ToTable("TOURS");

            builder.Property(x => x.Name)
                .HasColumnName("TOUR_NAME")
                .HasColumnType(EntityFrameworkCoreConstants.GetColumnTypeNVarChar(50))
                .HasDefaultValue(string.Empty);

            builder.Property(x => x.UserFriendlyName)
               .HasColumnName("TOUR_USER_FRIENDLY_NAME")
               .HasColumnType(EntityFrameworkCoreConstants.GetColumnTypeNVarChar(50))
               .HasDefaultValue(string.Empty);

            builder.Property(x => x.Content)
                .HasColumnName("TOUR_CONTENT")
                .HasColumnType(EntityFrameworkCoreConstants.GetColumnTypeNVarChar(4000))
                .HasDefaultValue(string.Empty);

            builder.Property(x => x.ParentId)
               .HasColumnName("PARENT_ID")
               .HasDefaultValue(0);

            builder.Property(x => x.SliderOrderIndex)
               .HasColumnName("SLIDER_ORDER_INDEX")
               .HasDefaultValue(0);

            builder.Property(x => x.SliderText)
               .HasColumnName("SLIDER_TEXT")
               .HasColumnType(EntityFrameworkCoreConstants.GetColumnTypeNVarChar(4000))
               .HasDefaultValue(string.Empty);

            builder.Property(x => x.IsShowSlider)
               .HasColumnName("IS_SHOW_SLIDER")
               .HasDefaultValue(0);

            builder.Property(x => x.SeoTitle)
               .HasColumnName("SEO_TITLE")
               .HasColumnType(EntityFrameworkCoreConstants.GetColumnTypeNVarChar(2000))
               .HasDefaultValue(string.Empty);

            builder.Property(x => x.SeoDescription)
               .HasColumnName("SEO_DESCRIPTION")
               .HasColumnType(EntityFrameworkCoreConstants.GetColumnTypeNVarChar(2000))
               .HasDefaultValue(string.Empty);

            builder.Property(x => x.SeoKeywords)
               .HasColumnName("SEO_KEYWORDS")
               .HasColumnType(EntityFrameworkCoreConstants.GetColumnTypeNVarChar(2000))
               .HasDefaultValue(string.Empty);

            builder.Property(x => x.SeoAuthor)
              .HasColumnName("SEO_AUTHOR")
              .HasColumnType(EntityFrameworkCoreConstants.GetColumnTypeNVarChar(2000))
              .HasDefaultValue(string.Empty);


            var datas = new List<Tour>();

            datas.Add(new Tour
            {
                Id = 1,
                Name = "Tur-1",
                UserFriendlyName = "Tur 1",
                Content = "Tur 1 basliyor",
                ParentId = 0
            });

            datas.Add(new Tour
            {
                Id = 2,
                Name = "Tur-2",
                UserFriendlyName = "Tur 2",
                Content = "Tur 2 basliyor",
                ParentId = 0
            });

            datas.Add(new Tour
            {
                Id = 3,
                Name = "Tur-3",
                UserFriendlyName = "Tur 3",
                Content = "Tur 3 basliyor",
                ParentId = 1
            });

            datas.Add(new Tour
            {
                Id = 4,
                Name = "Tur-4",
                UserFriendlyName = "Tur 4",
                Content = "Tur 4 basliyor",
                ParentId = 1
            });

            datas.Add(new Tour
            {
                Id = 5,
                Name = "Tur-5",
                UserFriendlyName = "Tur 5",
                Content = "Tur 5 basliyor",
                ParentId = 0
            });

            builder.HasData(datas);
        }
    }
}
