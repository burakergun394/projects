namespace PusulaGroup.WebApp.Application.Dtos
{
    public class GetListTourDetailDto
    {
        public string Name { get; set; }
        public string UserFriendlyName { get; set; }
        public string ImagePath { get; set; }
    }

    public class GetTourDetailDto
    {
        public string Name { get; }
        public string UserFriendlyName { get; set; }
        public string Content { get; set; }
        public bool IsMainTour { get; set; }
        public List<TourDetailDto> SubTours { get; set; }
        public List<string> TourImages { get; set; }
    }

    public class TourDetailDto
    {
        public string Name { get; set; }
        public string UserFriendlyName { get; set; }
        public string ImagePath { get; set; }
    }

    public class TourImageDto
    {
        public string Path { get; set; }
    }

    public class AdminGetAllTourDetail
    {
        public int TourId { get; set; }
        public string UserFriendlyName { get; set; }
        public string MainTourName { get; set; }
    }
}
