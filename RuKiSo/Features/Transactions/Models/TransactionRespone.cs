﻿using RuKiSo.Utils.MVVM;

namespace RuKiSo.Features.Transactions.Models
{
    public class TransactionRespone : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool TranType { get; set; }
        public DateTime TranDate { get; set; }
        public double Value { get; set; }
        public int Quantity { get; set; }
    }
}
