using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Model.DTO
{
    public class GetPostsResponse
    {
        public List<Post> Posts { get; set; }
        public int TotalRowCount { get; set; }

    }
}
