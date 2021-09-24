using Microsoft.AspNetCore.Mvc;
using QuanLyLopHoc.QuanLyLopHocDB.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuanLyHocSinh.Models;
using QuanLyLopHoc.QuanLyLopHocDB;
using System.Text.RegularExpressions;
using QuanLyLopHoc.Helpers;
using static QuanLyHocSinh.Models.QuanLyHocSinhModel;
using Microsoft.Extensions.Logging;

namespace QuanLyHocSinh.Controllers
{
    public class QuanLyHocSinhController : Controller
    {

        QuanLyLopHocContext db = new QuanLyLopHocContext();
        QuanLyLopHocContext db1 = new QuanLyLopHocContext();
        bool KiemTraKyTuTen(string HKt_KyTu)
        {
            bool Ho_Kt_KyTu_1 = Regex.IsMatch(HKt_KyTu, @"[^aAàÀảẢãÃáÁạẠăĂằẰẳẲẵẴắẮặẶâÂầẦẩẨẫẪấẤậẬbBcCdDđĐeEèÈẻẺẽẼé
                                                                    ÉẹẸêÊềỀểỂễỄếẾệỆfFgGhHiIìÌỉỈĩĨíÍịỊjJkKlLmMnNoOòÒỏỎõÕóÓọỌôÔồỒổỔỗỖốỐộỘơƠờỜởỞỡỠớỚợỢpPqQrRsStTu
                                                                    UùÙủỦũŨúÚụỤưƯừỪửỬữỮứỨựỰvVwWxXyYỳỲỷỶỹỸýÝỵỴzZ][^\x20{5}]");
            if (Ho_Kt_KyTu_1 == true && HKt_KyTu.Length > 20)
            {
                return false;
            }
            else return true;
        }
        bool KiemTraTenLop(string HKt_KyTu)
        {
            bool Ho_Kt_KyTu_1 = Regex.IsMatch(HKt_KyTu, @"[^aAàÀảẢãÃáÁạẠăĂằẰẳẲẵẴắẮặẶâÂầẦẩẨẫẪấẤậẬbBcCdDđĐeEèÈẻẺẽẼé
                                                                    ÉẹẸêÊềỀểỂễỄếẾệỆfFgGhHiIìÌỉỈĩĨíÍịỊjJkKlLmMnNoOòÒỏỎõÕóÓọỌôÔồỒổỔỗỖốỐộỘơƠờỜởỞỡỠớỚợỢpPqQrRsStTu
                                                                    UùÙủỦũŨúÚụỤưƯừỪửỬữỮứỨựỰvVwWxXyYỳỲỷỶỹỸýÝỵỴzZ1234567890][^\x20{5}]");
            if (Ho_Kt_KyTu_1 == true && HKt_KyTu.Length > 20)
            {
                return false;
            }
            else return true;
        }
        bool MaDinhDanh(string HKt_KyTu)
        {
            bool Ho_Kt_KyTu_1 = Regex.IsMatch(HKt_KyTu, @"[^AQWERTYUIOPSDFGHJKLZXCVBNM1234567890-]");
            if (Ho_Kt_KyTu_1 == true && HKt_KyTu.Length > 7)
            {
                return false;
            }
            else return true;
        }

