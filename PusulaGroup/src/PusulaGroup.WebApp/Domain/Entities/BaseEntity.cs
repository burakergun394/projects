namespace PusulaGroup.WebApp.Domain.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
    }
    public class Tour : BaseEntity
    {
        public string Name { get; set; }
        public string UserFriendlyName { get; set; }
        public string Content { get; set; }
        public int ParentId { get; set; }
        public bool IsShowSlider { get; set; }
        public string SliderText { get; set; }
        public int SliderOrderIndex { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public string SeoAuthor { get; set; }
    }

    public class TourImage : BaseEntity
    {
        public int TourId { get; set; }
        public string Path { get; set; }
        public bool IsMain { get; set; }
    }

    public class Service : BaseEntity
    {
        public string Name { get; set; }
        public string UserFriendlyName { get; set; }
        public string Content { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public string SeoAuthor { get; set; }
    }

    public class ServiceImage : BaseEntity
    {
        public int ServiceId { get; set; }
        public string Path { get; set; }
        public bool IsMain { get; set; }
    }

    public class AboutUs: BaseEntity
    {
        public string AboutUsContent { get; set; }
        public string OurVisionContent { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public string SeoAuthor { get; set; }
        public string ImagePath { get; set; }
    }

    public class GeneralInfo : BaseEntity
    {
        public string LogoPath { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string TelephoneNumber { get; set; }
        public string Email { get; set; }
        public string EMailHost { get; set; }
        public string EmailFrom { get; set; }
        public string EmailPassword { get; set; }
        public int EmailPort { get; set; }
        public string WhatsappTelephoneNumber { get; set; }
        public string FacebookUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string LinkedinUrl { get; set; }
        public string InstagramUrl { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public string SeoAuthor { get; set; }
    }

    public class OurCustomer: BaseEntity
    {
        public string Content { get; set; }
        public string Header { get; set; }
        public string SubHeader { get; set; }
        public string ImagePath { get; set; }
    }
}
