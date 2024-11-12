using System;

namespace BoysCoffe.Models
{
    // Lưu lịch sử đăng nhập
    public class LoginHistory
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime LoginTime { get; set; }
    }
}