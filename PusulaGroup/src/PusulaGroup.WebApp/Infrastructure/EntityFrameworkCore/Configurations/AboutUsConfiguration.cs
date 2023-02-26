using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PusulaGroup.WebApp.Domain.Entities;
using PusulaGroup.WebApp.Infrastructure.EntityFrameworkCore.Constants;

namespace PusulaGroup.WebApp.Infrastructure.EntityFrameworkCore.Configurations
{
    public class AboutUsConfiguration : BaseConfiguration<AboutUs>
    {
        public override void Builder(EntityTypeBuilder<AboutUs> builder)
        {
            builder.ToTable("ABOUT_USES");

            builder.Property(x => x.AboutUsContent)
                .HasColumnName("ABOUT_US_CONTENT")
                .HasColumnType(EntityFrameworkCoreConstants.GetColumnTypeNVarChar(4000))
                .HasDefaultValue(string.Empty);

            builder.Property(x => x.OurVisionContent)
               .HasColumnName("OUR_VISION_CONTENT")
               .HasColumnType(EntityFrameworkCoreConstants.GetColumnTypeNVarChar(4000))
               .HasDefaultValue(string.Empty);

            builder.Property(x => x.ImagePath)
                .HasColumnName("IMAGE_PATH")
                .HasColumnType(EntityFrameworkCoreConstants.GetColumnTypeNVarChar(4000))
                .HasDefaultValue(string.Empty);

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

            var datas = new List<AboutUs>();

            datas.Add(new AboutUs
            {
                Id = 1,
                AboutUsContent = "About us content",
                OurVisionContent = "Vision content",
                SeoTitle = "About us seo title",
                SeoDescription = "About us seo description"
            });

            builder.HasData(datas);
        }
    }

    public class OurCustomerConfiguration : BaseConfiguration<OurCustomer>
    {
        public override void Builder(EntityTypeBuilder<OurCustomer> builder)
        {
            builder.ToTable("OUR_CUSTOMERS");

            builder.Property(x => x.Header)
                .HasColumnName("HEADER")
                .HasColumnType(EntityFrameworkCoreConstants.GetColumnTypeNVarChar(200))
                .HasDefaultValue(string.Empty)
                .IsRequired();

            builder.Property(x => x.SubHeader)
               .HasColumnName("SUB_HEADER")
               .HasColumnType(EntityFrameworkCoreConstants.GetColumnTypeNVarChar(200))
               .HasDefaultValue(string.Empty)
               .IsRequired();

            builder.Property(x => x.Content)
                .HasColumnName("CONTENT")
                .HasColumnType(EntityFrameworkCoreConstants.GetColumnTypeNVarChar(4000))
                .HasDefaultValue(string.Empty)
                .IsRequired();

            builder.Property(x => x.ImagePath)
              .HasColumnName("IMAGE_PATH")
              .HasColumnType(EntityFrameworkCoreConstants.GetColumnTypeNVarChar(2000))
              .HasDefaultValue(string.Empty)
              .IsRequired();

            var datas = new List<OurCustomer>();

            datas.Add(new OurCustomer
            {
                Id = 1
            });

            builder.HasData(datas);
        }
    }
}
