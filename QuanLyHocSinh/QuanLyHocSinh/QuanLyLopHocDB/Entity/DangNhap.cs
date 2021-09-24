using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyHocSinh.QuanLyLopHocDB.Entity
{
    public class DangNhap
    {

        public int ID { get; set; }
        public string Ten { get; set; }
        public string MatKhau { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
