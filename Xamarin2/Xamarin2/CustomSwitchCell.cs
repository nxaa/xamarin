using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Xamarin2
{
    public class CustomSwitchCell : SwitchCell
    {
        public int ItemID { get; set; }
        public int Number { get; set; }
        public int NumberOfPeople { get; set; }
        public bool Selected { get; set; }
    }
}
