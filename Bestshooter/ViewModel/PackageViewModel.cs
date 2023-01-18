using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bestshooter.Models.DomainModel;
namespace Bestshooter.ViewModel
{
    public class PackageViewModel
    {
        public Game Game { get; set; }
        public List<Pack> Packages { get; set; }
        public List<ViewModel.PacklistViewModel> pl { get; set; }
    }
}