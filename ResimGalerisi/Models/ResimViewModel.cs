using ResimGalerisi.Attributes;
using ResimGalerisi.Classes;
using System.ComponentModel.DataAnnotations;

namespace ResimGalerisi.Models
{
    public class ResimViewModel
    {
        [MaxLength(100,ErrorMessage ="{0} en fazla {1} karakterden oluşabilir")]
        [Display(Name ="Başlık")]
        [Required(ErrorMessage ="{0} alanı zorunludur")]//0 dsiplayname 1 maxlength gibi karsılıkları var
        public string Baslik { get; set; }=string.Empty;
        //[GecerliResim(MaksimumDosyaBoyutuMb=1)]
        [GecerliResim(MaksimumDosyaBoyutuMb =1)]
        [Display(Name ="Resim")]
        [Required(ErrorMessage ="{0} alanı zorunludur")]
        public IFormFile Dosya { get; set; } = null!;
        public List<Resim> Resims { get; set; } = new();
    }
}
