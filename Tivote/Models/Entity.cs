﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Tivote.Models
{
    public abstract class Entity
    {
        [Key]
        public Guid Id { get; set; }
    }
}

