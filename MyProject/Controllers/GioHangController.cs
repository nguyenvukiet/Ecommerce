using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyProject.Models;

namespace MyProject.Controllers
{
    public class GioHangController : Controller
    {
        // tạo đối tượng data chứa dữ liệu từ model dbUstoraShop đã tạo 
        SalesOnlineDataContext db = new SalesOnlineDataContext();

        //Xây dựng trang giỏ hàng
        // GET: GioHang
        public ActionResult GioHang()
        {
            List<GioHang> lstGiohang = Laygiohang();
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("Index", "SalesOnline");
            }
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return View(lstGiohang);
        }


        //Tạo Partial view để hiển thị thông tin giỏ hàng
        public ActionResult GiohangPartial()
        {
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return PartialView();
        }


        //lấy giỏ hàng
        public List<GioHang> Laygiohang()
        {
            List<GioHang> lstGiohang = Session["GioHang"] as List<GioHang>;
            if (lstGiohang == null)
            {
                //Nếu giỏ hàng chưa tồn tại thì khởi tạo listGiohang
                lstGiohang = new List<GioHang>();
                Session["Giohang"] = lstGiohang;
            }
            return lstGiohang;
        }


        // thêm hàng vào giỏ 
        public ActionResult ThemGiohang(string iMaSP, string strURL)
        {
            //Lấy ra Session giỏ hàng 
            List<GioHang> lstGiohang = Laygiohang();
            //Kiểm tra sách này tồn tại trong Session["GioHang"] chưa ?
            GioHang sanpham = lstGiohang.Find(n => n.iMaSP == iMaSP);
            if (sanpham == null)
            {
                sanpham = new GioHang(iMaSP);
                lstGiohang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSoluong++;
                return Redirect(strURL);
            }
        }


        //Tổng số lượng 
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> lstGiohang = Session["GioHang"] as List<GioHang>;
            if (lstGiohang != null)
            {
                iTongSoLuong = lstGiohang.Sum(n => n.iSoluong);
            }
            return iTongSoLuong;
        }


        //Tính tổng tiền 
        private double TongTien()
        {
            double iTongTien = 0;
            List<GioHang> lstGiohang = Session["GioHang"] as List<GioHang>;
            if (lstGiohang != null)
            {
                iTongTien = lstGiohang.Sum(n => n.dThanhtien);
            }
            return iTongTien;
        }


        //Xóa giỏ hàng
        public ActionResult XoaGiohang(string iMaSP)
        {
            //Lấy giỏ hàng từ Session
            List<GioHang> lstGiohang = Laygiohang();
            //Kiểm tra sách đã có trong Session["GioHang"]
            GioHang sanpham = lstGiohang.SingleOrDefault(n => n.iMaSP == iMaSP);
            //Nếu tồn tại thì cho sửa Soluong
            if (sanpham != null)
            {
                lstGiohang.RemoveAll(n => n.iMaSP == iMaSP);
                return RedirectToAction("GioHang");
            }
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("Index", "SalesOnline");
            }
            return RedirectToAction("GioHang");
        }


        //Xóa tất cả hàng trong giỏ
        public ActionResult XoaTatcaGiohang()
        {
            //Lấy giỏ hàng từ Session
            List<GioHang> lstGiohang = Laygiohang();
            lstGiohang.Clear();
            return RedirectToAction("Index", "SalesOnline");
        }


        //Cập nhật giỏ hàng 
        public ActionResult CapnhatGiohang(string iMaSP, FormCollection f)
        {
            sanPham sp = db.sanPhams.SingleOrDefault(n => n.maSP == iMaSP);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Lấy giỏ hàng từ Session 
            List<GioHang> lstGiohang = Laygiohang();
            //Kiểm tra sách đã có trong Session["GioHang"]
            GioHang sanpham = lstGiohang.SingleOrDefault(n => n.iMaSP == iMaSP);
            //Nếu tồn tại thì cho sửa Soluong
            if (sanpham != null)
            {
                sanpham.iSoluong = int.Parse(f["txtSoluong"].ToString());
            }
            return RedirectToAction("GioHang");
        }


        ////Xây dựng một view cho người dung chỉnh sửa giỏ hàng
        //public ActionResult SuaGiohang()
        //{
        //    if (Session["GioHang"] == null)
        //    {
        //        return RedirectToAction("Index", "Shop");
        //    }
        //    List<GioHang> lstGiohang = Laygiohang();
        //    return View(lstGiohang);
        //}

    }
}
    