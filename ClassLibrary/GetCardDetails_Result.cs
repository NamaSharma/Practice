//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClassLibrary
{
    using System;
    
    public partial class GetCardDetails_Result
    {
        public long CardNumber { get; set; }
        public string Name { get; set; }
        public int Cvv { get; set; }
        public int Pin { get; set; }
        public System.DateTime ExpiryDate { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<long> CurrentBalance { get; set; }
    }
}
