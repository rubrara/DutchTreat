﻿namespace DutchTreat.Data.Entities
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public Product? Product { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public Order? Order { get; set; }
    }
}
