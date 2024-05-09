﻿namespace CursVN.Core.Models
{
    public class Product
    {
        private Product(Guid id, string name, string description, decimal price,
            byte discount, List<string> imageLinks, Guid typeId, List<string> paramValues)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Discount = discount;
            ImageLinks = imageLinks;
            TypeId = typeId;
            ParamValues = paramValues;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public byte Discount { get; set; }
        public List<string> ImageLinks { get; set; }
        public Guid TypeId { get; set; }
        public List<string> ParamValues { get; set; }
        public static ModelWrapper<Product> Create(Guid id, string name, string description, decimal price,
            byte discount, List<string> imageLinks, Guid typeId, List<string> paramValues)
        {
            return new ModelWrapper<Product>(
                model: new Product(id, name, description, price, discount, imageLinks, typeId, paramValues),
                error: string.Empty,
                valid: true
                );
        }
    }
}
