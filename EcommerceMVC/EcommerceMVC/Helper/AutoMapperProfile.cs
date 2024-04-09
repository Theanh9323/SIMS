using AutoMapper;
using EcommerceMVC.Data;
using EcommerceMVC.ViewModel;

namespace EcommerceMVC.Helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterVM, KhachHang>();/*.ForMember(kh => kh.HoTen, option => option.MapFrom(RegisterVM => RegisterVM.HoTen)).ReverseMap ;*/
        }
    }
}
