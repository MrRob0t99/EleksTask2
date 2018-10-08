﻿using System.Collections.Generic;

namespace EleksTask.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public  string Description { get; set; }

        public double Price { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public List<BasketProduct> Baskets { get; set; }
    }
}