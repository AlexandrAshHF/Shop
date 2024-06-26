﻿namespace CursVN.Core.Models
{
    public enum OrderStatus
    {
        None = 0,
        Processed = 1,
        Sent = 2,
        Received = 3,
    }
    public class Order
    {
        private Order(Guid id, DateTime dateOfCreate, OrderStatus status,
            List<ProductInOrder> products, decimal amount, Guid userId)
        {
            Id = id;
            DateOfCreate = dateOfCreate;
            Status = status;
            Products = products;
            UserId = userId;
            Amount = amount;
        }

        public Guid Id { get; set; }
        public DateTime DateOfCreate { get; set; }
        public OrderStatus Status { get; set; }
        public List<ProductInOrder> Products { get; set; }
        public decimal Amount { get; set; }
        public Guid UserId { get; set; }
        public static ModelWrapper<Order> Create(Guid id, DateTime dateOfCreate, OrderStatus status,
            List<ProductInOrder> products, decimal amount, Guid userId)
        {
            return new ModelWrapper<Order>(
                    model: new Order(id, dateOfCreate, status, products, amount, userId),
                    error: string.Empty,
                    valid: true
                    );
        }
    }
}