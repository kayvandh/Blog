using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Model.DTO
{
    public class TokenDetail
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpireDateTime { get; set; }
    }
}
