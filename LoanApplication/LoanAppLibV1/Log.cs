//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LoanAppLibV1
{
    using System;
    using System.Collections.Generic;
    
    public partial class Log
    {
        public int LogId { get; set; }
        public Nullable<int> UserId { get; set; }
        public string Action { get; set; }
        public System.DateTime Date { get; set; }
        public Nullable<decimal> OfferAmount { get; set; }
        public Nullable<int> Term { get; set; }
        public Nullable<double> InterestRate { get; set; }
        public string Status { get; set; }
        public Nullable<int> LevelId { get; set; }
        public string OfferStatus { get; set; }
        public string ProviderName { get; set; }
        public string Description { get; set; }
    
        public virtual User User { get; set; }
    }
}
