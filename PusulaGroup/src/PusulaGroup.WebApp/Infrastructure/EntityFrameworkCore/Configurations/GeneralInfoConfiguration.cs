using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PusulaGroup.WebApp.Domain.Entities;
using PusulaGroup.WebApp.Infrastructure.EntityFrameworkCore.Constants;

namespace PusulaGroup.WebApp.Infrastructure.EntityFrameworkCore.Configurations
{
    public class GeneralInfoConfiguration : BaseConfiguration<GeneralInfo>
    {
        public override void Builder(EntityTypeBuilder<GeneralInfo> builder)
        {
            builder.ToTable("GENERAL_INFOS");

            builder.Property(x => x.LogoPath)
                .HasColumnName("LOGO_PATH")
                .HasColumnType(EntityFrameworkCoreConstants.GetColumnTypeNVarChar(4000))
                .HasDefaultValue(string.Empty);

            builder.Property(x => x.Email)
               .HasColumnName("EMAIL")
               .HasColumnType(EntityFrameworkCoreConstants.GetColumnTypeNVarChar(50))
               .HasDefaultValue(string.Empty);
           
            builder.Property(x => x.EMailHost)
                .HasColumnName("EMAIL_HOST")
                .HasColumnType(EntityFrameworkCoreConstants.GetColumnTypeNVarChar(50))
                .HasDefaultValue(string.Empty);

            builder.Property(x => x.EmailFrom)
               .HasColumnName("EMAIL_FROM")
               .HasColumnType(EntityFrameworkCoreConstants.GetColumnTypeNVarChar(50))
               .HasDefaultValue(string.Empty);

            builder.Property(x => x.EmailPassword)
               .HasColumnName("EMAIL_PASSWORD")
               .HasColumnType(EntityFrameworkCoreConstants.GetColumnTypeNVarChar(50))
               .HasDefaultValue(string.Empty);

            builder.Property(x => x.EmailPort)
              .HasColumnName("EMAIL_PORT");

            builder.Property(x => x.Address1)
                .HasColumnName("ADDRESS_1")
                .HasColumnType(EntityFrameworkCoreConstants.GetColumnTypeNVarChar(200))
                .HasDefaultValue(string.Empty);

            builder.Property(x => x.Address2)
                .HasColumnName("ADDRESS_2")
                .HasColumnType(EntityFrameworkCoreConstants.GetColumnTypeNVarChar(200))
                .HasDefaultValue(string.Empty);

            builder.Property(x => x.TelephoneNumber)
               .HasColumnName("TELEPHONE_NUMBER")
               .HasColumnType(EntityFrameworkCoreConstants.GetColumnTypeNVarChar(50))
               .HasDefaultValue(string.Empty);

            builder.Property(x => x.WhatsappTelephoneNumber)
              .HasColumnName("WHATSAPP_TELEPHONE_NUMBER")
              .HasColumnType(EntityFrameworkCoreConstants.GetColumnTypeNVarChar(50))
              .HasDefaultValue(string.Empty);

            builder.Property(x => x.FacebookUrl)
               .HasColumnName("FACEBOOK_URL")
               .HasColumnType(EntityFrameworkCoreConstants.GetColumnTypeNVarChar(100))
               .HasDefaultValue(string.Empty);

            builder.Property(x => x.InstagramUrl)
              .HasColumnName("INSTAGRAM_URL")
              .HasColumnType(EntityFrameworkCoreConstants.GetColumnTypeNVarChar(100))
              .HasDefaultValue(string.Empty);

            builder.Property(x => x.TwitterUrl)
              .HasColumnName("TWITTER_URL")
              .HasColumnType(EntityFrameworkCoreConstants.GetColumnTypeNVarChar(100))
              .HasDefaultValue(string.Empty);

            builder.Property(x => x.LinkedinUrl)
              .HasColumnName("LINKEDIN_URL")
              .HasColumnType(EntityFrameworkCoreConstants.GetColumnTypeNVarChar(100))
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

            var datas = new List<GeneralInfo>();

            datas.Add(new GeneralInfo
            {
                Id = 1
            });

            builder.HasData(datas);
        }
    }
}
