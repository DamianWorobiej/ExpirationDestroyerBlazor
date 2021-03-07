using System;

namespace ExpirationDestroyerBlazorServer.BusinessLogic.DTOs
{
    public class ProductDTO
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public DateTime ExpirationDate { get; set; }

        public bool Expired { get; set; }

        public bool Consumed { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime DeletedAt { get; set; }

        public ProductDTO Clone()
        {
            return (ProductDTO)this.MemberwiseClone();
        }
    }
}
