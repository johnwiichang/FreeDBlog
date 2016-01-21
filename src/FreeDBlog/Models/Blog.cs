using System;
using System.ComponentModel.DataAnnotations;

namespace FreeDBlog.Models
{
    public class Blog
    {
        [Required]
        public String Id { get; set; }

        [Required]
        public DateTime CreateTime { get; set; }

        [Required]
        [DataType(DataType.Html)]
        public String Content { get; set; }
    }
}