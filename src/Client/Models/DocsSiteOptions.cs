using System.ComponentModel.DataAnnotations;

namespace Steeltoe.Client.Models
{
    public class DocsSiteOptions
    {
        [Required]
        public string BaseAddress { get; set; }

        public string ApiBrowserHome { get; set; }
        public string BlogHome { get; set; }
        public string DocsHome { get; set; }
        public string DocsStreamHome { get; set; }
        public string GuidesHome { get; set; }
        public string FileShareHome { get; set; }

        public void SetUrls()
        {
            ApiBrowserHome = ConfigureIfMissing(ApiBrowserHome, $"{BaseAddress}/api/browser/v3/all");
            BlogHome = ConfigureIfMissing(BlogHome, $"{BaseAddress}/articles/");
            DocsHome = ConfigureIfMissing(DocsHome, $"{BaseAddress}/api/v3/welcome/");
            DocsStreamHome = ConfigureIfMissing(DocsStreamHome, $"{BaseAddress}/api/v3/stream/");
            GuidesHome = ConfigureIfMissing(GuidesHome, $"{BaseAddress}/guides/");
            FileShareHome = ConfigureIfMissing(FileShareHome, $"{BaseAddress}/api/v3/fileshares/");
        }

        private static string ConfigureIfMissing(string value, string defaultValue)
        {
            return !string.IsNullOrWhiteSpace(value) ?
                value : defaultValue;
        }
    }
}
