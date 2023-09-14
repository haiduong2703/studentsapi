using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApiSinhVien.Data
{
    [Table("SinhVien")]
    public class SinhVien
    {
        [Key]
        public int MaSV { get; set; }
        [Required]
        [StringLength(100)]
        public string HoTen { get; set; }
        [DataType(DataType.Date)]
        public DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; }

        public int MaKhoa { get; set; }
        [ForeignKey("MaKhoa")]
        public Khoa Khoa { get; set; }
    }
}
