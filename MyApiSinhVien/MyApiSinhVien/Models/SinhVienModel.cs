using System;

namespace MyApiSinhVien.Models
{
    public class SinhVienModel
    {
        public int MaSV { get; set; }
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set;}
        public string GioiTinh { get; set; }
        public int MaKhoa { get; set; }

        public SinhVienModel() { }
        public SinhVienModel( string hoTen, DateTime ngaySinh, string gioiTinh, int khoa)
        {
            HoTen = hoTen;
            NgaySinh = ngaySinh;
            GioiTinh = gioiTinh;
            MaKhoa = khoa;
        }
    }
}
