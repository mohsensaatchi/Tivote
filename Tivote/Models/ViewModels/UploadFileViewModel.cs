using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Tivote.Models.ViewModels
{
    public class UploadFileViewModel
    {
        [Display(Name = "فایل")]
        public IFormFile File { get; set; }
    }
}
