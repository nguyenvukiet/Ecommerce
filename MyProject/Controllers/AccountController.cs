using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyProject.Models;

namespace MyProject.Controllers
{
    public class AccountController : Controller
    {
        //Tạo đối tượng db chứa dữ liệu từ model dbMyProject đã tạo:
        //SalesOnlineEntities3 db = new SalesOnlineEntities3();
        SalesOnlineDataContext db = new SalesOnlineDataContext();
        // GET: Nguoidung
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Dangky()
        {
            return View();
        }
        //POST: Hàm Dangky(Post) Nhận dữ liệu từ trang Dangky và thực hiện việc tạo mới dữ liệu 
        [HttpPost]
        public ActionResult Dangky(FormCollection collection, taiKhoanTV kh)
        {
            //Gán các giá trị người dùng nhập liệu cho các biến 
            var hoten = collection["HotenKH"];
            var tendn = collection["TenDN"];
            var matkhau = collection["Matkhau"];
            var matkhaunhaplai = collection["Matkhaunhaplai"];
            var diachi = collection["Diachi"];
            var email = collection["Email"];
            var dienthoai = collection["Dienthoai"];
            var ngaysinh = String.Format("{0:MM/dd/yyy}", collection["Ngaysinh"]);
            if (String.IsNullOrEmpty(hoten))
            {
                ViewData["Loi1"] = "Họ tên khách hàng hoặc mật khẩu không được để trống không được để trống";
            }
            else if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi2"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi3"] = "Phải nhập mật khẩu";
            }
            else if (String.IsNullOrEmpty(matkhaunhaplai))
            {
                ViewData["Loi4"] = "phải nhập lại mật khẩu";
            }
            else if (String.IsNullOrEmpty(email))
            {
                ViewData["Loi5"] = "Email không được để trống";
            }
            else if (String.IsNullOrEmpty(dienthoai))
            {
                ViewData["Loi5"] = "Phải nhập điện thoại";
            }
            else
            {
                //gán giá trị cho đối tượng được tạo mới (kh)
                kh.tenTV = hoten;
                kh.taiKhoan = tendn;
                kh.matKhau = matkhau;
                kh.email = email;
                kh.diaChi = diachi;
                kh.soDT = dienthoai;
                kh.ngaysinh = DateTime.Parse(ngaysinh);
                db.taiKhoanTVs.InsertOnSubmit(kh);
                db.SubmitChanges();
                return RedirectToAction("Dangnhap");
            }
            return this.Dangky();
        }
        [HttpGet]
        public ActionResult Dangnhap()
        {
            return View();
        }
        public ActionResult Dangnhap(FormCollection collection)
        {
            // gán các giá trị người dùng nhập liệu cho các biến
            var tendn = collection["TenDN"];
            var matkhau = collection["MatKhau"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                //gán giá trị cho đối tượng được tạo mới (kh)
                taiKhoanTV kh = db.taiKhoanTVs.SingleOrDefault(n => n.taiKhoan == tendn && n.matKhau == matkhau);
                if (kh != null)
                {
                    ViewBag.Thongbao = "Đăng nhập thành công";
                    Session["taiKhoan"] = kh;
                }
                else
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }
    }
}