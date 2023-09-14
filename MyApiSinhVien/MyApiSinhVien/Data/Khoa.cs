using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApiSinhVien.Data
{
    [Table("Khoa")]
    public class Khoa
    {
        [Key]
        public int MaKhoa { get; set; }
        [Required]
        [StringLength(100)]
        public string TenKhoa { get; set; }

        public virtual ICollection<SinhVien> SinhViens { get; set; }
    }
}
