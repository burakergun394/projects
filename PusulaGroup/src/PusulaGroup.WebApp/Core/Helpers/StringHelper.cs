using System.Text;

namespace PusulaGroup.WebApp.Core.Helpers
{
    public class StringHelper
    {
        public string TurkishCharacterToEnglish(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;

            StringBuilder builder = new(value);
            builder.Replace(" ", "-");
            builder.Replace("ş", "s");
            builder.Replace("ı", "i");
            builder.Replace("ö", "o");
            builder.Replace("ü", "u");
            builder.Replace("ç", "c");
            builder.Replace("ğ", "g");
            return builder.ToString().ToLower();
        }
    }
}
