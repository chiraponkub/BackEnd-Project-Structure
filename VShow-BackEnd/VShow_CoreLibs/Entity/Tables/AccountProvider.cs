// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace VShow_CoreLibs.Entity.Tables
{
    public partial class AccountProvider
    {
        public long AccId { get; set; }
        public string ProviderType { get; set; }
        public long UserId { get; set; }
        public string Username { get; set; }
        public string HashPassword { get; set; }
        public bool AccountVerify { get; set; }
        public string ProviderKey { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}