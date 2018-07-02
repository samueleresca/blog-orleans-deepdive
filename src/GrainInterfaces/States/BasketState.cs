using System;
using System.Collections.Generic;
using System.Linq;

namespace GrainInterfaces.States
{
    [Serializable]
    public class Product
    {
        public int ProductID { get; set; }

        public string ProductCategory { get; set; }

        public string SubCategory { get; set; }

        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        public decimal ProductPrice { get; }

        public double ProductWeight { get; set; }
    }

    [Serializable]
    public class Basket
    {
        public Guid Id { get; set; }
        
        public List<Product> Products { get; set; }
        
        public string PromoCode { get; set; }

        public decimal Total
        {
            get
            {
                return Products?.Sum(_ => _.ProductPrice) ?? 0;
            }
        }
    }

    [Serializable]
    public class BasketState
    {
        public Basket Value { get; set; }
    }
}