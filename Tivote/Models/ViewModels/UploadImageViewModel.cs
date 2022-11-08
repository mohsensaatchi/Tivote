using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Tivote.Models.ViewModels
{
    public class UploadImageViewModel
    {
        [Display(Name = "Picture")]
        public IFormFile? Image { get; set; }
    }
}
