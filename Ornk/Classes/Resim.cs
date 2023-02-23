using System.ComponentModel.DataAnnotations;

namespace Ornk.Classes
{
    public class Resim
    {
        public IFormFile? Resims { get; set; }   
        [MaxLength(255)]
        public string DosyaAd { get; set; } = string.Empty;

    }
}
