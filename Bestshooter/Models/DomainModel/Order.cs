//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Bestshooter.Models.DomainModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        public Order()
        {
            this.Factors = new HashSet<Factor>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sirname { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string PackIds { get; set; }
        public Nullable<int> FactorId { get; set; }
        public string TotalFee { get; set; }
        public System.DateTime Date { get; set; }
    
        public virtual ICollection<Factor> Factors { get; set; }
    }
}
