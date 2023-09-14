using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApiSinhVien.Data;
using System.Linq;
using MyApiSinhVien.Models;
namespace MyApiSinhVien.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhoaController : ControllerBase
    {
        private MyDbContext _context;

        public KhoaController(MyDbContext context) {
            _context = context;
        }
        [HttpGet] 
        
        public IActionResult Get()
        {
            var query = _context.Khoas.Select(x=>new KhoaModel
            {
                MaKhoa = x.MaKhoa,
                TenKhoa = x.TenKhoa
            }).ToList();
            return Ok(query);
        }
    }
}
