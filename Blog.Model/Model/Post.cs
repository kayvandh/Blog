﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Model.Model
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public string Writer { get; set; }
        public string Properties { get; set; }

    }
}
