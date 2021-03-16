using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpirationDestroyerBlazorServer.DataAccess.Models
{
    public class Product
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public DateTime ExpirationDate { get; set; }

        [NotMapped]
        public bool Expired
        {
            get
            {
                // Comparing DateTime with time part is bullshit. Just putting it up there.
                // Resolved just by date instead.
                return DateTime.Today.Date > this.ExpirationDate.Date;
            }
        }

        public bool Consumed { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime DeletedAt { get; set; }

    }
}
