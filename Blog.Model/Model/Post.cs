using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Model.Model
{
    public class Post
    {
        public Guid Id { get; set; }
        [StringLength(50)]
        [Required]
        public string Title { get; set; }

        [StringLength(200)]
        public string? Description { get; set; }

        [Required]
        public string Body { get; set; }

        [StringLength(50)]
        [Required]
        public string Writer { get; set; }

        public string? Properties { get; set; }


        public virtual List<PostRate> PostRates { get; set; }
    }
}
