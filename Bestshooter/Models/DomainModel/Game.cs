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
    
    public partial class Game
    {
        public Game()
        {
            this.Packs = new HashSet<Pack>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Pic1 { get; set; }
        public string Pic2 { get; set; }
        public string pic3 { get; set; }
        public string Preface { get; set; }
        public string Cont { get; set; }
    
        public virtual ICollection<Pack> Packs { get; set; }
    }
}
