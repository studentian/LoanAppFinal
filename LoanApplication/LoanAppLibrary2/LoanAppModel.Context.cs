﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LoanAppLibrary2
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class loanappdbEntities : DbContext
    {
        public loanappdbEntities()
            : base("name=loanappdbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AccessLevel> AccessLevels { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Offer> Offers { get; set; }
        public virtual DbSet<UserFinancial> UserFinancials { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
