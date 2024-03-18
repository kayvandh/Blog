using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Model.Model
{
    public class PostRate
    {
        public Guid Id { get; set; }
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        public int Value { get; set; }

        public virtual Post Post { get; set; }
        public virtual User User { get; set; }  
    }
}
