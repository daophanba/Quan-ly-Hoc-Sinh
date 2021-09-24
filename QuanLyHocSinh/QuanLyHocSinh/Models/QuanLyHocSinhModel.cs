using QuanLyLopHoc.QuanLyLopHocDB.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyHocSinh.Models
{
    public class QuanLyHocSinhModel
    {
        public class Input_Index
        {
            public int PageNow { get; set; } = 1;
            public int PageSize { get; set; } = 6;
            public int PageTotalRow { get; set; }
            public int PageMin { get; set; } = 1;
            public int PageMax { get; set; }
            public List<LopHoc> LopHoc { get; set; }
            public List<HocSinh> HocSinh { get; set; }
            public int IDLopHoc { get; set; }
            public string TenLopHoc { get; set; }


        }

        public class ThemThongTin
        {
            public string MaLop { get; set; }
            public string TenLop { get; set; }
            public int SiSo { get; set; }
            public string GiaoVien { get; set; }
            public int ID { get; set; }

        }
        public class ThongBaoLoi
        {
            public bool KtMaLop { get; set; } = false;
            public bool KtTenLop { get; set; } = false;
            public bool KtGiaoVien { get; set; } = false;
            public bool KiemTraRong { get; set; } = false;
            public bool KtMaLopTrungLap { get; set; } = false;
            public bool Start { get; set; } = false;

        }

        public class ThongTinHocSinh
        {
            public string HoVaTen { get; set; }
            public string NgaySinh { get; set; }
            public string GioiTinh { get; set; }
            public string QueQuan { get; set; }
            public int ID { get; set; }
            public int IDLopHoc { get; set; }
        }
    }
}
