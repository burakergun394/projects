namespace PusulaGroup.WebApp.Application.Dtos
{
    public class ServiceDetailDto
    {
        public string Name { get; set; }
        public string UserFriendlyName { get; set; }
        public string Content { get; set; }
        public List<ServiceImageDto> Images { get; set; }
      
    }

    public class ServiceImageDto
    {
        public string Path { get; set; }
        public bool IsMain { get; set; }
    }

}
