﻿namespace ProductsService.Domain.Core;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int AvailableStock { get; set; }
    public int UserId { get; set; }
    public DateTime DateCreated { get; set; }
}