        bool KiemTraNgay(string Ngay)
        {
            DateTime NgayChuyenDoi = new DateTime();
            return (!DateTime.TryParse(Ngay, out NgayChuyenDoi));
        }
        bool KiemTraGioiTinh(string GioiTinh)
        {
            if (GioiTinh == "Nam" || GioiTinh == "Nữ")
            {
                return false;
            }
            else return true;
        }
        public IActionResult LopHoc(Models.QuanLyHocSinhModel.Input_Index input_Index)
        {
            LopHoc TongSiSoLopHoc = new LopHoc();
            var qr = db.LopHocs.AsQueryable();
            qr = qr.Where(x => x.IsDeleted == false);
            //var HocSinhKT = db1.HocSinhs.AsQueryable();

            //HocSinhKT = HocSinhKT.Where(x => x.IsDeleted == false);

            //foreach (var i in qr)
            //{
            //    var SuaSiSo = db1.LopHocs.Where(x => x.ID == i.ID).FirstOrDefault();
            //    SuaSiSo.SiSo = HocSinhKT.Where(x => x.IDLopHoc == i.ID).Count();
            //    db1.SaveChanges();
            //}
            input_Index.PageTotalRow = qr.Count();

            input_Index.PageMax = (int)Math.Ceiling((decimal)(input_Index.PageTotalRow) / (decimal)(input_Index.PageSize));
            if (input_Index.PageMax == 0)
            {
                input_Index.PageMax = 1;
            }
            input_Index.PageMin = 1;
            input_Index.LopHoc = qr.Skip((input_Index.PageNow - 1) * input_Index.PageSize)
                                 .Take(input_Index.PageSize)
                                 .ToList();

            return View(input_Index);
        }
        [HttpPost]
        public IActionResult ThemThongTinLop(ThemThongTin ThongTinThem)
        {
            ResultHepper Result = new ResultHepper();

            try
            {
                if (ThongTinThem.GiaoVien == null || ThongTinThem.TenLop == null || ThongTinThem.MaLop == null)
                {
                    Result.message = "Bạn để trống dữ liệu 1 hoặc nhiều biểu mẫu nhập";
                    return Json(Result);
                }
                if (KiemTraKyTuTen(ThongTinThem.GiaoVien) == false)
                {
                    Result.message = "Tên giáo viên viết sai";
                    return Json(Result);
                }
                if (KiemTraTenLop(ThongTinThem.TenLop) == false)
                {
                    Result.message = "Tên lớp chỉ chứa chữ và số";
                    return Json(Result);
                }
                if (MaDinhDanh(ThongTinThem.MaLop) == false)
                {
                    Result.message = @"Mã lớp chỉ chứa chữ in hoa, "" - "", số và dài tối đa 7 ký tự";
                    return Json(Result);
                }
                var KtMaDinhDanh = db.LopHocs
                    .Where(x => x.MaDinhDanhLop == ThongTinThem.MaLop)
                    .ToList();
                if (db.LopHocs.Any(x => x.MaDinhDanhLop == ThongTinThem.MaLop))
                {
                    Result.message = "Mã lớp đã tồn tại";
                    return Json(Result);
                }

                LopHoc LopHocThem = new LopHoc()
                {
                    MaDinhDanhLop = ThongTinThem.MaLop,
                    TenLopHoc = ThongTinThem.TenLop,
                    GiaoVienChuNhiem = ThongTinThem.GiaoVien,
                    CreateDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                };
                db.LopHocs.Add(LopHocThem);
                db.SaveChanges();
                Result.start = true;
                return Json(Result);


            }
            catch (Exception ex)
            {
                Result.message = "Thêm lớp học thất bại";
                return Json(Result);
            }
            return Json(Result);
        }
        public IActionResult SuaThongTinLop(Models.QuanLyHocSinhModel.ThemThongTin ThongTinSua)
        {
            ResultHepper Result = new ResultHepper();
            if (KiemTraKyTuTen(ThongTinSua.GiaoVien) && KiemTraTenLop(ThongTinSua.TenLop) && MaDinhDanh(ThongTinSua.MaLop))
            {
                LopHoc SuaThongTins = db.LopHocs
                                        .Where(x => x.ID == ThongTinSua.ID)
                                        .FirstOrDefault();
                if (SuaThongTins == null) return Json(false);
                if (SuaThongTins.MaDinhDanhLop != ThongTinSua.MaLop)
                {
                    var SuaThongTinMaDinhDanhs = db.LopHocs.Where(x => x.MaDinhDanhLop == ThongTinSua.MaLop).ToList();
                    if (SuaThongTinMaDinhDanhs.Count() == 0)
                    {
                        SuaThongTins.MaDinhDanhLop = ThongTinSua.MaLop;
                    }
                    else
                    {
                        return Json(Result);
                    }
                }
                SuaThongTins.TenLopHoc = ThongTinSua.TenLop;
                SuaThongTins.GiaoVienChuNhiem = ThongTinSua.GiaoVien;

                db.SaveChanges();
                Result.start = true;
                return Json(Result);
            }

            else
            {
                Result.message = "Sửa thông tin lớp học thất bại";
                return Json(Result);
            }

        }
        public IActionResult XoaThongTinLop(Models.QuanLyHocSinhModel.ThemThongTin ThongTinXoa)
        {
            ResultHepper Result = new ResultHepper();
            LopHoc XoaThongTins = db.LopHocs.Where(x => x.ID == ThongTinXoa.ID).FirstOrDefault();
            if (XoaThongTins == null)
            {
                Result.message = "Không tìm thấy thông tin cần xoá";
                return Json(Result);
            }
            XoaThongTins.IsDeleted = true;
            db.SaveChanges();
            var XoaHocSinhMotLop = db.HocSinhs.Where(x => x.IDLopHoc == ThongTinXoa.ID).ToList();

            foreach (var x in XoaHocSinhMotLop)
            {
                x.IsDeleted = true;
            }
            db.SaveChanges();
            Result.start = true;
            return Json(Result);
        }




