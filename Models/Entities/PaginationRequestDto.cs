﻿namespace UserManagement.Models.Entities
{
    public class PaginationRequestDto
    {
        public int PageNumber { get; set; } = 1;    
        public int PageSize { get; set; } = 10;
        public bool IsDescending { get; set; } = true;

    }
}
