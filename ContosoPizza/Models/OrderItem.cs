﻿namespace ContosoPizza.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int PizzaId { get; set; }
        public virtual Order Order { get; set; }
        public virtual Pizza Pizza { get; set; }
        public virtual ICollection<OrderTopping> Toppings { get; set; }
    }
}
