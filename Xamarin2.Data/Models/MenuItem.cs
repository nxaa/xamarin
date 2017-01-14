using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamarin2.Data.Models
{
    public class MenuItem
    {
        public int MenuItemID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public bool Active { get; set; }

        public MenuItemCategory MenuItemCategory { get; set; }
    }

    public enum MenuItemCategory
    {
        Cold_Drink,
        Hot_Drink,
        Soup,
        Appetizer,
        Main_Course,
        Dessert
    }
}
