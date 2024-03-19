using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Model.DTO
{
    public abstract class ListRequest
    {
        public int TakeCount { get; set; }
        public int Skip { get; set; }
    }
}
