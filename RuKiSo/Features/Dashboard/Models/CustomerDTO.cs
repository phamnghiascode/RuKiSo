﻿using RuKiSo.Utils.MVVM;

namespace RuKiSo.Features.Models
{
    public class CustomerDTO : BaseModel
    {
        public string Name { get; set; }
        public string Img { get; set; } = "defautavatar.jpg";
        public int Quantity { get; set; }
    }
}
