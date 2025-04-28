﻿using System;

namespace UserManagement.Models.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;
    }
}
