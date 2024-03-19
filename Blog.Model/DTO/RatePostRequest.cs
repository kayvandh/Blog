using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Model.DTO
{
    public class RatePostRequest
    {
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        [Range(1,5)]
        public int Value { get; set; }
    }
}
