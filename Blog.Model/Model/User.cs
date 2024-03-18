using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Model.Model
{

    public class User
    {
        public Guid Id { get; set; }

        [StringLength(50)]
        [Required]
        public string UserName { get; set; }


        [StringLength(200)]
        [Required]
        public string Password { get; set; }

        [StringLength(500)]
        public string? Token { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? TokenExpireDateTime { get; set; }
        public bool Active { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? LastLoginDateTime { get; set; }

        [StringLength(200)]
        public string? LastLoginIpAddress { get; set; }

        public string? Properties { get; set; }

        public virtual List<PostRate> PostRates { get; set; }
    }
}
