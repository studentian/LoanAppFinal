//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LoanAppLibrary
{
    using System;
    using System.Collections.Generic;
    
    public partial class Log
    {
        public int LogId { get; set; }
        public int UserId { get; set; }
        public string Action { get; set; }
        public System.DateTime Date { get; set; }
        public decimal Offer_Amount { get; set; }
        public int Term { get; set; }
        public double Interest_Rate { get; set; }
        public string Status { get; set; }
        public int LevelId { get; set; }
        public string Offer_Status { get; set; }
        public string Provider_name { get; set; }
    
        public virtual User User { get; set; }
    }
}