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
    using System.Collections.Generic;
    
    public partial class Datum
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Card_Number { get; set; }
        public int CVV { get; set; }
        public int Pin { get; set; }
        public Nullable<int> Current_Balance { get; set; }
        public string Card_Validation { get; set; }
    }
}