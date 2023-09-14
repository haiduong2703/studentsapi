using MyApiSinhVien.Models;
using System;
using System.Collections.Generic;

namespace MyApiSinhVien.Service.SinhVien
{
    public interface ISinhVienService
    {
        List<SinhVienModel> GetSinhVien(int page=1);
        List<SinhVienModel> SearchSinhVien(string value);
        void AddSinhVien(SinhVienModel sv);
        void UpdateSinhVien(int id,SinhVienModel sv);
        int totalPage();
        Boolean DeleteSinhVien(int id);
    }
}
