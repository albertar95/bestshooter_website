using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bestshooter.Models.DomainModel;

namespace Bestshooter.ViewModel
{
    public class FactorViewModel
    {
        public Factor factor { get; set; }
        public Order order { get; set; }
        public List<Pack> packs { get; set; }
    }
}