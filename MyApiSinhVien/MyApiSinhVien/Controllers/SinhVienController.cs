using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApiSinhVien.Data;
using MyApiSinhVien.Models;
using MyApiSinhVien.Service.SinhVien;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyApiSinhVien.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SinhVienController : ControllerBase
    {
        private readonly ISinhVienService _SinhVienService;
        public SinhVienController(ISinhVienService SinhVienService) {
            _SinhVienService = SinhVienService;
        }
        
        [HttpGet]
        public IActionResult GetAll(int page=1)
        {
            try
            {
                var result = _SinhVienService.GetSinhVien(page);
                int total = _SinhVienService.totalPage();
                return Ok(new
                {
                    result,
                    total
                });
            }catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("{search}")]
        public IActionResult GetSearch(string search)
        {
            return Ok(_SinhVienService.SearchSinhVien(search));
        }

        [HttpPost]
        public IActionResult Create(SinhVienModel sv)
        {
            try
            {
                _SinhVienService.AddSinhVien(sv);
                return Ok("Thêm sinh viên thành công.");
            }
            catch (Exception ex)
            {
                // Bắt và xử lý ngoại lệ ở đây
                // Bạn có thể không hiển thị lỗi nếu không muốn
                return StatusCode(500, "Có lỗi xảy ra khi thêm sinh viên.\n" + ex.Message);
            }
        }
        [HttpPut("{id}")]
        public IActionResult Edit(int id, SinhVienModel svEdit)
        {
            try
            {
                _SinhVienService.UpdateSinhVien(id,svEdit);
                return Ok("Cập nhập sinh viên thành công.");
            }
            catch (Exception ex)
            {
                // Bắt và xử lý ngoại lệ ở đây
                // Bạn có thể không hiển thị lỗi nếu không muốn
                return StatusCode(500, "Có lỗi xảy ra khi thêm sinh viên.\n" + ex.Message);
            }

        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            return Ok(_SinhVienService.DeleteSinhVien(id));
        }
        
        
    }
}
