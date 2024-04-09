using EcommerceMVC.Data;
using EcommerceMVC.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceMVC.ViewComponents
{
    public class TypeMenuViewComponent : ViewComponent
    {
        private readonly Hshop2023Context _context;
        public TypeMenuViewComponent(Hshop2023Context context) => _context = context;
        public IViewComponentResult Invoke()
        {
            var data = _context.Loais.Select(lo => new MenuLoaiVM
            {
                MaLoai = lo.MaLoai,
                TenLoai = lo.TenLoai,
                SoLuong = lo.HangHoas.Count
            }).OrderBy(p => p.TenLoai);
            return View(data);
        }
    }
}
