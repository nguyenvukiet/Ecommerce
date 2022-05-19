using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyProject.Models
{
    public class GioHang
    {
        SalesOnlineDataContext db  = new SalesOnlineDataContext();
        public string iMaSP { set; get; }
        public String sTenSP { set; get; }
        public String sHinhDD { set; get; }
        public Double dDongia { set; get; }

        public int iSoluong { set; get; }
        public Double dThanhtien
        {
            get { return iSoluong * dDongia; }
        }
        //khởi tạo giỏ hàng theo maSP được truyền vào với Soluong mặc định là 1
        public GioHang(String maSP)
        {
            iMaSP = maSP;
            sanPham sp = db.sanPhams.Single(n => n.maSP == iMaSP);
            sTenSP = sp.tenSP;
            sHinhDD = sp.hinhDD;
            dDongia = double.Parse(sp.giaBan.ToString());
            iSoluong = 1;
        }
    }
}