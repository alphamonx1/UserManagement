﻿namespace ProductService.Application.DTOs
{
    public class CreateProductRequestDTO
    {
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
    }
}
