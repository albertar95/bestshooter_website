using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bestshooter.Models.DomainModel;
namespace Bestshooter.ViewModel
{
    public class ChecksViewModel
    {
        public Pack pack { get; set; }
        public int qty { get; set; }
        public string totalFee { get; set; }
    }
}