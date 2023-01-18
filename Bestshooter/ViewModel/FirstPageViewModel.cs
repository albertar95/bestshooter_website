using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bestshooter.Models.DomainModel;
namespace Bestshooter.ViewModel
{
    public class FirstPageViewModel
    {
        public List<Pack> MostOrdered { get; set; }
        public List<Game> AllGames { get; set; }
        public Setting sets { get; set; }
    }
}