using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EleksTask.Models
{
    public class Category
    {
        
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();
    }
}
