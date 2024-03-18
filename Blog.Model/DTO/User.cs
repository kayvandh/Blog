using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Mapping;
using System.Text.Json.Serialization;

namespace Blog.Model.DTO
{

    public class User : IDefaultMapFrom<Model.User>, IDefaultMapTo<Model.User>
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }

        [JsonIgnore]
        public bool Active { get; set; }
    }
}
