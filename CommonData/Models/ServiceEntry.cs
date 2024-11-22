﻿using API.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Models
{
    public class ServiceEntry : IDbEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
