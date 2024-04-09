using AutoMapper;
using EcommerceMVC.Data;
using EcommerceMVC.Helper;
using EcommerceMVC.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using System.Security.Claims;

namespace EcommerceMVC.Controllers
{
    public class KhachHangController : Controller
    {
        private readonly Hshop2023Context _context;
        private readonly IMapper mapper;

        public KhachHangController(Hshop2023Context context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }
        [HttpGet]
        public IActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public IActionResult DangKy(RegisterVM model, IFormFile Hinh)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var khachHang = mapper.Map<KhachHang>(model);
                    khachHang.RandomKey = Util.GenerateRamdomKey();
                    khachHang.MatKhau = model.MatKhau.ToMd5Hash(khachHang.RandomKey);
                    khachHang.HieuLuc = true;
                    khachHang.VaiTro = 0;
                    if (Hinh != null)
                    {
                        khachHang.Hinh = Util.UpLoadHinh(Hinh, "KhachHang");
                    }
                    _context.Add(khachHang);
                    _context.SaveChanges();
                    return RedirectToAction("Index", "HangHoa");
                } catch (Exception ex)
                {

                }
                
            }
            return View();
        }
        [HttpGet]
        public IActionResult Login(string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model, string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            if(ModelState.IsValid)
            {
                var khachHang = _context.KhachHangs.SingleOrDefault(kh => kh.MaKh == model.UserName);
                if(khachHang == null)
                {
                    ModelState.AddModelError("Error", "NotContain");
                } else
                {
                    if(!khachHang.HieuLuc)
                    {
                        ModelState.AddModelError("loi", "Account Locked");
                    } else
                    {
                        if(khachHang.MatKhau != model.Password.ToMd5Hash(khachHang.RandomKey))
                        {
                            ModelState.AddModelError("loi", "password or username is Incorrected ");
                        } else
                        {
                            var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Email,khachHang.Email),
                                new Claim(ClaimTypes.Name,khachHang.HoTen),
                                new Claim("CustomerID",khachHang.MaKh),
                                new Claim(ClaimTypes.Role,"Customer")
                            };
                            var claimsIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
                            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                            await HttpContext.SignInAsync(claimsPrincipal);
                            if (Url.IsLocalUrl(ReturnUrl))
                            {
                                return Redirect(ReturnUrl);
                            } else
                            {
                                return Redirect("/");
                            }
                        }
                    }
                }
            }
            return View();
        }
        [Authorize]
        public IActionResult Profile()
        {
            return View();
        }
        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
