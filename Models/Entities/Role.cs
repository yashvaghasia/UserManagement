﻿using UserManagement.Models.Entities;

public class Role : BaseEntity
{
    public string Name { get; set; }

    public ICollection<User> Users { get; set; }
    public bool IsDeleted { get; internal set; }
}
