using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bestshooter.Models.DomainModel;
namespace Bestshooter.ViewModel
{
    public class CheckoutPageViewModel
    {
        public List<ViewModel.ChecksViewModel> vm { get; set; }
        public string pa { get; set; }
        public string fee { get; set; }
    }
}