using Microsoft.EntityFrameworkCore;
using MyApiSinhVien.Data;
using MyApiSinhVien.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace MyApiSinhVien.Service.SinhVien
{
    public class SinhVienService : ISinhVienService
    {
        private readonly MyDbContext _context;
        public static int PAGE_SIZE { get; set; } = 2;
        public SinhVienService(MyDbContext context)
        {
            _context = context;
        }
       
        public void AddSinhVien(SinhVienModel sv)
        {
            if (String.IsNullOrWhiteSpace(sv.HoTen) || sv.HoTen.Length > 100)
            {
                throw new Exception("Họ tên không hợp lệ.");
            }
            DateTime now = DateTime.Now;
            if (sv.NgaySinh != null && sv.NgaySinh > now)
            {
                throw new Exception("Ngày sinh của sinh viên không được lớn hơn ngày tháng hiện tại.");
            }
            if (String.IsNullOrWhiteSpace(sv.GioiTinh) || (sv.GioiTinh!="Nam" && sv.GioiTinh!="Nữ"))
            {
                throw new Exception("Giới tính phải là Nam hoặc Nữ.");
            }
            if (_context.Khoas.SingleOrDefault(x => x.MaKhoa.Equals(sv.MaKhoa)) == null)
            {
                throw new Exception("Không tồn tại mã khoa, vui lòng nhập lại.");
            }
            
            
            var sinhvien = new Data.SinhVien()
            {
                HoTen = sv.HoTen,
                NgaySinh = sv.NgaySinh,
                GioiTinh = sv.GioiTinh,
                MaKhoa = sv.MaKhoa
            };
            _context.SinhViens.Add(sinhvien);
            _context.SaveChanges();
        }

        public bool DeleteSinhVien(int id)
        {
            var sinhvien = _context.SinhViens.Find(id);
            if (sinhvien == null)
            {
                return false;
            }
            _context.SinhViens.Remove(sinhvien);
            _context.SaveChanges();
            return true;
        }

        public List<SinhVienModel> GetSinhVien(int page = 1)
        {
            #region Pagezing
            var allStudents = _context.SinhViens.AsQueryable();
            if (page < 1)
            {
                throw new Exception("Số trang phải lớn hơn >=1 ");
            }
            else
            {
                var result = PaginatedList<Data.SinhVien>.Create(allStudents, page, PAGE_SIZE);
                return result.Select(x => new SinhVienModel
                {
                    MaSV = x.MaSV,
                    HoTen = x.HoTen,
                    NgaySinh = x.NgaySinh,
                    GioiTinh = x.GioiTinh,
                    MaKhoa = x.MaKhoa
                }).ToList();
            }
            #endregion

            
        }
        public int totalPage()
        {
            var allStudents = _context.SinhViens.AsQueryable();
            int total=(int)Math.Ceiling(allStudents.Count() / (double)PAGE_SIZE);
            return total;
        }
        public List<SinhVienModel> SearchSinhVien(string search)
        {
            var data = _context.SinhViens.Where(x => x.MaSV.ToString().Contains(search) || x.HoTen.Contains(search) || x.NgaySinh.ToString().Contains(search) || x.GioiTinh.Contains(search));
            var result = data.Select(x => new SinhVienModel
            {
                MaSV = x.MaSV,
                HoTen = x.HoTen,
                NgaySinh = x.NgaySinh,
                GioiTinh = x.GioiTinh,
                MaKhoa = x.MaKhoa
            }).ToList();
            return result.ToList();
        }

        public void UpdateSinhVien(int id, SinhVienModel svEdit)
        {
            try
            {
                var sv = _context.SinhViens.FirstOrDefault(x => x.MaSV == id);
                if (String.IsNullOrWhiteSpace(svEdit.HoTen) || svEdit.HoTen.Length > 100)
                {
                    throw new Exception("Họ tên không hợp lệ.");
                }
                DateTime now = DateTime.Now;
                if (svEdit.NgaySinh != null && svEdit.NgaySinh > now)
                {
                    throw new Exception("Ngày sinh của sinh viên không được lớn hơn ngày tháng hiện tại.");
                }
                if (String.IsNullOrWhiteSpace(svEdit.GioiTinh) || (svEdit.GioiTinh != "Nam" && svEdit.GioiTinh != "Nữ"))
                {
                    throw new Exception("Giới tính phải là Nam hoặc Nữ.");
                }
                if (_context.Khoas.SingleOrDefault(x => x.MaKhoa.Equals(sv.MaKhoa)) == null)
                {
                    throw new Exception("Không tồn tại mã khoa, vui lòng nhập lại.");
                }
                if (svEdit == null)
                {
                    throw new Exception("Không có sinh viên này trong danh sách.");
                }
                if (id != svEdit.MaSV)
                {
                    throw new Exception("Mã sinh viên không hợp lệ.");
                }

                //update
                sv.HoTen = svEdit.HoTen;
                sv.NgaySinh = svEdit.NgaySinh;
                sv.GioiTinh = svEdit.GioiTinh;
                sv.MaKhoa = svEdit.MaKhoa;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
