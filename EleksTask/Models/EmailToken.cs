using System;

namespace TourServer.Models
{
    public class EmailToken
    {
        public int Id { get; set; }

        public Guid Token { get; set; }

        public int UserId { get; set; }
    }
}
