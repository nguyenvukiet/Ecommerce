using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyProject.Models
{
    public class Commoninfo
    {
        private SalesOnlineDataContext db;
        public Commoninfo()
        {
            this.db = new SalesOnlineDataContext();
        }
        public IEnumerable<loaiSP> NhomHang
        {
            get
            {
                return this.db.loaiSPs;
            }
        }
        public List<string> NhaSanXuat
        {
            get
            {
                List<string> kq = new List<string>();
                foreach (sanPham i in this.db.sanPhams)
                    if (!kq.Contains(i.nhaSanXuat.Trim()))
                        kq.Add(i.nhaSanXuat.Trim());
                return kq;
            }
        }
        public IEnumerable<sanPham> SanPhamMoi(int n)
        {
            return db.sanPhams.OrderByDescending(x => x.ngayDang).Take(n);
        }
    }
}