        public IActionResult HocSinh(Models.QuanLyHocSinhModel.Input_Index Trang_HocSinh)
        {

            if (!db.LopHocs.Any(x => x.ID == Trang_HocSinh.IDLopHoc && x.IsDeleted == false))
            {
                return View();  // chưa biết báo lỗi như thế nào
            }


            var HocSinhKT = db.HocSinhs.AsQueryable();

            HocSinhKT = HocSinhKT.Where(x => x.IsDeleted == false && x.IDLopHoc == Trang_HocSinh.IDLopHoc);

            Trang_HocSinh.PageTotalRow = HocSinhKT.Count();



            Trang_HocSinh.PageMax = (int)Math.Ceiling((decimal)(Trang_HocSinh.PageTotalRow) / (decimal)(Trang_HocSinh.PageSize));
            if (Trang_HocSinh.PageMax == 0)
            {
                Trang_HocSinh.PageMax = 1;
            }
            Trang_HocSinh.PageMin = 1;
            if (Trang_HocSinh.PageNow == 0) 
            {
                Trang_HocSinh.PageNow = Trang_HocSinh.PageMax;
            }
            Trang_HocSinh.HocSinh = HocSinhKT.Skip((Trang_HocSinh.PageNow - 1) * Trang_HocSinh.PageSize)
                                 .Take(Trang_HocSinh.PageSize)
                                 .ToList();
            return View(Trang_HocSinh);
        }
        public IActionResult ThemThongTinHocSinh(Models.QuanLyHocSinhModel.ThongTinHocSinh ThemHocSinh)
        {
            ResultHepper Result = new ResultHepper();
            if (ThemHocSinh.HoVaTen == null || ThemHocSinh.NgaySinh == null
              || ThemHocSinh.GioiTinh == null || ThemHocSinh.QueQuan == null)
            {
                Result.message = "Bạn đang để rỗng 1 hoặc nhiều dữ liệu";
                return Json(Result);
            }
            if (!KiemTraKyTuTen(ThemHocSinh.HoVaTen))
            {
                Result.message = "Ký tự tên nhập sai";
                return Json(Result);
            }
            if (KiemTraNgay(ThemHocSinh.NgaySinh))
            {
                Result.message = "Ngày bạn nhập sai";
                return Json(Result);
            }
            if (KiemTraGioiTinh(ThemHocSinh.GioiTinh))
            {
                Result.message = "Giới tính nhập sai";
                return Json(Result);
            }


            HocSinh HocSinh = new HocSinh()
            {
                IDLopHoc = ThemHocSinh.IDLopHoc,
                HoVaTen = ThemHocSinh.HoVaTen,
                GioiTinh = ThemHocSinh.GioiTinh,
                NgaySinh = DateTime.Parse(ThemHocSinh.NgaySinh),
                QueQuan = ThemHocSinh.QueQuan,
                CreateDate = DateTime.Now,
                ModifiedDate = DateTime.Now,

            };
            db.HocSinhs.Add(HocSinh);
            db.SaveChanges();
            LopHoc lopHoc = db.LopHocs
                              .Where(x => x.ID == ThemHocSinh.IDLopHoc)
                              .FirstOrDefault();
            int DemSiSo = db.HocSinhs
                            .Where(x => x.IDLopHoc == ThemHocSinh.IDLopHoc && x.IsDeleted == false)
                            .Count();
            lopHoc.SiSo = DemSiSo;
            db.SaveChanges();
            Result.start = true;
            return Json(Result);
        }
        public IActionResult SuaThongTinHocSinh(Models.QuanLyHocSinhModel.ThongTinHocSinh SuaThongTin)
        {
            ResultHepper Result = new ResultHepper();
            if (SuaThongTin.HoVaTen == null || SuaThongTin.NgaySinh == null
                || SuaThongTin.GioiTinh == null || SuaThongTin.QueQuan == null)
            {
                Result.message = "Bạn đang để rỗng 1 hoặc nhiều dữ liệu";
                return Json(Result);
            }
            if (!KiemTraKyTuTen(SuaThongTin.HoVaTen))
            {
                Result.message = "Ký tự tên nhập sai";
                return Json(Result);
            }
            if (KiemTraNgay(SuaThongTin.NgaySinh))
            {
                Result.message = "Ngày bạn nhập sai";
                return Json(Result);
            }
            if (KiemTraGioiTinh(SuaThongTin.GioiTinh))
            {
                Result.message = "Giới tính nhập sai";
                return Json(Result);
            }

            if (!db.HocSinhs.Any(x => x.ID == SuaThongTin.ID))
            {
                Result.message = "Không tìm thấy học sinh mà bạn sửa";
                return Json(Result);
            }
            var SuaHocSinhs = db.HocSinhs.Where(x => x.ID == SuaThongTin.ID)
                                .FirstOrDefault();
            SuaHocSinhs.HoVaTen = SuaThongTin.HoVaTen;
            SuaHocSinhs.NgaySinh = DateTime.Parse(SuaThongTin.NgaySinh);
            SuaHocSinhs.GioiTinh = SuaThongTin.GioiTinh;
            SuaThongTin.QueQuan = SuaThongTin.QueQuan;
            db.SaveChanges();
            Result.start = true;
            return Json(Result);
        }
        public IActionResult XoaThongTinHocSinh(Models.QuanLyHocSinhModel.ThongTinHocSinh XoaThongTin)
        {
            ResultHepper Result = new ResultHepper();
            HocSinh XoaHocSinh = db.HocSinhs
                                   .Where(x => x.ID == XoaThongTin.ID)
                                   .FirstOrDefault();
            XoaHocSinh.IsDeleted = true;
            XoaHocSinh.DeletedDate = DateTime.Now;
            db.SaveChanges();
            int DemSiSo = db.HocSinhs
                      .Where(x => x.IDLopHoc == XoaThongTin.IDLopHoc && x.IsDeleted == false)
                      .Count();
            //db.SaveChanges();
            LopHoc XoaHocSinhLopHoc = db.LopHocs
                                        .Where(x => x.ID == XoaThongTin.IDLopHoc)
                                        .FirstOrDefault();
            XoaHocSinhLopHoc.SiSo = DemSiSo;
            db.SaveChanges();
            Result.start = true;
            return Json(Result);
        }

    }
}
