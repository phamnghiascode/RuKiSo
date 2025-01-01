﻿namespace RuKiSoBackEnd.Models.DTOs
{
    public class IngredientAPIDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public int Quantity { get; set; }
        public double PurchasePrice { get; set; }
    }
}
