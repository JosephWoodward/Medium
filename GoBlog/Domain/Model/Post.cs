﻿using System;
using System.ComponentModel.DataAnnotations;

namespace GoBlog.Domain.Model
{
    public class Post
    {
        [Key]
        [Required]
        public string Slug { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Summary { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime PublishDate { get; set; }
    }
}