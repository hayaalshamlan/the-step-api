﻿namespace WebApplication3.Models
{
    public class AddBankRequest
    {
        public string Name { get; set; }
        public string LocationURL { get; set; }
        public string LocationName { get; set; }
        public string BranchManager { get; set; }
        public int EmployeeCount { get; set; }
        public int Id { get; set; }
    }
}
