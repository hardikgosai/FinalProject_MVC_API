using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        public string? ProductName { get; set; }

        public string? ProductDescription { get; set; }

        public int? ProductPrice { get; set;}

        public int? CategoryId { get; set;}

        [JsonIgnore]
        public Category? Category { get; set; }
    }
}
