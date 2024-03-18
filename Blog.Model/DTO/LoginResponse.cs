using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Model.DTO
{
    public class LoginResponse
    {
        public User User { get; set; }
        public TokenDetail TokenDetail { get; set; }
    }
}
