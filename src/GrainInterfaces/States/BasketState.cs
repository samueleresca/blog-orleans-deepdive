using System;
using System.Collections.Generic;
using System.Linq;

namespace GrainInterfaces.States
{
    public abstract class Product
    {
        public int ProductID { get; }

        public string ProductCategory { get; }

        public string SubCategory { get; }

        public string ProductName { get; }

        public string ProductDescription { get; }

        public decimal ProductPrice { get; }

        public double ProductWeight { get; set; }
    }

    public abstract class Basket
    {
        public Guid Id { get; set; }
        
        public List<Product> Products { get; set; }
        
        public string PromoCode { get; set; }

        public decimal Total
        {
            get { return Products.Sum(_ => _.ProductPrice); }
        }
    }

    public class BasketState
    {
        public Basket Value { get; set; }
    }
}