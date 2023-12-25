﻿using DutchTreat.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace DutchTreat.ViewModels
{
    public class OrderItemViewModel
    {
        public Guid Id { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
        [Required]
        public Guid ProductId { get; set; }

        public string ProductCategory { get; set; }
        public string ProductSize { get; set; }
        public string ProductTitle { get; set; }
        public string ProductArtist { get; set; }
        public string ProductArtId { get; set; }
    }
}