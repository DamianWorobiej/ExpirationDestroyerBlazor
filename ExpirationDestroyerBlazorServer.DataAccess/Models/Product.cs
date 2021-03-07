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
                return this.ExpirationDate < DateTime.Now;
            }
        }

        public bool Consumed { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime DeletedAt { get; set; }

    }
}
