namespace PusulaGroup.Domain.Entities;

public class Tour: BaseEntity
{
    public string Name { get; set; }
    public string UserFriendlyName { get; set; }
    public string Content { get; set; }
    public int ParentId { get; set; }
    public string SeoTitle { get; set; }
    public string SeoDescription { get; set; }
}

public class TourImage: BaseEntity
{
    public int TourId { get; set; }
    public string Path { get; set; }
    public bool IsMain { get; set; }
}

public class Service: BaseEntity
{
    public string Name { get; set; }
    public string UserFriendlyName { get; set; }
    public string Content { get; set; }
}

public class ServiceImage : BaseEntity
{
    public int ServiceId { get; set; }
    public string Path { get; set; }
    public bool IsMain { get; set; }
}