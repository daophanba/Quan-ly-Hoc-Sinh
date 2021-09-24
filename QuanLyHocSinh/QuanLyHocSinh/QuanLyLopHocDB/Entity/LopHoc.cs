using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyLopHoc.QuanLyLopHocDB.Entity
{
    public class LopHoc
    {
        public int ID { get; set; }
        public string MaDinhDanhLop { get; set; }
        public string TenLopHoc { get; set; }
        public int SiSo { get; set; }
        public string GiaoVienChuNhiem { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}
