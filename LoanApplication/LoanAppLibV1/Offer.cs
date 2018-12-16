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
    
    public partial class Offer
    {
        public int OfferId { get; set; }
        public int UserId { get; set; }
        public Nullable<int> QuoteId { get; set; }
        public decimal OfferAmount { get; set; }
        public int Term { get; set; }
        public double InterestRate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<int> OfferStatus { get; set; }
        public string ProviderName { get; set; }
        public string CompanyReg { get; set; }
    
        public virtual OfferStatu OfferStatu { get; set; }
        public virtual UserFinancial UserFinancial { get; set; }
    }
}
