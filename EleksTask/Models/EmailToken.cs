using System;
using System.ComponentModel.DataAnnotations;

namespace TourServer.Models
{
    public class EmailToken
    {
        [Key]
        public int Id { get; set; }

        public Guid Token { get; set; }

        public string UserId { get; set; }
    }
}
