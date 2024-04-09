using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
namespace EcommerceMVC.ViewModel
{
    public class RegisterVM
    {
        [Display(Name ="Username")]
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "*")]
        [MaxLength(20, ErrorMessage = "maximum about 20 character")]
        public string MaKh { get; set; } = null!;
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "*")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string? MatKhau { get; set; }
        [Display(Name = "Fullname")]
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "*")]
        [MaxLength(50,ErrorMessage = "maximum about 50 character")]
        public string HoTen { get; set; } = null!;
        public bool GioiTinh { get; set; } = true;
        [Display(Name = "BirthDay")]
        [DataType(DataType.Date)]
        public DateTime? NgaySinh { get; set; }
        [MaxLength(50, ErrorMessage = "maximum about 60 character")]
        public string? DiaChi { get; set; }
        [MaxLength(50, ErrorMessage = "maximum about 24 character")]
        [RegularExpression(@"0[93]\d{8}",ErrorMessage = "your phone is not as VietNam format")]
        public string? DienThoai { get; set; }
        [EmailAddress(ErrorMessage = "Format is incorrected")]
        public string Email { get; set; } = null!;

        public string? Hinh { get; set; }
    }
}
