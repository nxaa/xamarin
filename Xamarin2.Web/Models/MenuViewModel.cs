using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Xamarin2.Data.Models;

namespace Xamarin2.Web.Models
{
    public class MenuViewModel
    {

        public string Name { get; set; }
        
        public string Description { get; set; }

        public double Price { get; set; }

        [DisplayName("Category")]
        public string MenuItemCategory { get; set; }
    }
}