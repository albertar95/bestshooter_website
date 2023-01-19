using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bestshooter.Models.DomainModel;
namespace Bestshooter.ViewModel
{
    public class AdminViewModel
    {
        public Admin admin { get; set; }
        public List<Admin> admins { get; set; }
    }
}