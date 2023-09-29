﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarListApp.Maui.Models
{
    [Table("Car")]
    public class Car : BaseEntity
    {
        public string Make { get; set; }
        
        public string Model { get; set; }

        [MaxLength(12), Unique]
        public string Vin { get; set; }

        [Ignore]
        public string Display
        {
            get
            {
                return $"{Make} {Model}";
            }
        }
    }
}
