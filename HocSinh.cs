namespace NguyenDucBinh_2021603555.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HocSinh")]
    public partial class HocSinh
    {
        [Key]
        [StringLength(10)]
        [Required(ErrorMessage = "Khong duoc de trong sbd")]
        [DisplayName("So bao danh")]
        public string sbd { get; set; }

        [Required(ErrorMessage ="Khong duoc de trong ho ten")]
        [DisplayName("Ho ten")]
        [StringLength(50)]
        public string hoten { get; set; }

        [StringLength(50)]
        public string anhduthi { get; set; }

        [Required(ErrorMessage = "Khong duoc de trong ma lop")]
        [DisplayName("Ma lop")]
        public int? malop { get; set; }

        [Required(ErrorMessage = "Khong duoc de trong diem thi")]
        [DisplayName("Diem thi")]
        public double? diemthi { get; set; }

        public virtual LopHoc LopHoc { get; set; }
    }
}
